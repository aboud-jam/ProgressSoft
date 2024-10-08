using Core.DTO.BusinessCardDTO;
using Core.IServices;
using Core.IServicesl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Text;

namespace ProgressSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly IExportService _exportService;
        private readonly IBusinessCardServices _businessCardsServices;
        public ExportController(IExportService exportService, IBusinessCardServices businessCardsServices)
        {
            _exportService = exportService;
            _businessCardsServices = businessCardsServices;
        }

        [HttpGet("xml")]
        public async Task<IActionResult> ExportToXml([FromQuery] BusinessCardForRequest filterModel)
        {
            var data = await _businessCardsServices.GetAllBusinessCard(filterModel);
            var xmlData = await _exportService.ExportToXmlAsync(data);

            return File(Encoding.UTF8.GetBytes(xmlData), "application/xml", "BusinessCards.xml");
        }

        [HttpGet("excel")]
        public async Task<IActionResult> ExportToExcel([FromQuery] BusinessCardForRequest filterModel)
        {
            var data = await _businessCardsServices.GetAllBusinessCard(filterModel);

            // Generate the Excel file from the data
            var excelFile = await _exportService.ExportToExcelAsync(data);

            // Return the file as a downloadable attachment
            return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BusinessCards.xlsx");
        }
    }
}
