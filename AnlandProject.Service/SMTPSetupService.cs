using AnlandProject.Models;
using AnlandProject.Models.Interface;
using AnlandProject.Models.Repository;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service
{
    public class SMTPSetupService : ISMTPSetupService
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        private IRepository<setup_smtp> _smtpSetupRepository = new GenericRepository<setup_smtp>();

        public SMTPSetupModel SMTPSetupQuery(string type)
        {
            var tempData = _smtpSetupRepository.Get(q => q.type == type);

            SMTPSetupModel result = null;
            if (tempData != null)
            {
                result = new SMTPSetupModel()
                {
                    ID = tempData.ID,
                    Type = tempData.type,
                    SMTP = tempData.smtp,
                    SMTPUserName = tempData.smtpusername,
                    SMTPPassword = tempData.smtppassword,
                    Recipient = tempData.recipient,
                    RecipientName = tempData.recipientname,
                    Subject = tempData.subject
                };
            }
            return result;
        }

        public bool SMPTSetupSave(SMTPSetupModel saveData)
        {
            int resultRow = 0;
            try
            {
                var originalData = _smtpSetupRepository.Get(q => q.type == saveData.Type);
                originalData.smtp = saveData.SMTP;
                originalData.smtpusername = saveData.SMTPUserName;
                if (!string.IsNullOrWhiteSpace(saveData.SMTPPassword)) originalData.smtppassword = saveData.SMTPPassword;
                originalData.recipient = saveData.Recipient;
                originalData.recipientname = saveData.RecipientName;
                originalData.subject = saveData.Subject;
                resultRow = _smtpSetupRepository.Update(originalData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public void Dispose()
        {
            if (_smtpSetupRepository != null)
            {
                _smtpSetupRepository.Dispose();
            }
        }
    }
}
