using System.Collections.Generic;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dto.Export
{
    [XmlType("Procedure")]
    public class ExportProcedureDto
    {
        public string Passport { get; set; }

        public string OwnerName { get; set; }

        public string DateTime { get; set; }

        [XmlArray]
        public List<ExportProcedureAnimalAidDto> AnimalAids { get; set; }
    }
}