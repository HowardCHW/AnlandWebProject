using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class BackendUserModel
    {
        public int ID { get; set; }
        public string UserAccount { get; set; }
        public string UserName { get; set; }
        public List<int> MenuPermissions { get; set; }
        public string GroupName { get; set; }
        public string Action { get; set; }
    }
}
