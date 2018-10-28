using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface IQaService :  IDisposable
    {
        List<QaModel> QaQueryAll();

        QaModel QaQueryByID(int id, bool isFront = false);

        bool QaSave(QaModel saveData);

        bool QaDelete(int id);
    }
}
