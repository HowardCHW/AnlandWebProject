using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface IProposalService : IDisposable
    {
        List<ProposalDataModel> PropoaslQueryAll();

        ProposalDataModel PropoaslQueryByID(int id);

        bool ProposalSave(ProposalDataModel saveData);

        bool ProposalDelete(int id);
    }
}
