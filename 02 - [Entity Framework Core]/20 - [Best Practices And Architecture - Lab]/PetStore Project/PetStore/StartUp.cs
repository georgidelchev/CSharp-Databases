using PetStore.Data;
using PetStore.Services.Implementations;

namespace PetStore
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var data = new PetStoreDbContext();

            using (data)
            {
                var brandService = new BrandService(data);

                var brandWithToys = brandService.FindByIdWithToys(1);
            }
        }
    }
}
