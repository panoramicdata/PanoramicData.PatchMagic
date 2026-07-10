using Microsoft.EntityFrameworkCore;
using PanoramicData.PatchMagic.Models.Cpe;
using PanoramicData.PatchMagic.Models.Cve;

namespace PanoramicData.PatchMagic
{
	public class CodeContext : DbContext
	{
		public CodeContext(DbContextOptions<CodeContext> options) : base(options)
		{
		}
		public DbSet<CpeItem> CpeItems { get; set; }
		public DbSet<Reference> References { get; set; }
		public DbSet<Entry> Entries { get; set; }
		public DbSet<VulnerableConfiguration> VulnerableConfigurations { get; set; }
		public DbSet<VulnerableSoftwareList> VulnerableSoftwareLists { get; set; }
		public DbSet<VulnerableProduct> VulnerableProducts { get; set; }
		public DbSet<CveReferences> CveReferences { get; set; }
		public DbSet<ReferencesList> ReferencesLists { get; set; }
		public DbSet<BaseMetrics> BaseMetrics { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<BaseMetrics>()
				.HasOne(baseMetrics => baseMetrics.Entry)
				.WithOne(entry => entry.BaseMetrics)
				.HasForeignKey<BaseMetrics>("EntryId");

			modelBuilder.Entity<VulnerableConfiguration>()
				.HasOne(configuration => configuration.Entry)
				.WithOne(entry => entry.VulnerableConfiguration)
				.HasForeignKey<VulnerableConfiguration>("EntryId");

			modelBuilder.Entity<VulnerableSoftwareList>()
				.HasOne(softwareList => softwareList.Entry)
				.WithOne(entry => entry.VulnerableSoftwareList)
				.HasForeignKey<VulnerableSoftwareList>("EntryId");
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PatchMagic;Trusted_Connection=True;");
			}
			//optionsBuilder.UseSqlServer("Server=.\\MSSQLSERVER2014;Initial Catalog=PatchMagic;Integrated Security=True;");
		}
	}
}
