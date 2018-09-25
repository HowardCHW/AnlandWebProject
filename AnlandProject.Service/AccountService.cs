using AnlandProject.Models;
using AnlandProject.Models.Interface;
using AnlandProject.Models.Repository;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using AnlandProject.Service.Utility;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AnlandProject.Service
{
    public class AccountService : IAccountService
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        private IRepository<admin> _accountRepository = new GenericRepository<admin>();

        public bool IsExists(string account)
        {
            return _accountRepository.GetCount(x => x.name == account) > 0;
        }

        public List<AccountModel> AccountQueryAll()
        {
            var result = _accountRepository.GetAll().Select(a => new AccountModel
            {
                ID = a.ID,
                Account = a.username,
                Name = a.name,
                PWD = a.passwd
            }).ToList();
            return result;
        }

        public AccountModel AccountQueryByID(int id)
        {
            var tempData = _accountRepository.Get(i => i.ID == id);

            AccountModel result = null;
            if (tempData != null)
            {
                result = new AccountModel()
                {
                    ID = tempData.ID,
                    Account = tempData.username,
                    Name = tempData.name,
                    PWD = tempData.passwd,
                    Rights = tempData.rights
                };
            }
            return result;
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
                    ID = data.ID,
                    Account = data.username,
                    Name = data.name,
                    Rights = data.rights
                };
            }
            return result;
        }

        public bool AccountSave(AccountModel saveData)
        {
            int resultRow = 0;
            try
            {
                if (saveData.ID > 0)
                {
                    var originalData = _accountRepository.Get(n => n.ID == saveData.ID);
                    originalData.username = saveData.Account;
                    if (!string.IsNullOrWhiteSpace(saveData.PWD)) { originalData.passwd = saveData.PWD; }
                    originalData.name = saveData.Name;
                    originalData.rights = saveData.Rights;
                    resultRow = _accountRepository.Update(originalData);
                }
                else
                {
                    admin newData = new admin()
                    {
                        username = saveData.Account,
                        passwd = saveData.PWD,
                        name = saveData.Name,
                        rights = saveData.Rights
                    };
                    resultRow = _accountRepository.Create(newData);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public bool AccountDelete(int id)
        {
            int result = 0;
            try
            {
                var deleteData = _accountRepository.Get(i => i.ID == id);
                result = _accountRepository.Delete(deleteData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }

        public void Dispose()
        {
            if (_accountRepository != null)
            {
                _accountRepository.Dispose();
            }
        }
    }
}
