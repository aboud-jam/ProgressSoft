using Core.DTO.BusinessCardDTO;
using Core.Entity;
using Core.IServicesl;
using Core.Settings;
using Presistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services;

public class BusinessCardServices : IBusinessCardServices
{
    private readonly IRepository<BusinessCard> _context;
    public BusinessCardServices(IRepository<BusinessCard> context)
    {
       _context = context;
    }
    public async Task<ServiceOperationResult> AddBusinessCard(BusinessCardDetails model)
    {
        var finalResult = new ServiceOperationResult();
        if (model is null)
        {
            finalResult.ErrorCodes.Add(Core.Enums.Errors.InvalidRequest);
            return finalResult;
        }
        if (await _context.Any(item => item.Email == model.Email))
        {
            finalResult.ErrorCodes.Add(Core.Enums.Errors.EmailExists);
        }
        if (await _context.Any(item => item.Phone == model.Phone))
        {
            finalResult.ErrorCodes.Add(Core.Enums.Errors.AlreadyExist);
        }
        if (await _context.Any(item => item.Name == model.Name))
        {
            finalResult.ErrorCodes.Add(Core.Enums.Errors.AlreadyExist);
        }
        if (!finalResult.IsSuccessful)
        {
            return finalResult;
        }
        var newBusinessCard = model.Convert();
        _context.Add(newBusinessCard);
        await _context.SaveChangesAsync();
        return finalResult;
    }

    public async Task<ServiceOperationResult> DeleteBusinessCard(int id)
    {
        var finalResult = new ServiceOperationResult();
        //var item = await _context.GetSingle(predicate:item => item.Id == id);
        var item = await _context.GetSingle(predicate: a=> a.Id == id);
        if (item is null)
        {
            finalResult.ErrorCodes.Add(Core.Enums.Errors.ItemNotFound);
            return finalResult;
        }
        
            _context.Delete(item);
            await _context.SaveChangesAsync();
        
        return finalResult;
        
    }

    public async Task<List<BusinessCardDetails>> GetAllBusinessCard(BusinessCardForRequest model)
    {
        bool filterByName = !string.IsNullOrEmpty(model.Name);
        bool filterByPhone = !string.IsNullOrEmpty(model.Phone);
        bool filterByGender = !string.IsNullOrEmpty(model.Gender);
        bool filterByEmail = !string.IsNullOrEmpty(model.Email);

        var result = await _context.GetList(selector: item => new BusinessCardDetails
        {
            DateOfBirth = item.DateOfBirth,
            Email = item.Email,
            Gender = item.Gender,
            Phone = item.Phone,
            Name = item.Name,
            Address = item.Address,
        },
        predicate: a =>
        (!filterByEmail || (a.Email.ToLower().Contains(model.Email.ToLower()))) &&
        (!filterByPhone || (a.Phone.ToLower().Contains(model.Phone.ToLower()))) &&
        (!filterByGender || (a.Gender.ToLower().Contains(model.Gender.ToLower()))) &&
        (!filterByName || (a.Name.ToLower().Contains(model.Name.ToLower())))
        );
        return result.ToList();
    }
}
