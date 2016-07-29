using System.ComponentModel.DataAnnotations;

namespace PanoramicData.PatchMagic.Models.Cve
{
	public class VulnerableProduct : IdentifiedItem
	{
		[Required]
		public VulnerableSoftwareList VulnerableSoftwareList { get; set; }
		public string Product { get; set; }
	}
}