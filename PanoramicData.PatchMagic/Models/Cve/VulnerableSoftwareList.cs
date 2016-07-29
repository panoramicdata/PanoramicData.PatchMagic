using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PanoramicData.PatchMagic.Models.Cve
{
	public class VulnerableSoftwareList : IdentifiedItem
	{
		[Required]
		public Entry Entry { get; set; }
		public List<VulnerableProduct> VulnerableProducts { get; set; }
	}
}