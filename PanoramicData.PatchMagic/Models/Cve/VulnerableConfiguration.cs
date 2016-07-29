using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PanoramicData.PatchMagic.Models.Cve
{
	public class VulnerableConfiguration : IdentifiedItem
	{
		[Required]
		public Entry Entry { get; set; }
		public string VcId { get; set; } // e.g. id="http://nvd.nist.gov/"
		public ICollection<LogicalTest> LogicalTests { get; set; }
	}
}