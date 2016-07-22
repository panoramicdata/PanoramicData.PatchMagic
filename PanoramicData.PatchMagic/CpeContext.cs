using Microsoft.EntityFrameworkCore;

namespace PanoramicData.PatchMagic
{
	public class CpeContext : DbContext
	{
		public CpeContext(DbContextOptions options) : base(options)
		{
			
		}
		public DbSet<CpeItem> CpeItems { get; set; }
		public DbSet<Reference> References { get; set; }
	}
}
