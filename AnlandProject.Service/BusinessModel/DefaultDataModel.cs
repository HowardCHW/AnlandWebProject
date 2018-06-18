using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AnlandProject.Service.BusinessModel
{
    public class DefaultDataModel
    {
        public int ID { get; set; }
        public string Obj { get; set; }
        public string Theme { get; set; }
        public string ThemeName { get; set; }
        public string Cake { get; set; }
        public string CakeName { get; set; }
        public string Service { get; set; }
        public string ServiceName { get; set; }
        public string Subject { get; set; }
        public string Author { get; set; }
        public string HomepageMomo { get; set; }
        public string Homepage1 { get; set; }
        public string Homepage2 { get; set; }
        public string Homepage3 { get; set; }
        public string Homepage4 { get; set; }
        public string Homepage5 { get; set; }
        public string Homepage6 { get; set; }
        public string Body { get; set; }
        public DateTime? PostDate { get; set; }
        public DateTime? PostTime { get; set; }
        public int? Hit { get; set; }
        public string PostName { get; set; }
        public DateTime? EndDate { get; set; }
        public string PostGroup { get; set; }
        public bool PostOut { get; set; }
        public string PostOutTxt { get; set; }
        public string PosterRight { get; set; }
        public string File1Momo { get; set; }
        public string File2Momo { get; set; }
        public string File3Momo { get; set; }
        public string File4Momo { get; set; }
        public string File5Momo { get; set; }
        public int? CreatedDeptID { get; set; }
        public string CreatedDeptName { get; set; }
        public string CreatedUser { get; set; }
        public string CreatedUserPhone { get; set; }

        public List<HttpPostedFileBase> Files { get; set; }

        public List<GroupFile> GroupFile { get; set; }
    }
}
