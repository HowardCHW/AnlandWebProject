using AnlandProject.Models;
using AnlandProject.Models.Interface;
using AnlandProject.Models.Repository;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using AnlandProject.Service.Utility;
using System;
using System.Linq.Expressions;

namespace AnlandProject.Service
{
    public class AccountService : IAccountService
    {
        private IRepository<admin> _accountRepository = new GenericRepository<admin>();

        public bool IsExists(string account)
        {
            return _accountRepository.GetCount(x => x.name == account) > 0;
        }

        public AccountModel AccountQuery(LogonViewModel logonModel)
        {
            string value = System.Configuration.ConfigurationManager.AppSettings["SkipVerifyPassword"];
            bool.TryParse(value, out bool isSkip);

            Expression<Func<admin, bool>> queryExpr = x => x.username == logonModel.Account;
            if (!isSkip)
            {
                queryExpr.And(x => x.passwd == logonModel.Password);                
            }

            var data = _accountRepository.Get(queryExpr);
            AccountModel result = null;
            if (data != null)
            {
                result = new AccountModel()
                {
                    Account = data.name,
                    Name = data.username
                };                
            }
            return result;
        }
    }
}
