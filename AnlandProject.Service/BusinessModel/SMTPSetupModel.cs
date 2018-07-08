using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class SMTPSetupModel
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string SMTP { get; set; }
        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; }
        public string Recipient { get; set; }
        public string RecipientName { get; set; }
        public string Subject { get; set; }
        
    }
}
