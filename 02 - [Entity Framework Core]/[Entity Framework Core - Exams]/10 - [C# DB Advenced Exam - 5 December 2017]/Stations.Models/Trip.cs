using System;
using Stations.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stations.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OriginStationId { get; set; }

        [Required]
        public virtual Station OriginStation { get; set; }

        [Required]
        public int DestinationStationId { get; set; }

        [Required]
        public virtual Station DestinationStation { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public int TrainId { get; set; }

        [Required]
        public virtual Train Train { get; set; }

        [Required]
        public TripStatus Status { get; set; }

        public TimeSpan? TimeDifference { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
            = new HashSet<Ticket>();
    }
}