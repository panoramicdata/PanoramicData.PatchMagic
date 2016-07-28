namespace PanoramicData.PatchMagic.Models.Cve
{
	public class Reference : IdentifiedItem
	{
		public References References { get; set; }
		public string Href { get; set; }
		public string Value { get; set; }
	}
}