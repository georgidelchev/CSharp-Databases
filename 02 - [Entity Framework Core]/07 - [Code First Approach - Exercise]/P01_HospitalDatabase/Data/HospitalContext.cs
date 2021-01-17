using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {

        }

        public HospitalContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(Configuration.CONNECTION_STRING);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Patient>(patient =>
                {
                    patient
                        .HasKey(p => p.PatientId);

                    patient
                        .Property(p => p.FirstName)
                        .HasMaxLength(50)
                        .IsRequired(true)
                        .IsUnicode(true);

                    patient
                        .Property(p => p.LastName)
                        .HasMaxLength(50)
                        .IsRequired(true)
                        .IsUnicode(true);

                    patient
                        .Property(p => p.Address)
                        .HasMaxLength(250)
                        .IsRequired(true)
                        .IsUnicode(true);

                    patient
                        .Property(p => p.Email)
                        .HasMaxLength(80)
                        .IsRequired(true)
                        .IsUnicode(false);

                    patient
                        .Property(p => p.HasInsurance)
                        .IsRequired(true);
                });

            modelBuilder
                .Entity<Visitation>(visitations =>
                {
                    visitations
                        .HasKey(v => v.VisitationId);

                    visitations
                        .Property(v => v.Date)
                        .IsRequired(true);

                    visitations
                        .Property(v => v.Comments)
                        .HasMaxLength(250)
                        .IsRequired(false)
                        .IsUnicode(true);

                    visitations
                        .HasOne(v => v.Patient)
                        .WithMany(p => p.Visitations)
                        .HasForeignKey(v => v.PatientId);

                    visitations
                        .HasOne(v => v.Doctor)
                        .WithMany(d => d.Visitations)
                        .HasForeignKey(v => v.DoctorId);
                });

            modelBuilder
                .Entity<Diagnose>(diagnose =>
                {
                    diagnose
                        .HasKey(d => d.DiagnoseId);

                    diagnose
                        .Property(d => d.Name)
                        .HasMaxLength(50)
                        .IsRequired(true)
                        .IsUnicode(true);

                    diagnose
                        .Property(d => d.Comments)
                        .HasMaxLength(250)
                        .IsRequired(false)
                        .IsUnicode(true);

                    diagnose
                        .HasOne(d => d.Patient)
                        .WithMany(p => p.Diagnoses)
                        .HasForeignKey(d => d.PatientId);
                });

            modelBuilder
                .Entity<Medicament>(medicament =>
                {
                    medicament
                        .HasKey(m => m.MedicamentId);

                    medicament
                        .Property(m => m.Name)
                        .HasMaxLength(50)
                        .IsRequired(true)
                        .IsUnicode(true);
                });

            modelBuilder
                .Entity<PatientMedicament>(patientMedicament =>
                {
                    patientMedicament
                        .HasKey(pm => new
                        {
                            pm.PatientId,
                            pm.MedicamentId
                        });

                    patientMedicament
                        .HasOne(pm => pm.Patient)
                        .WithMany(p => p.Prescriptions)
                        .HasForeignKey(pm => pm.PatientId);

                    patientMedicament
                        .HasOne(pm => pm.Medicament)
                        .WithMany(m => m.Prescriptions)
                        .HasForeignKey(pm => pm.MedicamentId);
                });

            modelBuilder
                .Entity<Doctor>(doctor =>
                {
                    doctor
                        .HasKey(d => d.DoctorId);

                    doctor
                        .Property(d => d.Name)
                        .HasMaxLength(100)
                        .IsRequired(true)
                        .IsUnicode(true);

                    doctor
                        .Property(d => d.Specialty)
                        .HasMaxLength(100)
                        .IsRequired(true)
                        .IsUnicode(true);
                });
        }
    }
}