using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PanoramicData.PatchMagic.Models.Cve
{
	public class CveReferences : IdentifiedItem
	{
		[Required]
		public Entry Entry { get; set; }
		public ReferenceType ReferenceType { get; set; }
		public string Source { get; set; }
		public ICollection<ReferencesList> ReferenceList { get; set; }
	}
}