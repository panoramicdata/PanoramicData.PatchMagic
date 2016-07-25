using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using Microsoft.EntityFrameworkCore;

namespace PanoramicData.PatchMagic.Controllers
{
	[Route("api/[controller]")]
	public class CommonPlatformEnumerationsController : Controller
	{
		[HttpGet("update")]
		public IActionResult Update()
		{
			// Download the latest
			var fileInfo = GetValue("http://static.nvd.nist.gov/", "/feeds/xml/cpe/dictionary/official-cpe-dictionary_v2.3.xml.zip").Result;
			//var fileInfo = GetValue("https://scap.nist.gov/", "schema/cpe/2.3/cpe-dictionary_2.3.xsd").Result;

			// Unzip to a temp directory
			var tempDirectoryInfo = GetTemporaryDirectory();
			ZipFile.ExtractToDirectory(fileInfo.FullName, tempDirectoryInfo.FullName);

			// Get file
			var filePath = Directory.GetFiles(tempDirectoryInfo.FullName).First(f => f.EndsWith("xml"));

			// Create a new XmlDocument
			var xmlDoc = new XmlDocument();

			try
			{
				xmlDoc.LoadXml(System.IO.File.ReadAllText(filePath));
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
				return Ok("Could not process XML");
			}

			var cpeItemsList = ProcessXml(xmlDoc);

			// Update the database
			// Note: http://www.asp.net/web-api/overview/error-handling/web-api-global-error-handling
			UpdateDatabaseFromCpeItemsList(cpeItemsList);

			return Ok("Successfully loaded XML");
		}

		private static void UpdateDatabaseFromCpeItemsList(IEnumerable<CpeItem> cpeItemsList)
		{
			var options = new DbContextOptions<CodeContext>();
			using (var ctx = new CodeContext(options))
			{
				foreach (var cpeItem in cpeItemsList)
				{
					ctx.CpeItems.Add(cpeItem);
				}
				ctx.SaveChanges();
			}
		}

		private static List<CpeItem> ProcessXml(XmlDocument xmlDoc)
		{
			var cpeItemsList = new List<CpeItem>();

			foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes)
			{
				if (xmlNode.Name != "cpe-item") continue;

				var cpeItem = new CpeItem
				{
					Name = xmlNode.Attributes["name"].Value
				};

				var references = new List<Reference>();

				foreach (XmlNode childNode in xmlNode.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "title":
							cpeItem.Title = childNode.InnerText;
							break;
						case "references":
							references.AddRange(from XmlNode referenceNode in childNode.ChildNodes
								select new Reference
								{
									Href = referenceNode.Attributes["href"].Value
								});
							break;
						case "cpe-23:cpe23-item":
							cpeItem.Cpe23Name = childNode.InnerText;
							break;
					}
				}
				cpeItem.References = references;

				cpeItemsList.Add(cpeItem);
			}
			return cpeItemsList;
		}

		private DirectoryInfo GetTemporaryDirectory() => Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));

		private static async Task<FileInfo> GetValue(string baseUrl, string remotePath)
		{
			const int largeBufferSize = 64000;
			var tempFileName = Path.GetTempFileName();

			var httpClient = new HttpClient
			{
				BaseAddress = new Uri(baseUrl)
			};

			using (var request = new HttpRequestMessage(HttpMethod.Get, remotePath))
			{
				using (Stream contentStream = await (await httpClient.SendAsync(request)).Content.ReadAsStreamAsync(),
						stream = new FileStream(tempFileName, FileMode.Create, FileAccess.Write, FileShare.None, largeBufferSize, true))
				{
					await contentStream.CopyToAsync(stream);
				}
			}
			return new FileInfo(tempFileName);
		}

		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		[HttpGet("test")]
		public IActionResult Test()
		{
			using (var ctx = new CodeContext(new DbContextOptions<CodeContext>()))
			{
				ctx.CpeItems.Add(new CpeItem());
				ctx.SaveChanges();
			}
			return Ok("OK");
		}
	}
}