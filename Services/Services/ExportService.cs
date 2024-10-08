using ClosedXML.Excel;
using Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Services.Services;

public class ExportService : IExportService
{
    public async Task<byte[]> ExportToExcelAsync<T>(IEnumerable<T> data)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("BusinessCards");

            var properties = typeof(T).GetProperties();

            // Write the headers in the first row
            for (int i = 0; i < properties.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = properties[i].Name;
            }

            // Write the data starting from row 2
            int rowIndex = 2;
            foreach (var item in data)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cell(rowIndex, i + 1).Value = properties[i].GetValue(item)?.ToString();
                }
                rowIndex++;
            }

            // Auto adjust columns to fit content
            worksheet.Columns().AdjustToContents();

            // Save the Excel file to a byte array
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return await Task.FromResult(stream.ToArray());
            }
        }
    }

    public async Task<string> ExportToXmlAsync<T>(IEnumerable<T> data)
    {
        var xmlSerializer = new XmlSerializer(typeof(List<T>));
        using (var stringWriter = new StringWriter())
        {
            xmlSerializer.Serialize(stringWriter, data.ToList());
            return await Task.FromResult(stringWriter.ToString());
        }
    }
}
