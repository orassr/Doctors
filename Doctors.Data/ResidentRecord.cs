namespace Doctors.Data
{
    public class ResidentRecord
    {
        public string? ResidentLastName { get; set; }
        public string? ResidentFirstName { get; set; }
        public string? ResidentId { get; set; }
        public int Age { get; set; }
        public string? LocationFloor { get; set; }
        public string? LocationUnit { get; set; }
        public string? LocationRoom { get; set; }
        public string? LocationBed { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string? Status { get; set; }
    }
}