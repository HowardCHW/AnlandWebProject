using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class ProposalDataModel
    {
        public int ID { get; set; }
        public DateTime ProposalDate { get; set; }
        public string Proposer { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string ProposalSubject { get; set; }
        public string ProposalContent { get; set; }
        public string ProposalBenefit { get; set; }
    }
}
