using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class QuestionnaireAnswerModel
    {
        public int QuestionID { get; set; }
        public string SenderIP { get; set; }
        public DateTime FillInDate { get; set; }
        public List<string> SingleOpt { get; set; }
        public List<List<string>> MultipleOpt { get; set; }
        public List<string> QandA { get; set; }
        public string AllAnswers { get; set; }
    }
}
