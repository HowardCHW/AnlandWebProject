using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class CategoritesDataModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Theme { get; set; }
        public string Cake { get; set; }
        public string Service { get; set; }
        public string Url { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
