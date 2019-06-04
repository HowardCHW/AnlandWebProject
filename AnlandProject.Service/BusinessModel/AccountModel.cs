using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class AccountModel
    {
        public int ID { get; set; }
        public string Account { get; set; }
        public string PWD { get; set; }
        public string Name { get; set; }
        public string Rights { get; set; }
        public List<int> MenuPermissions { get; set; }
        public string GroupName { get; set; }
        public string Action { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public bool IsFirstLogin { get; set; }
        public int LoginFailCount { get; set; }
        public bool ShowPwd { get; set; }
        public bool PWDExpired { get; set; }
        public DateTime PWDChangeDate { get; set; }
    }
}
