using Stations.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stations.DataProcessor.Dto.Import
{
    public class ImportTrainDto
    {
        [Required]
        [MaxLength(10)]
        public string TrainNumber { get; set; }

        [Range(0, 2)]
        public TrainType? Type { get; set; }

        public List<ImportTrainSeatDto> Seats { get; set; }
            = new List<ImportTrainSeatDto>();
    }
}