using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class MainMenuModel
    {
        public int id { get; set; }
        public string MenuName { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }
        public int ParentId { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }
}
