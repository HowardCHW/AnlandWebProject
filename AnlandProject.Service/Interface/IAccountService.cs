﻿using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface IAccountService
    {
        bool IsExists(string account);

        AccountModel AccountQuery(LogonViewModel logonModel);
    }
}