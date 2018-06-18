using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface IMeetingService : IDisposable
    {
        List<DefaultDataModel> MeetingQueryAll();

        DefaultDataModel MeetingQueryByID(int id);

        bool MeetingSave(DefaultDataModel saveData);

        bool MeetingDelete(int id);
    }
}
