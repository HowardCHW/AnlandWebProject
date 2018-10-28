using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface IIntroService : IDisposable
    {
        List<DefaultDataModel> IntroQueryAll();

        DefaultDataModel IntroQueryByID(int id, bool isFront = false);

        bool IntroSave(DefaultDataModel saveData);

        bool IntroDelete(int id);
    }
}
