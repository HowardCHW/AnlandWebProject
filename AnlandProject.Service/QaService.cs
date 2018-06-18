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
    public class QaService : IQaService
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        private IRepository<qa> _qaRepository = new GenericRepository<qa>();
        
        public List<QaModel> QaQueryAll()
        {
            var result = _qaRepository.GetAll().Select(q => new QaModel()
            {
                ID = q.ID,
                Theme = q.Theme,
                Cake = q.Cake,
                Service = q.Service,
                Classfy = q.Classfy,
                Subject = q.Subject,
                Content = q.Content.Replace("<br>", "\n"),
                SubmitName = q.SubmitName,
                SubmitDate = q.SubmitDate,
                ModifyName = q.ModifyName,
                ModifyDate = q.ModifyDate,
                Hit = q.hit,
                CreatedDeptID = q.created_dept_id,
                CreatedUser = q.created_user_name,
                CreatedUserPhone = q.created_user_phone
            }).ToList();

            return result;
        }

        public QaModel QaQueryByID(int id)
        {
            var tempData = _qaRepository.Get(q => q.ID == id);

            QaModel result = null;
            if (tempData != null)
            {
                result = new QaModel()
                {
                    ID = tempData.ID,
                    Theme = tempData.Theme,
                    Cake = tempData.Cake,
                    Service = tempData.Service,
                    Classfy = tempData.Classfy,
                    Subject = tempData.Subject,
                    Content = tempData.Content.Replace("<br>", "\n"),
                    SubmitName = tempData.SubmitName,
                    SubmitDate = tempData.SubmitDate,
                    ModifyName = tempData.ModifyName,
                    ModifyDate = tempData.ModifyDate,
                    Hit = tempData.hit,
                    CreatedDeptID = tempData.created_dept_id,
                    CreatedUser = tempData.created_user_name,
                    CreatedUserPhone = tempData.created_user_phone
                };
            }
            return result;
        }

        public bool QaSave(QaModel saveData)
        {
            int resultRow = 0;
            try
            {
                if (saveData.ID > 0)
                {
                    var originalData = _qaRepository.Get(q => q.ID == saveData.ID);
                    originalData.Theme = saveData.Theme;
                    originalData.Cake = saveData.Cake;
                    originalData.Service = saveData.Service;
                    originalData.Classfy = saveData.Classfy;
                    originalData.Subject = saveData.Subject;
                    originalData.Content = saveData.Content;
                    originalData.SubmitName = saveData.SubmitName;
                    originalData.SubmitDate = saveData.SubmitDate;
                    originalData.ModifyName = saveData.ModifyName;
                    originalData.ModifyDate = saveData.ModifyDate;
                    originalData.created_dept_id = saveData.CreatedDeptID;
                    originalData.created_user_name = saveData.CreatedUser;
                    originalData.created_user_phone = saveData.CreatedUserPhone;
                    resultRow = _qaRepository.Update(originalData);
                }
                else
                {
                    qa newData = new qa()
                    {
                        Theme = saveData.Theme,
                        Cake = saveData.Cake,
                        Service = saveData.Service,
                        Classfy = saveData.Classfy,
                        Subject = saveData.Subject,
                        Content = saveData.Content,
                        SubmitName = saveData.SubmitName,
                        SubmitDate = saveData.SubmitDate,
                        ModifyName = saveData.ModifyName,
                        ModifyDate = saveData.ModifyDate,
                        created_dept_id = saveData.CreatedDeptID,
                        created_user_name = saveData.CreatedUser,
                        created_user_phone = saveData.CreatedUserPhone
                    };
                    resultRow = _qaRepository.Create(newData);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public bool QaDelete(int id)
        {
            int result = 0;
            try
            {
                var deleteData = _qaRepository.Get(q => q.ID == id);
                result = _qaRepository.Delete(deleteData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }

        public void Dispose()
        {
            if (_qaRepository != null)
            {
                _qaRepository.Dispose();
            }
        }
    }
}
