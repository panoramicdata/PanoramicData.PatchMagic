using System;
using System.ComponentModel.DataAnnotations;

namespace PanoramicData.PatchMagic.Models.Cve
{
	public class BaseMetrics : IdentifiedItem
	{
		[Required]
		public Entry Entry { get; set; }
		public float Score { get; set; }
		public string AccessVector { get; set; }
		public AccessComplexityType AccessComplexity { get; set; }
		public AuthenticationType Authentication { get; set; }
		public ConfidentialityImpactType ConfidentialityImpact { get; set; }
		public IntegrityImpactType IntegrityImpact { get; set; }
		public AvailabilityImpactType AvailabilityImpact { get; set; }
		public string Source { get; set; }
		public DateTime GeneratedOn { get; set; }
	}
}