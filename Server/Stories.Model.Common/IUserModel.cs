﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.Model.Common
{
    public interface IUserModel
    {
        Guid PersonID { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Email { get; set; }
    }
}
