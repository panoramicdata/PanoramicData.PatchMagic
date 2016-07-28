using System.Collections.Generic;

namespace PanoramicData.PatchMagic.Models.Cve
{
	public class References : IdentifiedItem
	{
		public Entry Entry { get; set; }
		public ReferenceType ReferenceType { get; set; }
		public string Source { get; set; }
		public ICollection<Reference> ReferenceList { get; set; }
	}
}