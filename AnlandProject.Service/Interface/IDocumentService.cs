using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface IDocumentService : IDisposable
    {
        List<DefaultDataModel> DocumentQueryAll();

        DefaultDataModel DocumentQueryByID(int id);

        bool DocumentSave(DefaultDataModel saveData);

        bool DocumentDelete(int id);
    }
}
