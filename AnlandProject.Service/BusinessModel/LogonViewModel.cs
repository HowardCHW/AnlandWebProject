using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class LogonViewModel
    {
        [Required(ErrorMessage = "請輸入帳號")]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }
    }
}
