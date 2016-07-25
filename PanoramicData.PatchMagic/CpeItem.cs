using System.Collections.Generic;

namespace PanoramicData.PatchMagic
{
	public class CpeItem : IdentifiedItem
	{
		public string Name { get; set; }
		public string Title { get; set; }
		public string Cpe23Name { get; set; }
		public ICollection<Reference> References { get; set; }
	}
}
