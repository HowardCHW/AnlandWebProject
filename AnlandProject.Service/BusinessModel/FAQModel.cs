using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class FAQModel
    {
        public int No { get; set; }
        public DateTime MsgDate { get; set; }
        public string MsgName { get; set; }
        public string MsgEmail { get; set; }
        public string MsgSubject { get; set; }
        public string MsgContent { get; set; }
        public string RpyUnit { get; set; }
        public DateTime RpyDate { get; set; }
        public string RpyName { get; set; }
        public string RpyTel { get; set; }
        public string RpyContent { get; set; }
        public string Attach1 { get; set; }
        public string Attach2 { get; set; }
        public string Attach3 { get; set; }
        public string MsgTel { get; set; }
        public string MsgType { get; set; }

    }
}
