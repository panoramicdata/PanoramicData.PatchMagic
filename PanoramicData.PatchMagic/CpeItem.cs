using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PanoramicData.PatchMagic
{
	public class CpeItem
	{
		public string Name { get; set; }
		public string Title { get; set; }
		public string Cpe23Name { get; set; }
		public List<Reference> References { get; set; }
	}
}
