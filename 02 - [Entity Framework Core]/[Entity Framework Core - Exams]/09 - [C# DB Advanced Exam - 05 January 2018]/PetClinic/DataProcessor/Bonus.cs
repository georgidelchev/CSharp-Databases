using System.Linq;
using PetClinic.Data;

namespace PetClinic.DataProcessor
{
    public class Bonus
    {
        public static string UpdateVetProfession(PetClinicContext context, string phoneNumber, string newProfession)
        {
            var vet = context.Vets.FirstOrDefault(v => v.PhoneNumber == phoneNumber);

            if (vet == null)
            {
                return $"Vet with phone number {phoneNumber} not found!";
            }

            var vetOldProfession = vet.Profession;

            vet.Profession = newProfession;
            context.SaveChanges();

            return $"{vet.Name}'s profession updated from {vetOldProfession} to {vet.Profession}.";
        }
    }
}