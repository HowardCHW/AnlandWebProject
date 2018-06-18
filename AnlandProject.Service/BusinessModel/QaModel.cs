using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AnlandProject.Service.BusinessModel
{
    public class QaModel
    {
        public int ID { get; set; }
        public string Theme { get; set; }
        public string Cake { get; set; }
        public string Service { get; set; }
        public int? Classfy { get; set; }
        public string ClassfyName { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string SubmitName { get; set; }
        public DateTime? SubmitDate { get; set; }
        public string ModifyName { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? Hit { get; set; }
        public int? CreatedDeptID { get; set; }
        public string CreatedDeptName { get; set; }
        public string CreatedUser { get; set; }
        public string CreatedUserPhone { get; set; }
    }
}
