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
using static AnlandProject.Service.BusinessModel.ClassificationTypeEnum;

namespace AnlandProject.Service
{
    public class CommonService : ICommonService
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        private IRepository<theme> _themeRepository = new GenericRepository<theme>();
        private IRepository<cake> _cakeRepository = new GenericRepository<cake>();
        private IRepository<service> _serviceRepository = new GenericRepository<service>();
        private IRepository<newsclass> _newsCategoryRepository = new GenericRepository<newsclass>();
        private IRepository<Department> _departmentRepository = new GenericRepository<Department>();
        private IRepository<lawclass> _lawCategoryRepository = new GenericRepository<lawclass>();
        private IRepository<post_group> _postGroupRepository = new GenericRepository<post_group>();
        private IRepository<qaclass> _qaCategoryRepository = new GenericRepository<qaclass>();
        private IRepository<boardclass> _boardCategoryRepository = new GenericRepository<boardclass>();
        private IRepository<meetingclass> _meetingCategoryRepository = new GenericRepository<meetingclass>();
        private IRepository<documentclass> _documentCategoryRepository = new GenericRepository<documentclass>();
        private IRepository<website_visitor> _websiteVisitorsRepository = new GenericRepository<website_visitor>();

        #region Query
        public List<ClassificationModel> ThemeQueryAll()
        {
            var result = _themeRepository.GetAll().Select(t => new ClassificationModel()
            {
                ID = t.id,
                SNo = t.sno,
                CreUser = t.cre_user,
                TypeCode = t.typecode,
                TypeName = t.typename,
                CreDate = t.cre_date
            }).ToList();

            return result;
        }

        public List<ClassificationModel> CakeQueryAll()
        {
            var result = _cakeRepository.GetAll().Select(c => new ClassificationModel()
            {
                ID = c.id,
                SNo = c.sno,
                CreUser = c.cre_user,
                TypeCode = c.typecode,
                TypeName = c.typename,
                CreDate = c.cre_date
            }).ToList();

            return result;
        }

        public List<ClassificationModel> ServiceQueryAll()
        {
            var result = _serviceRepository.GetAll().Select(s => new ClassificationModel()
            {
                ID = s.id,
                SNo = s.sno,
                CreUser = s.cre_user,
                TypeCode = s.typecode,
                TypeName = s.typename,
                CreDate = s.cre_date
            }).ToList();

            return result;
        }

        public List<CategoryModel> NewsCategoryQueryAll()
        {
            var result = _newsCategoryRepository.GetAll().Select(n => new CategoryModel()
            {
                ID = n.id,
                ClassID = n.classid,
                ClassName = n.classname,
                CreUser = n.creuser,
                CreDate = n.credate
            }).ToList();

            return result;
        }

        public List<CategoryModel> LawsCategoryQueryAll()
        {
            var result = _lawCategoryRepository.GetAll().Select(l => new CategoryModel()
            {
                ID = l.ID,
                ClassID = l.classid,
                ClassName = l.classname,
                CreUser = l.creuser,
                CreDate = l.credate
            }).ToList();

            return result;
        }

        public List<CategoryModel> QaCategoryQueryAll()
        {
            var result = _qaCategoryRepository.GetAll().Select(l => new CategoryModel()
            {
                ID = l.ID,
                ClassID = l.classid,
                ClassName = l.classname,
                CreUser = l.creuser,
                CreDate = l.credate
            }).ToList();

            return result;
        }

        public List<CategoryModel> BoardCategoryQueryAll()
        {
            var result = _boardCategoryRepository.GetAll().Select(l => new CategoryModel()
            {
                ID = l.ID,
                ClassID = l.classid,
                ClassName = l.classname,
                CreUser = l.creuser,
                CreDate = l.credate
            }).ToList();

            return result;
        }

        public List<CategoryModel> MeetingCategoryQueryAll()
        {
            var result = _meetingCategoryRepository.GetAll().Select(l => new CategoryModel()
            {
                ID = l.ID,
                ClassID = l.classid,
                ClassName = l.classname,
                CreUser = l.creuser,
                CreDate = l.credate
            }).ToList();

            return result;
        }

        public List<CategoryModel> DocumentCategoryQueryAll()
        {
            var result = _documentCategoryRepository.GetAll().Select(l => new CategoryModel()
            {
                ID = l.ID,
                ClassID = l.classid,
                ClassName = l.classname,
                CreUser = l.creuser,
                CreDate = l.credate
            }).ToList();

            return result;
        }
        
        public List<CategoryModel> PostGroupQueryAll()
        {
            var result = _postGroupRepository.GetAll().Select(p => new CategoryModel()
            {
                ID = p.ID,
                ClassName = p.post_group1,
                ClassID = p.post_team_num
            }).ToList();

            return result;
        }

        public List<DepartmentModel> DeptQueryAll()
        {
            var result = _departmentRepository.GetAll().Select(d => new DepartmentModel()
            {
                ID = d.id,
                DeptName = d.dep_name,
                PhoneNo = d.phone_no,
                ExtNo = d.ext_no
            }).ToList();

            return result;
        }

        public int VisitorsQuery()
        {
            var result = _websiteVisitorsRepository.GetAll().FirstOrDefault().Visitors;
            return result;
        }
        #endregion

        #region Save / Update
        public bool ClassificationSearchSave(ClassificationModel saveData, ClassificationSearch type)
        {
            int resultRow = 0;
            try
            {
                if (saveData.ID > 0)
                {
                    switch (type)
                    {
                        case ClassificationSearch.Theme:
                            var origThemeData = _themeRepository.Get(n => n.id == saveData.ID);
                            origThemeData.sno = saveData.SNo;
                            origThemeData.cre_user = saveData.CreUser;
                            origThemeData.typecode = saveData.TypeCode;
                            origThemeData.typename = saveData.TypeName;
                            origThemeData.cre_date = saveData.CreDate;
                            resultRow = _themeRepository.Update(origThemeData);
                            break;
                        case ClassificationSearch.Cake:
                            var origCakeData = _cakeRepository.Get(n => n.id == saveData.ID);
                            origCakeData.sno = saveData.SNo;
                            origCakeData.cre_user = saveData.CreUser;
                            origCakeData.typecode = saveData.TypeCode;
                            origCakeData.typename = saveData.TypeName;
                            origCakeData.cre_date = saveData.CreDate;
                            resultRow = _cakeRepository.Update(origCakeData);
                            break;
                        case ClassificationSearch.Service:
                            var origiServiceData = _serviceRepository.Get(n => n.id == saveData.ID);
                            origiServiceData.sno = saveData.SNo;
                            origiServiceData.cre_user = saveData.CreUser;
                            origiServiceData.typecode = saveData.TypeCode;
                            origiServiceData.typename = saveData.TypeName;
                            origiServiceData.cre_date = saveData.CreDate;
                            resultRow = _serviceRepository.Update(origiServiceData);
                            break;
                        default:
                            break;
                    }                    
                }
                else
                {
                    switch (type)
                    {
                        case ClassificationSearch.Theme:
                            theme newThemeData = new theme()
                            {
                                sno = saveData.SNo,
                                cre_user = saveData.CreUser,
                                typecode = saveData.TypeCode,
                                typename = saveData.TypeName,
                                cre_date = saveData.CreDate
                            };
                            resultRow = _themeRepository.Create(newThemeData);
                            break;
                        case ClassificationSearch.Cake:
                            cake newCakeData = new cake()
                            {
                                sno = saveData.SNo,
                                cre_user = saveData.CreUser,
                                typecode = saveData.TypeCode,
                                typename = saveData.TypeName,
                                cre_date = saveData.CreDate
                            };
                            resultRow = _cakeRepository.Create(newCakeData);
                            break;
                        case ClassificationSearch.Service:
                            service newServiceData = new service()
                            {
                                sno = saveData.SNo,
                                cre_user = saveData.CreUser,
                                typecode = saveData.TypeCode,
                                typename = saveData.TypeName,
                                cre_date = saveData.CreDate
                            };
                            resultRow = _serviceRepository.Create(newServiceData);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public bool ClassificationSave(CategoryModel saveData, Classification type)
        {
            int resultRow = 0;
            try
            {
                if (saveData.ID > 0)
                {
                    switch (type)
                    {
                        case Classification.NewsClass:
                            var origNewsData = _newsCategoryRepository.Get(n => n.id == saveData.ID);
                            origNewsData.classid = saveData.ClassID;
                            origNewsData.classname = saveData.ClassName;
                            origNewsData.creuser = saveData.CreUser;
                            origNewsData.credate = saveData.CreDate;
                            resultRow = _newsCategoryRepository.Update(origNewsData);
                            break;
                        case Classification.LawClass:
                            var origLawData = _lawCategoryRepository.Get(n => n.ID == saveData.ID);
                            origLawData.classid = saveData.ClassID.Value;
                            origLawData.classname = saveData.ClassName;
                            origLawData.creuser = saveData.CreUser;
                            origLawData.credate = saveData.CreDate;
                            resultRow = _lawCategoryRepository.Update(origLawData);
                            break;
                        case Classification.QaClass:
                            var origQaData = _qaCategoryRepository.Get(n => n.ID == saveData.ID);
                            origQaData.classid = saveData.ClassID;
                            origQaData.classname = saveData.ClassName;
                            origQaData.creuser = saveData.CreUser;
                            origQaData.credate = saveData.CreDate;
                            resultRow = _qaCategoryRepository.Update(origQaData);
                            break;
                        case Classification.BoardClass:
                            var origBoardData = _boardCategoryRepository.Get(n => n.ID == saveData.ID);
                            origBoardData.classid = saveData.ClassID;
                            origBoardData.classname = saveData.ClassName;
                            origBoardData.creuser = saveData.CreUser;
                            origBoardData.credate = saveData.CreDate;
                            resultRow = _boardCategoryRepository.Update(origBoardData);
                            break;
                        case Classification.MeetingClass:
                            var origMeetingData = _meetingCategoryRepository.Get(n => n.ID == saveData.ID);
                            origMeetingData.classid = saveData.ClassID;
                            origMeetingData.classname = saveData.ClassName;
                            origMeetingData.creuser = saveData.CreUser;
                            origMeetingData.credate = saveData.CreDate;
                            resultRow = _meetingCategoryRepository.Update(origMeetingData);
                            break;
                        case Classification.DocumentClass:
                            var origDocuData = _documentCategoryRepository.Get(n => n.ID == saveData.ID);
                            origDocuData.classid = saveData.ClassID;
                            origDocuData.classname = saveData.ClassName;
                            origDocuData.creuser = saveData.CreUser;
                            origDocuData.credate = saveData.CreDate;
                            resultRow = _documentCategoryRepository.Update(origDocuData);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (type)
                    {
                        case Classification.NewsClass:
                            newsclass newNewsData = new newsclass()
                            {
                                classid = saveData.ClassID,
                                classname = saveData.ClassName,
                                creuser = saveData.CreUser,
                                credate = saveData.CreDate
                            };
                            resultRow = _newsCategoryRepository.Create(newNewsData);
                            break;
                        case Classification.LawClass:
                            lawclass newLawData = new lawclass()
                            {
                                classid = saveData.ClassID.Value,
                                classname = saveData.ClassName,
                                creuser = saveData.CreUser,
                                credate = saveData.CreDate
                            };
                            resultRow = _lawCategoryRepository.Create(newLawData);
                            break;
                        case Classification.QaClass:
                            qaclass newQaData = new qaclass()
                            {
                                classid = saveData.ClassID,
                                classname = saveData.ClassName,
                                creuser = saveData.CreUser,
                                credate = saveData.CreDate
                            };
                            resultRow = _qaCategoryRepository.Create(newQaData);
                            break;
                        case Classification.BoardClass:
                            boardclass newBoardData = new boardclass()
                            {
                                classid = saveData.ClassID,
                                classname = saveData.ClassName,
                                creuser = saveData.CreUser,
                                credate = saveData.CreDate
                            };
                            resultRow = _boardCategoryRepository.Create(newBoardData);
                            break;
                        case Classification.MeetingClass:
                            meetingclass newMeetingData = new meetingclass()
                            {
                                classid = saveData.ClassID,
                                classname = saveData.ClassName,
                                creuser = saveData.CreUser,
                                credate = saveData.CreDate
                            };
                            resultRow = _meetingCategoryRepository.Create(newMeetingData);
                            break;
                        case Classification.DocumentClass:
                            documentclass newDocumentData = new documentclass()
                            {
                                classid = saveData.ClassID,
                                classname = saveData.ClassName,
                                creuser = saveData.CreUser,
                                credate = saveData.CreDate
                            };
                            resultRow = _documentCategoryRepository.Create(newDocumentData);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }        

        public bool PostgroupSave(CategoryModel saveData)
        {
            int resultRow = 0;
            try
            {
                if (saveData.ID > 0)
                {
                    var originalData = _postGroupRepository.Get(n => n.ID == saveData.ID);
                    originalData.post_group1 = saveData.ClassName;
                    originalData.post_team_num = saveData.ClassID;
                    resultRow = _postGroupRepository.Update(originalData);
                }
                else
                {
                    post_group newData = new post_group()
                    {
                        post_group1 = saveData.ClassName,
                        post_team_num = saveData.ClassID
                    };
                    resultRow = _postGroupRepository.Create(newData);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public void VisitorsUpdate()
        {
            var originalData = _websiteVisitorsRepository.Get(v => v.ID == 1);
            originalData.Visitors = originalData.Visitors + 1;
            _websiteVisitorsRepository.Update(originalData);
        }
        #endregion

        #region Delete
        public bool ClassificationSearchDelete(int id, ClassificationSearch type)
        {
            int result = 0;
            try
            {
                switch (type)
                {
                    case ClassificationSearch.Theme:
                        var deleteThemeData = _themeRepository.Get(l => l.id == id);
                        result = _themeRepository.Delete(deleteThemeData);
                        break;
                    case ClassificationSearch.Cake:
                        var deleteCakeData = _cakeRepository.Get(l => l.id == id);
                        result = _cakeRepository.Delete(deleteCakeData);
                        break;
                    case ClassificationSearch.Service:
                        var deleteServiceData = _serviceRepository.Get(l => l.id == id);
                        result = _serviceRepository.Delete(deleteServiceData);
                        break;
                    default:
                        break;
                }                
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }

        public bool ClassificationDelete(int id, Classification type)
        {
            int result = 0;
            try
            {
                switch (type)
                {
                    case Classification.NewsClass:
                        var deleteNewsData = _newsCategoryRepository.Get(l => l.id == id);
                        result = _newsCategoryRepository.Delete(deleteNewsData);
                        break;
                    case Classification.LawClass:
                        var deleteLawData = _lawCategoryRepository.Get(l => l.ID == id);
                        result = _lawCategoryRepository.Delete(deleteLawData);
                        break;
                    case Classification.QaClass:
                        var deleteQaData = _qaCategoryRepository.Get(l => l.ID == id);
                        result = _qaCategoryRepository.Delete(deleteQaData);
                        break;
                    case Classification.BoardClass:
                        var deleteBoardData = _boardCategoryRepository.Get(l => l.ID == id);
                        result = _boardCategoryRepository.Delete(deleteBoardData);
                        break;
                    case Classification.MeetingClass:
                        var deleteMeetingData = _meetingCategoryRepository.Get(l => l.ID == id);
                        result = _meetingCategoryRepository.Delete(deleteMeetingData);
                        break;
                    case Classification.DocumentClass:
                        var deleteDocuData = _documentCategoryRepository.Get(l => l.ID == id);
                        result = _documentCategoryRepository.Delete(deleteDocuData);
                        break;
                    default:
                        break;
                }                
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }

        public bool PostgroupDelete(int id)
        {
            int result = 0;
            try
            {
                var deletePGData = _postGroupRepository.Get(l => l.ID == id);
                result = _postGroupRepository.Delete(deletePGData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }
        #endregion

        public void Dispose()
        {
            if (_themeRepository != null)
            {
                _themeRepository.Dispose();
            }
            if (_cakeRepository != null)
            {
                _cakeRepository.Dispose();
            }
            if (_serviceRepository != null)
            {
                _serviceRepository.Dispose();
            }
            if (_newsCategoryRepository != null)
            {
                _newsCategoryRepository.Dispose();
            }
            if (_departmentRepository != null)
            {
                _departmentRepository.Dispose();
            }
            if (_lawCategoryRepository != null)
            {
                _lawCategoryRepository.Dispose();
            }
            if (_postGroupRepository != null)
            {
                _postGroupRepository.Dispose();
            }
            if (_qaCategoryRepository != null)
            {
                _qaCategoryRepository.Dispose();
            }
            if (_boardCategoryRepository != null)
            {
                _boardCategoryRepository.Dispose();
            }
            if (_meetingCategoryRepository != null)
            {
                _meetingCategoryRepository.Dispose();
            }
            if (_documentCategoryRepository != null)
            {
                _documentCategoryRepository.Dispose();
            }
            if (_websiteVisitorsRepository != null)
            {
                _websiteVisitorsRepository.Dispose();
            }            
        }        
    }
}
