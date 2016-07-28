using Microsoft.EntityFrameworkCore;
using PanoramicData.PatchMagic.Models.Cpe;

namespace PanoramicData.PatchMagic
{
	public class CodeContext : DbContext
	{
		public CodeContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<CpeItem> CpeItems { get; set; }
		public DbSet<Reference> References { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PatchMagic;Trusted_Connection=True;");
		}
	}
}
