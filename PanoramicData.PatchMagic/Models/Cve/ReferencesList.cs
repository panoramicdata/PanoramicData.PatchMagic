using System.ComponentModel.DataAnnotations;

namespace PanoramicData.PatchMagic.Models.Cve
{
	public class ReferencesList : IdentifiedItem
	{
		[Required]
		public CveReferences CveReferences { get; set; }
		public string Href { get; set; }
		public string Value { get; set; }
	}
}