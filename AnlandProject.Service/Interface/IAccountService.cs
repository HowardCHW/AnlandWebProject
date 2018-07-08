using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface IAccountService : IDisposable
    {
        bool IsExists(string account);

        List<AccountModel> AccountQueryAll();

        AccountModel AccountQueryByID(int id);

        AccountModel AccountQuery(LogonViewModel logonModel);

        bool AccountSave(AccountModel saveData);

        bool AccountDelete(int id);
    }
}
