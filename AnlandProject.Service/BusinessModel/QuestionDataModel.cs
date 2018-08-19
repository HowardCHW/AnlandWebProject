using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class QuestionDataModel
    {
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public string QuestionNo { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        public List<string> Options { get; set; }
    }
}
