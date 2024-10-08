using Core.DTO.BusinessCardDTO;
using Core.IServicesl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProgressSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessCardController(IBusinessCardServices businessCardServices) : ControllerBase
    {
        private readonly IBusinessCardServices _businessCardServices = businessCardServices;

        //[ServiceFilter(typeof(ActionHistoryFilter))]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BusinessCardDetails request)
        {
            return Ok(await _businessCardServices.AddBusinessCard(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(_businessCardServices.DeleteBusinessCard(id));
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] BusinessCardForRequest request)
        {
            return Ok(await _businessCardServices.GetAllBusinessCard(request));
        }
    }
}
