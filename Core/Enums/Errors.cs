using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums;

public enum Errors
{
    [Description("Item not found")]
    ItemNotFound = 0,
    [Description("Operation not allowed")]
    OperationNotAllowed = 1,
    [Description("Operations not saved")]
    OperationsNotSaved = 2,
    [Description("Already Exist")]
    AlreadyExist = 3,
    [Description("Invalid Request")]
    InvalidRequest = 4,
    [Description("Invalid Username or Password")]
    InvalidUsernameOrPassword = 5,
    [Description("Uploading file not valid")]
    UploadingFileNotValid = 6,
    [Description("Operation not completed")]
    OperationNotCompleted = 10,
    [Description("No new calls to process")]
    NoNewCallsToProcess = 11,
    [Description("Username Exists")]
    UsernameExists = 15,
    [Description("Email Exists")]
    EmailExists = 16,
    [Description("Name Exists")]
    NameExists = 18,
    
}
