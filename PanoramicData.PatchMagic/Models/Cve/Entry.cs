using System;
using System.Collections.Generic;

namespace PanoramicData.PatchMagic.Models.Cve
{
	public class Entry : IdentifiedItem
	{
		public string CveId { get; set; }
		public VulnerableConfiguration VulnerableConfiguration { get; set; }
		public VulnerableSoftwareList VulnerableSoftwareList { get; set; }
		public DateTime PublisheDateTime { get; set; }
		public DateTime LastModifiedDateTime { get; set; }
		public string CweId { get; set; }
		public ICollection<References> References { get; set; }
		public string Summary { get; set; }
		public BaseMetrics BaseMetrics { get; set; }
	}
}
