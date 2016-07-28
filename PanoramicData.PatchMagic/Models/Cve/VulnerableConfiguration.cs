namespace PanoramicData.PatchMagic.Models.Cve
{
	public class VulnerableConfiguration : IdentifiedItem
	{
		public Entry Entry { get; set; }
		public string VcId { get; set; } // e.g. id="http://nvd.nist.gov/"

	}
}