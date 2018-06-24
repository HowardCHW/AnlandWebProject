﻿using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface IOnlineApplicationService : IDisposable
    {
        List<OnlineApplicationModel> OnlineAppQueryAll();

        OnlineApplicationModel OnlineAppQueryByID(int id);

        bool OnlineAppDelete(int id);

        List<OnlineApplicationModel> EngCopyQueryAll();

        OnlineApplicationModel EngCopyQueryByID(int id);

        bool EngCopyDelete(int id);

        List<OnlineApplicationModel> CopyQueryAll();

        OnlineApplicationModel CopyQueryByID(int id);

        bool CopyDelete(int id);

        List<OnlineApplicationModel> LandPriceQueryAll();

        OnlineApplicationModel LandPriceQueryByID(int id);

        bool LandPriceDelete(int id);
    }
}
