using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class QuestionnaireDataModel
    {
        public int QuestionID { get; set; }
        public string QuestionName { get; set; }
        public string Owner { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? StopDay { get; set; }
        public int SelectQty { get; set; }
        public int AnswerQty { get; set; }
        public string QuestionStatus { get; set; }
        public bool IsOpen { get; set; }
        public string Content { get; set; }
        public bool CheckOK { get; set; }
        public string Who { get; set; }
        public DateTime CreateDate { get; set; }
        public List<QuestionDataModel> Questions { get; set; }
        public int TotalReceivedAnswers { get; set; }
        public List<AnswerDataModel> Answers { get; set; }
    }
}
