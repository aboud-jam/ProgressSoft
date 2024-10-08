using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity;

public interface ICreationInfo
{
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
}
