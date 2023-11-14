using CsvHelper.Configuration;
using Doctors.Data;

public sealed class ResidentRecordMap : ClassMap<ResidentRecord>
{
    public ResidentRecordMap()
    {
        Map(m => m.ResidentLastName).Name("Resident Last Name");
        Map(m => m.ResidentFirstName).Name("Resident First Name");
        Map(m => m.ResidentId).Name("Resident Id");
        Map(m => m.Age).Name("Age");
        Map(m => m.LocationFloor).Name("Location Floor");
        Map(m => m.LocationUnit).Name("Location Unit");
        Map(m => m.LocationRoom).Name("Location Room");
        Map(m => m.LocationBed).Name("Location Bed");
        Map(m => m.AdmissionDate).Name("Admission Date");
        Map(m => m.Status).Name("Status");
    }
}
