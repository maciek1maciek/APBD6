namespace Excercise.Models
{
    public class Prescription
    {

        public int IdPrescription { get; set; } 

        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }

        public int IdDoctor { get; set; }
        public int IdPatient { get; set; }

        public virtual Doctor Doctor { get; set; } = null!;
        
        public virtual Patient Patient { get; set; } = null!;

        public virtual ICollection<PrescriptionMedicament> PrescriptionMedicament { get; set; } = new List<PrescriptionMedicament>();

    }
}
