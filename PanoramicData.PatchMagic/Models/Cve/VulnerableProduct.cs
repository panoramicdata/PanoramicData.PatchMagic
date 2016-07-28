namespace PanoramicData.PatchMagic.Models.Cve
{
	public class VulnerableProduct : IdentifiedItem
	{
		public VulnerableSoftwareList VulnerableSoftwareList { get; set; }
		public string Product { get; set; }
	}
}