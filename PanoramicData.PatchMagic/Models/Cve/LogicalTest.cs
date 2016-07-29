using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PanoramicData.PatchMagic.Models.Cve
{
	public class LogicalTest
	{
		[Required]
		public VulnerableConfiguration VulnerableConfiguration { get; set; }
		public LogicalTestOperator Operator { get; set; }

		[DefaultValue(false)]
		public bool Negate { get; set; }
	}
}
