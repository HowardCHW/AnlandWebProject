using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class ClassificationModel
    {
        public int ID { get; set; }
        public string SNo { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string CreUser { get; set; }
        public DateTime? CreDate { get; set; }
    }
}
