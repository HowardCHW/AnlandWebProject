using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface IFAQService : IDisposable
    {
        List<FAQModel> FAQQueryAll();

        FAQModel FAQQueryByNo(int no);

        bool FAQSave(FAQModel saveData);

        bool FAQDelete(int no);
    }
}
