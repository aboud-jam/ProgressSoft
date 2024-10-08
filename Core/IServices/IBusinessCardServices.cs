using Core.DTO.BusinessCardDTO;
using Core.Entity;
using Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServicesl
{
    public interface IBusinessCardServices
    {
        Task<ServiceOperationResult> AddBusinessCard(BusinessCardDetails model);
        Task<ServiceOperationResult> DeleteBusinessCard(int id);
        Task<List<BusinessCardDetails>> GetAllBusinessCard(BusinessCardForRequest model);
    }
}
