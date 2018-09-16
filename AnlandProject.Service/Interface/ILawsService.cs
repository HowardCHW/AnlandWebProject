using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface ILawsService :  IDisposable
    {
        List<LawsModel> LawsQueryAll();

        LawsModel LawsQueryByID(int id, bool isFront = false);

        bool LawsSave(LawsModel saveData);

        bool LawDelete(int id);
    }
}
