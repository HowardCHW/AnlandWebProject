using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface INewsService : IDisposable
    {
        List<DefaultDataModel> NewsQueryAll();

        DefaultDataModel NewsQueryByID(int id);

        bool NewsSave(DefaultDataModel saveData);

        bool NewsDelete(int id);
    }
}
