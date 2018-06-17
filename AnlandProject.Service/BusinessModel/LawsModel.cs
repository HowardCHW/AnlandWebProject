using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AnlandProject.Service.BusinessModel
{
    public class LawsModel
    {
        public int ID { get; set; }
        public int? Classfy { get; set; }
        public string ClassfyName { get; set; }
        public string Theme { get; set; }
        public string Cake { get; set; }
        public string Service { get; set; }
        public DateTime? LDate { get; set; }
        public string Superior { get; set; }
        public string LNumber { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string SubmitName { get; set; }
        public DateTime? SubmitDate { get; set; }
        public string ModifyName { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string Attachfile { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
        public string Homepage2 { get; set; }
        public string Homepage3 { get; set; }
        public string Homepage4 { get; set; }
        public string Homepage5 { get; set; }
        public string Homepage6 { get; set; }
        public string File1Momo { get; set; }
        public string File2Momo { get; set; }
        public string File3Momo { get; set; }
        public string File4Momo { get; set; }
        public string File5Momo { get; set; }
        public int? Hit { get; set; }
        public int? CreatedDeptID { get; set; }
        public string CreatedDeptName { get; set; }
        public string CreatedUser { get; set; }
        public string CreatedUserPhone { get; set; }

        public List<HttpPostedFileBase> Files { get; set; }

        public List<GroupFile> GroupFile { get; set; }
    }
}
