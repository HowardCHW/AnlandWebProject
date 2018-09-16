using AnlandProject.Models;
using AnlandProject.Models.Interface;
using AnlandProject.Models.Repository;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service
{
    public class ProposalService : IProposalService
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        private IRepository<proposal_data> _proposalRepository = new GenericRepository<proposal_data>();
        
        public List<ProposalDataModel> PropoaslQueryAll()
        {
            var result = _proposalRepository.GetAll().Select(n => new ProposalDataModel()
            {
                ID = n.ID,
                ProposalDate = n.ProposalDate,
                Proposer = n.Proposer,
                ContactPhone = n.ContactPhone,
                ContactEmail = n.ContactEmail,
                ProposalSubject = n.ProposalSubject,
                ProposalContent = n.ProposalContent,
                ProposalBenefit = n.ProposalBenefit
            }).ToList();

            return result;
        }

        public ProposalDataModel PropoaslQueryByID(int id)
        {
            var tempData = _proposalRepository.Get(x => x.ID == id);

            ProposalDataModel result = null;
            if (tempData != null)
            {
                result = new ProposalDataModel()
                {
                    ID = tempData.ID,
                    ProposalDate = tempData.ProposalDate,
                    Proposer = tempData.Proposer,
                    ContactPhone = tempData.ContactPhone,
                    ContactEmail = tempData.ContactEmail,
                    ProposalSubject = tempData.ProposalSubject,
                    ProposalContent = tempData.ProposalContent,
                    ProposalBenefit = tempData.ProposalBenefit
                };
            }
            return result;
        }

        public bool ProposalSave(ProposalDataModel saveData)
        {
            int resultRow = 0;
            try
            {
                proposal_data newData = new proposal_data()
                {
                    ProposalDate = saveData.ProposalDate,
                    Proposer = saveData.Proposer,
                    ContactPhone = saveData.ContactPhone,
                    ContactEmail = saveData.ContactEmail,
                    ProposalSubject = saveData.ProposalSubject,
                    ProposalContent = saveData.ProposalContent,
                    ProposalBenefit = saveData.ProposalContent
                };
                resultRow = _proposalRepository.Create(newData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public bool ProposalDelete(int id)
        {
            int result = 0;
            try
            {
                var deleteData = _proposalRepository.Get(n => n.ID == id);
                result = _proposalRepository.Delete(deleteData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }

        public void Dispose()
        {
            if (_proposalRepository != null)
            {
                _proposalRepository.Dispose();
            }
        }
    }
}
