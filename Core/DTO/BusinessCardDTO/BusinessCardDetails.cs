using Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.BusinessCardDTO;

public class BusinessCardDetails
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    [MaxLength(1048576)] // 1 MB = 1,048,576 bytes

    public string Address { get; set; }

    public BusinessCard Convert()
    {
        return new BusinessCard
        {
            Name = this.Name,
            Gender = this.Gender,
            DateOfBirth = this.DateOfBirth,
            Email = this.Email,
            Phone = this.Phone,
            Address = this.Address,
        };
    }
    
}
