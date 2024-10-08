using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices;

public interface IExportService
{
    Task<string> ExportToXmlAsync<T>(IEnumerable<T> data);
    Task<byte[]> ExportToExcelAsync<T>(IEnumerable<T> data);
}
