using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using PanoramicData.PatchMagic.Models;
using PanoramicData.PatchMagic.Models.Cpe;
using PanoramicData.PatchMagic.Models.Cve;

namespace PanoramicData.PatchMagic.Controllers
{
	[Route("api/[controller]")]
	public class CpeController : Controller
	{
		[HttpGet("update")]
		public IActionResult Update()
		{
			// Update CPE database
			if (!UpdateCpe())
			{
				return Ok("Could not process CPE XML");
			}

			// Update CVE database
			if (!UpdateCve())
			{
				return Ok("Could not process CVE XML");
			}

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

		private static void UpdateDatabaseFromCveItemsList(IEnumerable<Entry> entries)
		{
			var options = new DbContextOptions<CodeContext>();
			using (var ctx = new CodeContext(options))
			{
				foreach (var entry in entries)
				{
					ctx.Entries.Add(entry);
				}
				//ctx.SaveChanges();
			}
		}

		private static bool UpdateCve()
		{
			// Download the latest CVE data.
			var fileInfo = GetValue("http://static.nvd.nist.gov/", "/feeds/xml/cve/nvdcve-2.0-Recent.xml.zip").Result;
			//var fileInfo = GetValue("http://static.nvd.nist.gov/", "/feeds/xml/cve/nvdcve-2.0-2016.xml.zip").Result;

			// Unzip to a temporary directory.
			var tempDirectoryInfo = GetTemporaryDirectory();
			ZipFile.ExtractToDirectory(fileInfo.FullName, tempDirectoryInfo.FullName);

			// Get the file path.
			var filePath = Directory.GetFiles(tempDirectoryInfo.FullName).First(f => f.EndsWith("xml"));

			// Create a new XmlDocument and load.
			var xmlDoc = new XmlDocument();

			try
			{
				xmlDoc.LoadXml(System.IO.File.ReadAllText(filePath));
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);

				return false;
			}

			var entries = ProcessCveXml(filePath);

			// Update the database
			UpdateDatabaseFromCveItemsList(entries);

			return true;
		}

		private static bool UpdateCpe()
		{
			// Download the latest CPE list
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
				
				return false;
			}

			var cpeItemsList = ProcessCpeXml(xmlDoc);

			// Update the database
			UpdateDatabaseFromCpeItemsList(cpeItemsList); // Note: http://www.asp.net/web-api/overview/error-handling/web-api-global-error-handling

			return true;
		}

		private static IEnumerable<Entry> ProcessCveXml(string filePath)
		{
			// Read the XML
			var xDocument = XDocument.Load(filePath);
			var elements = xDocument.Root.Elements();
			var entries = new List<Entry>();

			// Process each entry
			foreach (var element in elements)
			{
				var entry = new Entry
				{
					CveId = element.Attributes("id").First().Value,
					References = new List<CveReferences>()
				};

				foreach (var childNode in element.Nodes())
				{
					var xElementNode = childNode as XElement;
					if (xElementNode == null) continue;

					switch (xElementNode.Name.LocalName)
					{
						case "vulnerable-configuration":

							break;
						case "references":
							var references = new CveReferences
							{
								ReferenceType = (ReferenceType) Enum.Parse(typeof(ReferenceType), xElementNode.Attribute("reference_type").Value, true),
								ReferenceList = new List<ReferencesList>()
							};

							// Process each sub node: either a source node or reference
							foreach (var referencesChildNode in xElementNode.Nodes())
							{
								var xReferencesChildNode = referencesChildNode as XElement;

								switch (xReferencesChildNode.Name.LocalName)
								{
									case "source":
										references.Source = xReferencesChildNode.Value;
										break;
									case "reference":
										references.ReferenceList.Add(new ReferencesList
										{
											Href = xReferencesChildNode.Attribute("href").Value,
											Value = xReferencesChildNode.Value
										});
										break;
									default:
										continue;
								}
							}
							entry.References.Add(references);
							break;
						case "summary":
							entry.Summary = xElementNode.Value;
							break;
						case "vulnerable-software-list":
							entry.VulnerableSoftwareList = new VulnerableSoftwareList();

							// Iterate product nodes
							foreach (var productNode in xElementNode.Nodes())
							{
								var xElementProduct = productNode as XElement;
								if (xElementProduct == null) continue;
								entry.VulnerableSoftwareList.VulnerableProducts = new List<VulnerableProduct>
								{
									new VulnerableProduct { Product = xElementProduct.Value }
								};
							}
							break;
						case "cve-id":
							entry.CveId = xElementNode.Value;
							break;
						case "published-datetime":
							entry.PublisheDateTime = DateTime.Parse(xElementNode.Value);
							break;
						case "last-modified-datetime":
							entry.LastModifiedDateTime = DateTime.Parse(xElementNode.Value);
							break;
						case "cwe":
							entry.CweId = xElementNode.Attribute("id").Value;
							break;
						case "cvss":
							// Find the base metrics (there should be only 1).
							foreach (var baseMetricsNode in xElementNode.Nodes())
							{
								var xElementBaseMetricsNode = baseMetricsNode as XElement;
								entry.BaseMetrics = new BaseMetrics();

								// Iterate the sub-nodes
								if (xElementBaseMetricsNode == null) continue;

								foreach (var baseMetricsChildNode in xElementBaseMetricsNode.Nodes())
								{
									var xBaseMetricsChildNode = baseMetricsChildNode as XElement;

									if (xBaseMetricsChildNode == null) continue;

									switch (xBaseMetricsChildNode.Name.LocalName)
									{
										case "score":
											entry.BaseMetrics.Score = float.Parse(xBaseMetricsChildNode.Value);
											break;
										case "access-vector":
											entry.BaseMetrics.AccessVector = xBaseMetricsChildNode.Value;
											break;
										case "access-complexity":
											entry.BaseMetrics.AccessComplexity = (AccessComplexityType) Enum.Parse(typeof(AccessComplexityType), xBaseMetricsChildNode.Value, true);
											break;
										case "authentication":
											entry.BaseMetrics.Authentication = (AuthenticationType) Enum.Parse(typeof(AuthenticationType), xBaseMetricsChildNode.Value, true);
											break;
										case "confidentiality-impact":
											entry.BaseMetrics.ConfidentialityImpact = (ConfidentialityImpactType) Enum.Parse(typeof(ConfidentialityImpactType), xBaseMetricsChildNode.Value, true);
											break;
										case "integrity-impact":
											entry.BaseMetrics.IntegrityImpact = (IntegrityImpactType) Enum.Parse(typeof(IntegrityImpactType), xBaseMetricsChildNode.Value, true);
											break;
										case "availability-impact":
											entry.BaseMetrics.AvailabilityImpact = (AvailabilityImpactType) Enum.Parse(typeof(AvailabilityImpactType), xBaseMetricsChildNode.Value, true);
											break;
										case "source":
											entry.BaseMetrics.Source = xBaseMetricsChildNode.Value;
											break;
										case "generated-on-datetime":
											entry.BaseMetrics.GeneratedOn = DateTime.Parse(xBaseMetricsChildNode.Value);
											break;
										default:
											// Skip to the next one.
											continue;
									}
								}
							}
							break;
						default:
							// Skip to the next one.
							continue;
					}
				}
				entries.Add(entry);
			}
			return entries;
		}

		private static IEnumerable<CpeItem> ProcessCpeXml(XmlDocument xmlDoc)
		{
			var cpeItemsList = new List<CpeItem>();

			foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes)
			{
				if (xmlNode.Name != "cpe-item") continue;

				var cpeItem = new CpeItem
				{
					Name = xmlNode.Attributes["name"].Value
				};

				var references = new List<Models.Cpe.Reference>();

				foreach (XmlNode childNode in xmlNode.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "title":
							cpeItem.Title = childNode.InnerText;
							break;
						case "references":
							references.AddRange(from XmlNode referenceNode in childNode.ChildNodes
								select new Models.Cpe.Reference
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

		private static DirectoryInfo GetTemporaryDirectory() => Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));

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