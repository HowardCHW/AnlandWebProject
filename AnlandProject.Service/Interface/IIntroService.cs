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
        List<Intro10Model> IntroQueryAll();

        Intro10Model IntroQueryByID(int id);

        bool IntroSave(Intro10Model saveData);

        bool IntroDelete(int id);
    }
}
