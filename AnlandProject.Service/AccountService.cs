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
using System.Web.Security;

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
                    Rights = tempData.rights,
                    IsAdmin = tempData.isadmin,
                    IsFirstLogin = tempData.is_first_time,
                    LoginFailCount = tempData.login_error_count,
                    Email = tempData.email,
                    PWDExpired = tempData.pwd_changed_date.HasValue && tempData.pwd_changed_date.Value.Date < DateTime.Now.Date
                };
            }
            return result;
        }

        public AccountModel AccountQuery(LogonViewModel logonModel)
        {
            //string value = System.Configuration.ConfigurationManager.AppSettings["SkipVerifyPassword"];
            //bool.TryParse(value, out bool isSkip);

            Expression<Func<admin, bool>> queryExpr = x => x.username == logonModel.Account;
            //if (!isSkip)
            //{
            //    queryExpr = queryExpr.AndAlso(x => x.passwd == logonModel.Password);
            //}

            var data = _accountRepository.Get(queryExpr);
            AccountModel result = null;
            if (data != null)
            {
                result = new AccountModel()
                {
                    ID = data.ID,
                    Account = data.username,
                    PWD = data.passwd,
                    Name = data.name,
                    Rights = data.rights,
                    IsAdmin = data.isadmin,
                    IsFirstLogin = data.is_first_time,
                    LoginFailCount = data.login_error_count,
                    PWDExpired = data.pwd_changed_date.HasValue && data.pwd_changed_date.Value.Date < DateTime.Now.Date
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
                    if (!string.IsNullOrWhiteSpace(saveData.PWD))
                    {
                        //新的密碼
                        originalData.passwd = saveData.PWD;
                        //紀錄下次修改密碼時間(當下時間加3個月)
                        originalData.pwd_changed_date = DateTime.Now.AddMonths(3);
                        //將現在修改的密碼同時寫入歷史密碼表中
                        originalData.OldPasswordRecored = new List<OldPasswordRecored>
                        {
                            new OldPasswordRecored
                            {
                                OldPassword = saveData.PWD,
                                ChangedDate = DateTime.Now
                            }
                        };
                        //第一次登入時修改密碼後將TAG改掉
                        if (originalData.is_first_time) originalData.is_first_time = false;
                    }
                    originalData.name = saveData.Name;
                    originalData.email = saveData.Email;
                    if (!string.IsNullOrWhiteSpace(saveData.Rights))
                    {
                        originalData.rights = saveData.Rights;
                    }
                    resultRow = _accountRepository.Update(originalData);
                }
                else
                {
                    string newPwd = Membership.GeneratePassword(8, 1);
                    admin newData = new admin()
                    {
                        username = saveData.Account,
                        passwd = newPwd,
                        name = saveData.Name,
                        rights = saveData.Rights,
                        email = saveData.Email,
                        is_first_time = true,
                        isadmin = false
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

        public void UpdateLoginError(string account, bool isError)
        {
            try
            {
                var data = _accountRepository.Get(a => a.username == account);
                if (isError)
                {
                    data.login_error_count += 1;
                }
                else
                {
                    data.login_error_count = 0;
                }
                _accountRepository.Update(data);
            }
            catch (Exception ex)
            {
                logger.Error(ex);                
            }
        }

        public bool PasswordCheck(string account, string newPwd)
        {
            admin accountData = _accountRepository.Get(a => a.username == account);
            if (accountData.OldPasswordRecored.Count > 0)
            {
                var temp = accountData.OldPasswordRecored.OrderByDescending(o => o.ChangedDate).Take(3);
                return temp.Any(t => t.OldPassword == newPwd);
            }
            return false;
        }

        public bool PasswordReset(int id, string defaultPwd)
        {
            int resultRow = 0;
            try
            {
                var tempData = _accountRepository.Get(i => i.ID == id);
                tempData.is_first_time = true;
                tempData.passwd = defaultPwd;
                tempData.login_error_count = 0;
                resultRow = _accountRepository.Update(tempData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public List<AccountModel> GetExpiredData()
        {
            List<AccountModel> result = new List<AccountModel>();
            DateTime thirdDay = DateTime.Now.AddDays(3).Date;
            result = _accountRepository.GetMany(a => a.pwd_changed_date <= thirdDay).Select(a => new AccountModel
            {
                Account = a.username,
                Email = a.email,
                PWDChangeDate = a.pwd_changed_date.Value
            }).ToList();

            return result;
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
