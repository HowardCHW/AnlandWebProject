﻿using AnlandProject.Models;
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
        List<NewsModel> NewsQueryAll();

        NewsModel NewsQueryByID(int id);

        bool NewsSave(NewsModel saveData);

        bool NewsDelete(int id);
    }
}
