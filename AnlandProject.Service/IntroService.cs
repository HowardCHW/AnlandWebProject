using AnlandProject.Models;
using AnlandProject.Models.Interface;
using AnlandProject.Models.Repository;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnlandProject.Service
{
    public class IntroService : IIntroService
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        private IRepository<intro10> _introRepository = new GenericRepository<intro10>();
        
        public List<DefaultDataModel> IntroQueryAll()
        {
            var result = _introRepository.GetAll().Select(i => new DefaultDataModel()
            {
                ID = i.ID,
                Obj = i.obj,
                Theme = i.Theme,
                Cake = i.Cake,
                Service = i.Service,
                Subject = i.subject,
                Author = i.author,
                HomepageMomo = i.homepage_momo,
                Homepage1 = i.homepage1,
                Homepage2 = i.homepage2,
                Homepage3 = i.homepage3,
                Homepage4 = i.homepage4,
                Homepage5 = i.homepage5,
                Homepage6 = i.homepage6,
                Body = i.Body.Replace("<br>",""),
                PostDate = i.postdate,
                PostTime = i.posttime,
                Hit = i.hit,
                PostName = i.postname,
                EndDate = i.end_date,
                PostGroup = i.post_group,
                PosterRight = i.poster_right,
                File1Momo = i.file1_momo,
                File2Momo = i.file2_momo,
                File3Momo = i.file3_momo,
                File4Momo = i.file4_momo,
                File5Momo = i.file5_momo,
                CreatedDeptID = i.created_dept_id,
                CreatedUser = i.created_user_name,
                CreatedUserPhone = i.created_user_phone
            }).ToList();

            return result;
        }

        public DefaultDataModel IntroQueryByID(int id, bool isFront = false)
        {
            var tempData = _introRepository.Get(i => i.ID == id);

            //紀錄前台使用者點擊次數
            if (isFront)
            {
                tempData.hit += 1;
                _introRepository.Update(tempData);
            }

            DefaultDataModel result = null;
            if (tempData != null)
            {
                result = new DefaultDataModel()
                {
                    ID = tempData.ID,
                    Obj = tempData.obj,
                    Theme = tempData.Theme,
                    Cake = tempData.Cake,
                    Service = tempData.Service,
                    Subject = tempData.subject,
                    Author = tempData.author,
                    HomepageMomo = tempData.homepage_momo,
                    Homepage1 = tempData.homepage1,
                    Homepage2 = tempData.homepage2,
                    Homepage3 = tempData.homepage3,
                    Homepage4 = tempData.homepage4,
                    Homepage5 = tempData.homepage5,
                    Homepage6 = tempData.homepage6,
                    Body = tempData.Body.Replace("<br>",""),
                    PostDate = tempData.postdate,
                    PostTime = tempData.posttime,
                    Hit = tempData.hit,
                    PostName = tempData.postname,
                    EndDate = tempData.end_date,
                    PostGroup = tempData.post_group,
                    PosterRight = tempData.poster_right,
                    File1Momo = tempData.file1_momo,
                    File2Momo = tempData.file2_momo,
                    File3Momo = tempData.file3_momo,
                    File4Momo = tempData.file4_momo,
                    File5Momo = tempData.file5_momo,
                    CreatedDeptID = tempData.created_dept_id,
                    CreatedUser = tempData.created_user_name,
                    CreatedUserPhone = tempData.created_user_phone
                };
            }
            return result;
        }

        public bool IntroSave(DefaultDataModel saveData)
        {
            int resultRow = 0;
            try
            {
                if (saveData.ID > 0)
                {
                    var originalData = _introRepository.Get(i => i.ID == saveData.ID);
                    originalData.obj = saveData.Obj;
                    originalData.Theme = saveData.Theme;
                    originalData.Cake = saveData.Cake; ;
                    originalData.Service = saveData.Service;
                    originalData.subject = saveData.Subject;
                    originalData.author = saveData.Author;
                    originalData.homepage_momo = saveData.HomepageMomo;
                    originalData.homepage1 = saveData.Homepage1;
                    if (!string.IsNullOrWhiteSpace(saveData.Homepage2)) originalData.homepage2 = saveData.Homepage2;
                    if (!string.IsNullOrWhiteSpace(saveData.Homepage3)) originalData.homepage3 = saveData.Homepage3;
                    if (!string.IsNullOrWhiteSpace(saveData.Homepage4)) originalData.homepage4 = saveData.Homepage4;
                    if (!string.IsNullOrWhiteSpace(saveData.Homepage5)) originalData.homepage5 = saveData.Homepage5;
                    if (!string.IsNullOrWhiteSpace(saveData.Homepage6)) originalData.homepage6 = saveData.Homepage6;
                    originalData.Body = saveData.Body;
                    originalData.postdate = DateTime.Now;
                    originalData.posttime = DateTime.Now;
                    //originalData.hit = saveData.Hit;
                    originalData.postname = saveData.PostName;
                    originalData.end_date = saveData.EndDate;
                    originalData.post_group = saveData.PostGroup;
                    originalData.poster_right = saveData.PosterRight;
                    if (!string.IsNullOrWhiteSpace(saveData.File1Momo)) originalData.file1_momo = saveData.File1Momo;
                    if (!string.IsNullOrWhiteSpace(saveData.File2Momo)) originalData.file2_momo = saveData.File2Momo;
                    if (!string.IsNullOrWhiteSpace(saveData.File3Momo)) originalData.file3_momo = saveData.File3Momo;
                    if (!string.IsNullOrWhiteSpace(saveData.File4Momo)) originalData.file4_momo = saveData.File4Momo;
                    if (!string.IsNullOrWhiteSpace(saveData.File5Momo)) originalData.file5_momo = saveData.File5Momo;
                    originalData.created_dept_id = saveData.CreatedDeptID;
                    originalData.created_user_name = saveData.CreatedUser;
                    originalData.created_user_phone = saveData.CreatedUserPhone;
                    resultRow = _introRepository.Update(originalData);
                }
                else
                {
                    intro10 newData = new intro10()
                    {
                        obj = saveData.Obj,
                        Theme = saveData.Theme,
                        Cake = saveData.Cake,
                        Service = saveData.Service,
                        subject = saveData.Subject,
                        author = saveData.Author,
                        homepage_momo = saveData.HomepageMomo,
                        homepage1 = saveData.Homepage1,
                        homepage2 = saveData.Homepage2,
                        homepage3 = saveData.Homepage3,
                        homepage4 = saveData.Homepage4,
                        homepage5 = saveData.Homepage5,
                        homepage6 = saveData.Homepage6,
                        Body = saveData.Body,
                        postdate = saveData.PostDate,
                        posttime = saveData.PostTime,
                        hit = 0,
                        postname = saveData.PostName,
                        end_date = saveData.EndDate,
                        post_group = saveData.PostGroup,
                        poster_right = saveData.PosterRight,
                        file1_momo = saveData.File1Momo,
                        file2_momo = saveData.File2Momo,
                        file3_momo = saveData.File3Momo,
                        file4_momo = saveData.File4Momo,
                        file5_momo = saveData.File5Momo,
                        created_dept_id = saveData.CreatedDeptID,
                        created_user_name = saveData.CreatedUser,
                        created_user_phone = saveData.CreatedUserPhone
                    };
                    resultRow = _introRepository.Create(newData);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public bool IntroDelete(int id)
        {
            int result = 0;
            try
            {
                var deleteData = _introRepository.Get(i => i.ID == id);
                result = _introRepository.Delete(deleteData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }

        public void Dispose()
        {
            if (_introRepository != null)
            {
                _introRepository.Dispose();
            }
        }
    }
}
