using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface ICommonService
    {
        List<ClassificationModel> ThemeQueryAll();

        List<ClassificationModel> CakeQueryAll();

        List<ClassificationModel> ServiceQueryAll();

        List<NewsCategoryModel> NewsCategoryQueryAll();

    }
}
