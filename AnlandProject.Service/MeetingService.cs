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
    public class MeetingService : IMeetingService
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        private IRepository<meeting> _meetingRepository = new GenericRepository<meeting>();
        
        public List<DefaultDataModel> MeetingQueryAll()
        {
            var result = _meetingRepository.GetAll().Select(n => new DefaultDataModel()
            {
                ID = n.ID,
                Subject = n.subject,
                Author = n.author,
                Homepage2 = n.homepage2,
                Homepage3 = n.homepage3,
                Homepage4 = n.homepage4,
                Homepage5 = n.homepage5,
                Homepage6 = n.homepage6,
                //Body = n.Body.Replace("<br>",""),
                PostDate = n.postdate,
                Hit = n.hit,
                EndDate = n.end_date,
                File1Momo = n.file1_momo,
                File2Momo = n.file2_momo,
                File3Momo = n.file3_momo,
                File4Momo = n.file4_momo,
                File5Momo = n.file5_momo
            }).ToList();

            return result;
        }

        public DefaultDataModel MeetingQueryByID(int id)
        {
            var tempData = _meetingRepository.Get(x => x.ID == id);

            DefaultDataModel result = null;
            if (tempData != null)
            {
                result = new DefaultDataModel()
                {
                    ID = tempData.ID,
                    Subject = tempData.subject,
                    Author = tempData.author,
                    Homepage2 = tempData.homepage2,
                    Homepage3 = tempData.homepage3,
                    Homepage4 = tempData.homepage4,
                    Homepage5 = tempData.homepage5,
                    Homepage6 = tempData.homepage6,
                    Body = tempData.Body.Replace("<br>",""),
                    PostDate = tempData.postdate,
                    Hit = tempData.hit,
                    EndDate = tempData.end_date,
                    File1Momo = tempData.file1_momo,
                    File2Momo = tempData.file2_momo,
                    File3Momo = tempData.file3_momo,
                    File4Momo = tempData.file4_momo,
                    File5Momo = tempData.file5_momo
                };
            }
            return result;
        }

        public bool MeetingSave(DefaultDataModel saveData)
        {
            int resultRow = 0;
            try
            {
                if (saveData.ID > 0)
                {
                    var originalData = _meetingRepository.Get(n => n.ID == saveData.ID);
                    originalData.subject = saveData.Subject;
                    originalData.author = saveData.Author;
                    originalData.homepage2 = saveData.Homepage2;
                    originalData.homepage3 = saveData.Homepage3;
                    originalData.homepage4 = saveData.Homepage4;
                    originalData.homepage5 = saveData.Homepage5;
                    originalData.homepage6 = saveData.Homepage6;
                    originalData.Body = saveData.Body;
                    originalData.postdate = saveData.PostDate;
                    originalData.hit = saveData.Hit;
                    originalData.end_date = saveData.EndDate;
                    originalData.file1_momo = saveData.File1Momo;
                    originalData.file2_momo = saveData.File2Momo;
                    originalData.file3_momo = saveData.File3Momo;
                    originalData.file4_momo = saveData.File4Momo;
                    originalData.file5_momo = saveData.File5Momo;
                    resultRow = _meetingRepository.Update(originalData);
                }
                else
                {
                    meeting newData = new meeting()
                    {
                        subject = saveData.Subject,
                        author = saveData.Author,
                        homepage2 = saveData.Homepage2,
                        homepage3 = saveData.Homepage3,
                        homepage4 = saveData.Homepage4,
                        homepage5 = saveData.Homepage5,
                        homepage6 = saveData.Homepage6,
                        Body = saveData.Body,
                        postdate = saveData.PostDate,
                        hit = saveData.Hit,
                        end_date = saveData.EndDate,
                        file1_momo = saveData.File1Momo,
                        file2_momo = saveData.File2Momo,
                        file3_momo = saveData.File3Momo,
                        file4_momo = saveData.File4Momo,
                        file5_momo = saveData.File5Momo,
                        created_dept_id = saveData.CreatedDeptID,
                        created_user_name = saveData.CreatedUser,
                        created_date = DateTime.Now
                    };
                    resultRow = _meetingRepository.Create(newData);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public bool MeetingDelete(int id)
        {
            int result = 0;
            try
            {
                var deleteData = _meetingRepository.Get(n => n.ID == id);
                result = _meetingRepository.Delete(deleteData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }

        public void Dispose()
        {
            if (_meetingRepository != null)
            {
                _meetingRepository.Dispose();
            }
        }
    }
}
