using Microsoft.EntityFrameworkCore;
using Xunit;

namespace PanoramicData.PatchMagic.Test
{
	public  class SimpleDatabaseTest
	{
		[Fact]
		public void SimpleDatabaseTest1()
		{
			using (var ctx = new CodeContext(new DbContextOptions<CodeContext>()))
			{
				ctx.CpeItems.Add(new CpeItem());
				ctx.SaveChanges();
			}
			Assert.Equal(true, true);
		}
	}
}
