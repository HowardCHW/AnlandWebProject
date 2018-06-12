using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface INewsService
    {
        List<NewsModel> NewsQueryAll();

        NewsModel NewsQueryByID(int id);

        bool NewsSave(NewsModel saveData);
    }
}
