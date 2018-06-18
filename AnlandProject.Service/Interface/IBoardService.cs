using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface IBoardService : IDisposable
    {
        List<DefaultDataModel> BoardQueryAll();

        DefaultDataModel BoardQueryByID(int id);

        bool BoardSave(DefaultDataModel saveData);

        bool BoardDelete(int id);
    }
}
