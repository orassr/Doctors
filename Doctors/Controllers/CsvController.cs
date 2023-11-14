using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Globalization;
using CsvHelper;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using Doctors.Data;

[Route("api/[controller]")]
[ApiController]
public class CsvController : ControllerBase
{
    [HttpPost("upload")]
    public IActionResult Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Upload a CSV file.");

        try
        {
            List<ResidentRecord> records;
            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<ResidentRecordMap>();
                records = csv.GetRecords<ResidentRecord>().ToList();
            }

            // Create Excel file
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.AddWorksheet("Sheet1");

                // Set the headers
                worksheet.Cell("A1").Value = $"HW {DateTime.Now:MM/dd/yy} {DateTime.Now:dddd}";
                worksheet.Cell("A2").Value = "[_] H&Ps [_] Labs/Rads [_] Dictate [_] TxfrTextToNotes [_] SignOldNotes [_] Bill [_] OldOrders [_] NewOrders [_] Cl";

                // Set column titles
                worksheet.Cell("B3").Value = "B/S";
                worksheet.Cell("C3").Value = "Resident Last Name";
                worksheet.Cell("D3").Value = "Resident First Name";
                worksheet.Cell("E3").Value = "Age";
                worksheet.Cell("F3").Value = "Location Room";
                worksheet.Cell("G3").Value = "Location Bed";
                worksheet.Cell("H3").Value = "Admission Date";
                worksheet.Cell("I3").Value = "Status";

                // Populate data
                int currentRow = 4;
                foreach (var record in records)
                {
                    worksheet.Cell(currentRow, 3).Value = record.ResidentLastName;
                    worksheet.Cell(currentRow, 4).Value = record.ResidentFirstName;
                    worksheet.Cell(currentRow, 5).Value = record.Age;
                    worksheet.Cell(currentRow, 6).Value = record.LocationRoom;
                    worksheet.Cell(currentRow, 7).Value = record.LocationBed;
                    worksheet.Cell(currentRow, 8).Value = record.AdmissionDate;
                    worksheet.Cell(currentRow, 9).Value = record.Status;
                    currentRow++;
                }

                // Adjust column widths
                worksheet.Columns().AdjustToContents();

                // Save to MemoryStream
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    // Return the Excel file as a download
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProcessedData.xlsx");
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error processing file: " + ex.Message);
        }
    }
}

// Assuming ResidentRecord and ResidentRecordMap are defined as before
