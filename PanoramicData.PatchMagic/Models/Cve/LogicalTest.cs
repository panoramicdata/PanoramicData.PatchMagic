using System.Collections.Generic;
using System.ComponentModel;

namespace PanoramicData.PatchMagic.Models.Cve
{
	public class LogicalTest
	{
		//[Required]
		//public VulnerableConfiguration VulnerableConfiguration { get; set; }
		public LogicalTestOperator Operator { get; set; }

		[DefaultValue(false)]
		public bool Negate { get; set; }
		//public ICollection<>
	}
}
