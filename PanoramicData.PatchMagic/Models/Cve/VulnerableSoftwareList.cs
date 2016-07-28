using System.Collections.Generic;

namespace PanoramicData.PatchMagic.Models.Cve
{
	public class VulnerableSoftwareList : IdentifiedItem
	{
		public Entry Entry { get; set; }
		public List<VulnerableProduct> VulnerableProducts { get; set; }
	}
}