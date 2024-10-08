using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity;

public class BusinessCard :ICreationInfo,ISoftDeletion
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Gender { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [EmailAddress]
    public string ?Email { get; set; }

    [Required]
    public string? Phone { get; set; }

    [MaxLength(1048576)] // 1 MB = 1,048,576 bytes
    public byte[]? Photo { get; set; }

    public string Address { get; set; }
    public bool IsDeleted { get ; set ; }
    public DateTime? DeletedOn { get ; set ; }
    public DateTime CreatedOn { get; set ; }
    public string CreatedBy { get ; set ; }
}
