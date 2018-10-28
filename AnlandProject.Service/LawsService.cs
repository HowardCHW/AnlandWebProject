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
    public class LawsService : ILawsService
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        private IRepository<landlaw> _lawsRepository = new GenericRepository<landlaw>();
        
        public List<LawsModel> LawsQueryAll()
        {
            var result = _lawsRepository.GetAll().Select(l => new LawsModel()
            {
                ID = l.ID,
                Classfy = l.Classfy,
                Theme = l.Theme,
                Cake = l.Cake,
                Service = l.Service,
                LDate = l.lDate,
                Superior = l.Superior,
                LNumber = l.lNumber,
                Subject = l.Subject,
                Content = l.Content.Replace("<br>", "\n"),
                SubmitName = l.SubmitName,
                SubmitDate = l.SubmitDate,
                ModifyName = l.ModifyName,
                ModifyDate = l.ModifyDate,
                Attachfile = l.Attachfile,
                Image = l.Image,
                Active = l.Active,
                Homepage2 = l.homepage2,
                Homepage3 = l.homepage3,
                Homepage4 = l.homepage4,
                Homepage5 = l.homepage5,
                Homepage6 = l.homepage6,
                File1Momo = l.file1_momo,
                File2Momo = l.file2_momo,
                File3Momo = l.file3_momo,
                File4Momo = l.file4_momo,
                File5Momo = l.file5_momo,
                Hit = l.hit,
                CreatedDeptID = l.created_dept_id,
                CreatedUser = l.created_user_name,
                CreatedUserPhone = l.created_user_phone
            }).ToList();

            return result;
        }

        public LawsModel LawsQueryByID(int id, bool isFront = false)
        {
            var tempData = _lawsRepository.Get(x => x.ID == id);

            //紀錄前台使用者點擊次數
            if (isFront)
            {
                tempData.hit += 1;
                _lawsRepository.Update(tempData);
            }

            LawsModel result = null;
            if (tempData != null)
            {
                result = new LawsModel()
                {
                    ID = tempData.ID,
                    Classfy = tempData.Classfy,
                    Theme = tempData.Theme,
                    Cake = tempData.Cake,
                    Service = tempData.Service,
                    LDate = tempData.lDate,
                    Superior = tempData.Superior,
                    LNumber = tempData.lNumber,
                    Subject = tempData.Subject,
                    Content = tempData.Content.Replace("<br>","\n"),
                    SubmitName = tempData.SubmitName,
                    SubmitDate = tempData.SubmitDate,
                    ModifyName = tempData.ModifyName,
                    ModifyDate = tempData.ModifyDate,
                    Attachfile = tempData.Attachfile,
                    Image = tempData.Image,
                    Active = tempData.Active,
                    Homepage2 = tempData.homepage2,
                    Homepage3 = tempData.homepage3,
                    Homepage4 = tempData.homepage4,
                    Homepage5 = tempData.homepage5,
                    Homepage6 = tempData.homepage6,
                    File1Momo = tempData.file1_momo,
                    File2Momo = tempData.file2_momo,
                    File3Momo = tempData.file3_momo,
                    File4Momo = tempData.file4_momo,
                    File5Momo = tempData.file5_momo,
                    Hit = tempData.hit,
                    CreatedDeptID = tempData.created_dept_id,
                    CreatedUser = tempData.created_user_name,
                    CreatedUserPhone = tempData.created_user_phone
                };
            }
            return result;
        }

        public bool LawsSave(LawsModel saveData)
        {
            int resultRow = 0;
            try
            {
                if (saveData.ID > 0)
                {
                    var originalData = _lawsRepository.Get(n => n.ID == saveData.ID);
                    originalData.Classfy = saveData.Classfy;
                    originalData.Theme = saveData.Theme;
                    originalData.Cake = saveData.Cake;
                    originalData.Service = saveData.Service;
                    originalData.lDate = saveData.LDate;
                    originalData.Superior = saveData.Superior;
                    originalData.lNumber = saveData.LNumber;
                    originalData.Subject = saveData.Subject;
                    originalData.Content = saveData.Content;
                    originalData.SubmitName = saveData.SubmitName;
                    originalData.SubmitDate = saveData.SubmitDate;
                    originalData.ModifyName = saveData.ModifyName;
                    originalData.ModifyDate = saveData.ModifyDate;
                    originalData.Attachfile = saveData.Attachfile;
                    originalData.Image = saveData.Image;
                    originalData.Active = saveData.Active;
                    originalData.homepage2 = saveData.Homepage2;
                    originalData.homepage3 = saveData.Homepage3;
                    originalData.homepage4 = saveData.Homepage4;
                    originalData.homepage5 = saveData.Homepage5;
                    originalData.homepage6 = saveData.Homepage6;
                    originalData.file1_momo = saveData.File1Momo;
                    originalData.file2_momo = saveData.File2Momo;
                    originalData.file3_momo = saveData.File3Momo;
                    originalData.file4_momo = saveData.File4Momo;
                    originalData.file5_momo = saveData.File5Momo;
                    originalData.hit = saveData.Hit;
                    originalData.created_dept_id = saveData.CreatedDeptID;
                    originalData.created_user_name = saveData.CreatedUser;
                    originalData.created_user_phone = saveData.CreatedUserPhone;
                    resultRow = _lawsRepository.Update(originalData);
                }
                else
                {
                    landlaw newData = new landlaw()
                    {
                        Classfy = saveData.Classfy,
                        Theme = saveData.Theme,
                        Cake = saveData.Cake,
                        Service = saveData.Service,
                        lDate = saveData.LDate,
                        Superior = saveData.Superior,
                        lNumber = saveData.LNumber,
                        Subject = saveData.Subject,
                        Content = saveData.Content,
                        SubmitName = saveData.SubmitName,
                        SubmitDate = saveData.SubmitDate,
                        ModifyName = saveData.ModifyName,
                        ModifyDate = saveData.ModifyDate,
                        Attachfile = saveData.Attachfile,
                        Image = saveData.Image,
                        Active = saveData.Active,
                        homepage2 = saveData.Homepage2,
                        homepage3 = saveData.Homepage3,
                        homepage4 = saveData.Homepage4,
                        homepage5 = saveData.Homepage5,
                        homepage6 = saveData.Homepage6,
                        file1_momo = saveData.File1Momo,
                        file2_momo = saveData.File2Momo,
                        file3_momo = saveData.File3Momo,
                        file4_momo = saveData.File4Momo,
                        file5_momo = saveData.File5Momo,
                        hit = 0,
                        created_dept_id = saveData.CreatedDeptID,
                        created_user_name = saveData.CreatedUser,
                        created_user_phone = saveData.CreatedUserPhone
                    };
                    resultRow = _lawsRepository.Create(newData);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public bool LawDelete(int id)
        {
            int result = 0;
            try
            {
                var deleteData = _lawsRepository.Get(l => l.ID == id);
                result = _lawsRepository.Delete(deleteData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }

        public void Dispose()
        {
            if (_lawsRepository != null)
            {
                _lawsRepository.Dispose();
            }
        }
    }
}
