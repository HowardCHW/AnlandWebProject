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

        public List<NewsCategoryModel> NewsCategoryQueryAll()
        {
            var result = _newsCategoryRepository.GetAll().Select(n => new NewsCategoryModel()
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

    }
}
