using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface IDirectorFAQService : IDisposable
    {
        List<FAQModel> DirectorFAQQueryAll();

        FAQModel DirectorFAQQueryByNo(int no);

        bool DirectorFAQSave(FAQModel saveData);

        bool DirectorFAQDelete(int no);
    }
}
