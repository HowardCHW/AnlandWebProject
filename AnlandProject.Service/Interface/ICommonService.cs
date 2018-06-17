using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface ICommonService : IDisposable
    {
        List<ClassificationModel> ThemeQueryAll();

        List<ClassificationModel> CakeQueryAll();

        List<ClassificationModel> ServiceQueryAll();

        List<CategoryModel> NewsCategoryQueryAll();

        List<DepartmentModel> DeptQueryAll();

        List<CategoryModel> LawsCategoryQueryAll();

        List<CategoryModel> PostGroupQueryAll();

    }
}
