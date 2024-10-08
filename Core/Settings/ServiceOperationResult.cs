using Core.Enums;
using Core.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Settings;

public class ServiceOperationResult<T>
{
    public ServiceOperationResult()
    {
        ErrorCodes = new List<Errors>();
        Result = new List<T>();
    }

    public bool IsSuccessful
    {
        get
        {
            return ErrorCodes.Count == 0;
        }
    }
    public List<T> Result { get; set; }
    public List<Errors> ErrorCodes { get; set; }
    public List<string> Errors
    {
        get
        {
            var _Errors = new List<string>();
            foreach (var error in ErrorCodes)
            {
                _Errors.Add(error.GetEnumDescription());
            }
            return _Errors;
        }
    }
}
public class ServiceOperationResult
{
    public ServiceOperationResult()
    {
        ErrorCodes = new List<Errors>();
    }

    public bool IsSuccessful
    {
        get
        {
            return ErrorCodes.Count == 0;
        }
    }
    public List<Errors> ErrorCodes { get; set; }
    public List<string> Errors
    {
        get
        {
            var _Errors = new List<string>();
            foreach (var error in ErrorCodes)
            {
                _Errors.Add(error.GetEnumDescription());
            }
            return _Errors;
        }
    }
}

