using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface ICategoritesService :  IDisposable
    {
        List<CategoritesDataModel> CategoritesQueryAll();

        CategoritesDataModel CategoritesQueryByID(int id);

        bool CategoritesSave(CategoritesDataModel saveData);

        bool CategoritesDelete(int id);
    }
}
