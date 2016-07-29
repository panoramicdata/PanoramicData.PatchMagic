using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PanoramicData.PatchMagic.Models.Cve
{
	public class Entry : IdentifiedItem
	{
		public BaseMetrics BaseMetrics { get; set; }
		[Required]
		public string CveId { get; set; }
		public string CweId { get; set; }
		public DateTime? LastModifiedDateTime { get; set; }
		[Required]
		public DateTime? PublisheDateTime { get; set; }
		public ICollection<CveReferences> References { get; set; }
		public string Summary { get; set; }
		public VulnerableConfiguration VulnerableConfiguration { get; set; }
		public VulnerableSoftwareList VulnerableSoftwareList { get; set; }
		
	}
}
