using System.ComponentModel.DataAnnotations;
using PetStore.Data;

namespace PetStore.Web.Models.View_Models.Category
{
    using static DataValidations;

    public class CategoryEditInputModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CATEGORY_NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [MaxLength(CATEGORY_DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; }
    }
}