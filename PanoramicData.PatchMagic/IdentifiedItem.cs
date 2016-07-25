using System.ComponentModel.DataAnnotations.Schema;

namespace PanoramicData.PatchMagic
{
	public abstract class IdentifiedItem
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
	}
}