using Excercise.Models;
using Microsoft.EntityFrameworkCore;

namespace Excercise.Data
{
     public class HospitalContext : DbContext
    {
        public DbSet<Doctor> Doctor { get; set; }

        public DbSet<Medicament> Medicament { get; set; }

        public DbSet<Patient> Patient { get; set; }
        public DbSet<Patient> Prescription { get; set; }
        public DbSet<Patient> PrescriptionMedicament { get; set; }

        public HospitalContext(DbContextOptions options) : base(options)
        {
        }

        public HospitalContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor);
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();

                entity.HasData(new List<Doctor>()
                {
                    new Doctor
                    {
                        IdDoctor = 1,
                        FirstName = "Jan",
                        LastName = "Kowalski",
                        Email = "test@test.com"
                    },
                    new Doctor
                    {
                        IdDoctor = 2,
                        FirstName = "Mirek",
                        LastName = "Kowalski",
                        Email = "test@test2.com"
                    }
                });

            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient);
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired(); 
                entity.Property(e => e.Birthdate).IsRequired();

                entity.HasData(new List<Patient>()
                {
                    new Patient
                    {
                        IdPatient = 1,
                        FirstName = "Jan",
                        LastName = "Kowalski",
                        Birthdate = new DateTime(1990, 10, 15)
                    },
                    new Patient
                    {
                        IdPatient = 2,
                        FirstName = "Mirek",
                        LastName = "Kowalski",
                        Birthdate = new DateTime(1990, 10, 15)
                    }
                });

            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament);
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Type).HasMaxLength(100).IsRequired();

                entity.HasData(new List<Medicament>()
                {
                    new Medicament
                    {
                        IdMedicament = 1,
                        Name = "APAP",
                        Description = "test",
                        Type = "nwm"
                    },
                    new Medicament
                    {
                        IdMedicament = 2,
                        Name = "Nurofen",
                        Description = "test2",
                        Type = "nwm"
                    }
                });

            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.DueDate).IsRequired();

                entity.HasOne(e => e.Doctor)
                     .WithMany()
                     .HasForeignKey(e => e.IdDoctor)
                     .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Patient)
                     .WithMany()
                     .HasForeignKey(e => e.IdPatient)
                     .OnDelete(DeleteBehavior.Cascade);

                entity.HasData(new List<Prescription>()
                {
                    new Prescription
                    {
                        IdPrescription = 1,
                        Date = new DateTime(1999, 2, 15),
                        DueDate = new DateTime(2002, 2, 16),
                        IdDoctor = 1,
                        IdPatient = 1,
                    },
                    new Prescription
                    {
                        IdPrescription = 2,
                        Date = new DateTime(1999, 3, 17),
                        DueDate = new DateTime(2010, 8, 1),
                        IdDoctor = 2,
                        IdPatient = 2,
                    }
                });


            });

            modelBuilder.Entity<PrescriptionMedicament>(entity =>
            {
                entity.ToTable("Prescription_Medicament");
                entity.HasKey(e => new {e.IdMedicament,e.IdPrescription});

                entity.Property(e => e.Dose).IsRequired(false);
                entity.Property(e => e.Details).HasMaxLength(100).IsRequired();

                entity.HasOne(e => e.Medicament)
                     .WithMany(e => e.PrescriptionMedicament)
                     .HasForeignKey(e => e.IdMedicament)
                     .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Prescription)
                     .WithMany(e => e.PrescriptionMedicament)
                     .HasForeignKey(e => e.IdPrescription)
                     .OnDelete(DeleteBehavior.Cascade);

                entity.HasData(new List<PrescriptionMedicament>()
                {
                    new PrescriptionMedicament
                    {
                        IdMedicament = 1,
                        IdPrescription = 1, 
                        Dose = 1,
                        Details = "Test"
                    },
                    new PrescriptionMedicament
                    {
                        IdMedicament = 2,
                        IdPrescription = 2,
                        Dose = 2,
                        Details = "Test2"
                    }
                });

            });
             

        }


    }
}
