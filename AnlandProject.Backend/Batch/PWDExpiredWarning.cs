using AnlandProject.Backend.Extensions;
using AnlandProject.Service;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AnlandProject.Backend.Batch
{
    public class PWDExpiredWarning : IJob
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        private IAccountService _accountService;

        public async Task Execute(IJobExecutionContext context)
        {
            //await Console.Out.WriteLineAsync("HelloJob is executing.");
            //logger.Trace("HelloJob is executing.");
            await Task.Run(() => GetExpiredPWDMem());
        }

        private void GetExpiredPWDMem()
        {
            try
            {
                logger.Trace("********GetExpiredPWD  Start********");
                ISMTPSetupService _smptSetupService;

                using (_smptSetupService = new SMTPSetupService())
                using (_accountService = new AccountService())
                {
                    List<AccountModel> data = _accountService.GetExpiredData();
                    if (data.Count > 0)
                    {
                        logger.Trace($"Expired members are {string.Join(", ", data.Select(d => d.Account))}.");
                        foreach (var item in data)
                        {
                            SMTPSetupModel smtpData = _smptSetupService.SMTPSetupQuery("email");
                            smtpData.SendMailBySMTPPWDExpiredWarning(item.Email, item.Account, item.PWDChangeDate);
                        }
                    }
                    else
                    {
                        logger.Trace("No expired members.");
                    }
                }
                logger.Trace("********GetExpiredPWD  End********");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
    }
}