using Stations.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Stations.DataProcessor.Dto.Import
{
    public class ImportTripDto
    {
        [Required]
        [MaxLength(10)]
        public string Train { get; set; }

        [Required]
        public string OriginStation { get; set; }

        [Required]
        public string DestinationStation { get; set; }

        [Required]
        public string DepartureTime { get; set; }

        [Required]
        public string ArrivalTime { get; set; }

        [Range(0, 2)]
        public TripStatus? Status { get; set; }

        public string TimeDifference { get; set; }
    }
}