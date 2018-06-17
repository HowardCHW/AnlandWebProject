using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class CategoryModel
    {
        public int ID { get; set; }
        public int? ClassID { get; set; }
        public string ClassName { get; set; }
        public string CreUser { get; set; }
        public DateTime? CreDate { get; set; }
    }
}
