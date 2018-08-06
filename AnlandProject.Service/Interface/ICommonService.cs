using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AnlandProject.Service.BusinessModel.ClassificationTypeEnum;

namespace AnlandProject.Service.Interface
{
    public interface ICommonService : IDisposable
    {
        #region Query
        List<ClassificationModel> ThemeQueryAll();

        List<ClassificationModel> CakeQueryAll();

        List<ClassificationModel> ServiceQueryAll();

        List<CategoryModel> NewsCategoryQueryAll();

        List<DepartmentModel> DeptQueryAll();

        List<CategoryModel> LawsCategoryQueryAll();

        List<CategoryModel> PostGroupQueryAll();

        List<CategoryModel> QaCategoryQueryAll();

        List<CategoryModel> BoardCategoryQueryAll();

        List<CategoryModel> MeetingCategoryQueryAll();

        List<CategoryModel> DocumentCategoryQueryAll();
        #endregion

        #region Save / Update
        bool ClassificationSearchSave(ClassificationModel saveData, ClassificationSearch type);

        bool ClassificationSave(CategoryModel saveData, Classification type);

        bool PostgroupSave(CategoryModel saveData);
        #endregion

        #region Delete
        bool ClassificationSearchDelete(int id, ClassificationSearch type);

        bool ClassificationDelete(int id, Classification type);

        bool PostgroupDelete(int id);
        #endregion

    }
}
