using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Shell")]
    public class ImportShellsDto
    {
        [Required]
        [Range(2, 1_680)]
        public double ShellWeight { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Caliber { get; set; }
    }
}
