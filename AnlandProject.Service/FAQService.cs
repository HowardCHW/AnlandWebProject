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
    public class FAQService : IFAQService
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        private IRepository<email> _faqRepository = new GenericRepository<email>();
        
        public List<FAQModel> FAQQueryAll()
        {
            var result = _faqRepository.GetAll().Select(n => new FAQModel()
            {
                No = n.no,
                MsgDate = n.msg_date,
                MsgName = n.msg_name,
                MsgEmail = n.msg_email,
                MsgSubject = n.msg_subject,
                MsgContent = n.msg_content,
                RpyUnit = n.rpy_unit,
                RpyDate = n.rpy_date,
                RpyName = n.rpy_name,
                RpyTel = n.rpy_tel,
                RpyContent = n.rpy_content,
                Attach1 = n.attach1,
                Attach2 = n.attach2,
                Attach3 = n.attach3,
                MsgTel = n.msg_tel,
                MsgType = n.msg_type
            }).ToList();

            return result;
        }

        public FAQModel FAQQueryByNo(int no)
        {
            var tempData = _faqRepository.Get(x => x.no == no);

            FAQModel result = null;
            if (tempData != null)
            {
                result = new FAQModel()
                {
                    No = tempData.no,
                    MsgDate = tempData.msg_date,
                    MsgName = tempData.msg_name,
                    MsgEmail = tempData.msg_email,
                    MsgSubject = tempData.msg_subject,
                    MsgContent = tempData.msg_content,
                    RpyUnit = tempData.rpy_unit,
                    RpyDate = tempData.rpy_date,
                    RpyName = tempData.rpy_name,
                    RpyTel = tempData.rpy_tel,
                    RpyContent = tempData.rpy_content,
                    Attach1 = tempData.attach1,
                    Attach2 = tempData.attach2,
                    Attach3 = tempData.attach3,
                    MsgTel = tempData.msg_tel,
                    MsgType = tempData.msg_type
                };
            }
            return result;
        }

        public bool FAQSave(FAQModel saveData)
        {
            int resultRow = 0;
            try
            {
                if (saveData.No > 0)
                {
                    var originalData = _faqRepository.Get(n => n.no == saveData.No);
                    //originalData.no = saveData.No;
                    //originalData.msg_date = saveData.MsgDate;
                    //originalData.msg_name = saveData.MsgName;
                    //originalData.msg_email = saveData.MsgEmail;
                    //originalData.msg_subject = saveData.MsgSubject;
                    //originalData.msg_content = saveData.MsgContent;
                    originalData.rpy_unit = saveData.RpyUnit;
                    originalData.rpy_date = saveData.RpyDate;
                    originalData.rpy_name = saveData.RpyName;
                    originalData.rpy_tel = saveData.RpyTel;
                    originalData.rpy_content = saveData.RpyContent;
                    originalData.attach1 = saveData.Attach1;
                    originalData.attach2 = saveData.Attach2;
                    originalData.attach3 = saveData.Attach3;
                    //originalData.msg_tel = saveData.MsgTel;
                    //originalData.msg_type = saveData.MsgType;
                    resultRow = _faqRepository.Update(originalData);
                }
                else
                {
                    //目前沒有新增
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public bool FAQDelete(int no)
        {
            int result = 0;
            try
            {
                var deleteData = _faqRepository.Get(n => n.no == no);
                result = _faqRepository.Delete(deleteData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }

        public void Dispose()
        {
            if (_faqRepository != null)
            {
                _faqRepository.Dispose();
            }
        }
    }
}
