using Microsoft.EntityFrameworkCore;
using PanoramicData.PatchMagic.Models;
using PanoramicData.PatchMagic.Models.Cpe;
using Xunit;

namespace PanoramicData.PatchMagic.Test
{
	public  class SimpleDatabaseTest
	{
		[Fact]
		public void SimpleDatabaseTest1()
		{
			var options = new DbContextOptionsBuilder<CodeContext>()
				.UseInMemoryDatabase(nameof(SimpleDatabaseTest1))
				.Options;

			using (var ctx = new CodeContext(options))
			{
				ctx.CpeItems.Add(new CpeItem());
				ctx.SaveChanges();
				Assert.Single(ctx.CpeItems);
			}
		}
	}
}
