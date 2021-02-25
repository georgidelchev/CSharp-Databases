using Microsoft.EntityFrameworkCore;
using PetClinic.Models;

namespace PetClinic.Data
{

    public class PetClinicContext : DbContext
    {
        public PetClinicContext()
        {
        }

        public PetClinicContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<AnimalAid> AnimalAids { get; set; }

        public DbSet<Passport> Passports { get; set; }

        public DbSet<Procedure> Procedures { get; set; }

        public DbSet<ProcedureAnimalAid> ProcedureAnimalAids { get; set; }

        public DbSet<Vet> Vets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<AnimalAid>(animalAid =>
                {
                    animalAid
                        .HasIndex(ai => ai.Name)
                        .IsUnique(true);
                });

            builder
                .Entity<ProcedureAnimalAid>(procedureAnimalAid =>
                {
                    procedureAnimalAid
                        .HasKey(paa => new
                        {
                            paa.AnimalAidId,
                            paa.ProcedureId
                        });
                });

            builder
                .Entity<Animal>(animal =>
                {
                    animal.HasOne(p => p.Passport)
                        .WithOne(a => a.Animal)
                        .HasForeignKey<Animal>(a => a.PassportSerialNumber)
                        .OnDelete(DeleteBehavior.Restrict);
                });

            builder
                .Entity<Vet>(vet =>
                {
                    vet
                        .HasIndex(v => v.PhoneNumber)
                        .IsUnique(true);
                });
        }
    }
}