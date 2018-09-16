using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class AnswerDataModel
    {
        public string QuestionNo { get; set; }
        public string Question { get; set; }
        public string QuestionType { get; set; }
        public List<OptionAnswerDataModel> AllAnswers { get; set; }
    }

    public class OptionAnswerDataModel
    {
        public string Option { get; set; }
        public int AnswerCount { get; set; }
        public string AnswerPercent { get; set; }
    }
}
