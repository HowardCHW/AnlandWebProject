using AnlandProject.Models;
using AnlandProject.Models.Interface;
using AnlandProject.Models.Repository;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service
{
    public class CommonService : ICommonService
    {
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

        public List<CategoryModel> PostGroupQueryAll()
        {
            var result = _postGroupRepository.GetAll().Select(p => new CategoryModel()
            {
                ID = p.ID,
                ClassName = p.post_group1
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
        }        
    }
}
