//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnlandProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class email
    {
        public int no { get; set; }
        public System.DateTime msg_date { get; set; }
        public string msg_name { get; set; }
        public string msg_email { get; set; }
        public string msg_subject { get; set; }
        public string msg_content { get; set; }
        public string rpy_unit { get; set; }
        public Nullable<System.DateTime> rpy_date { get; set; }
        public string rpy_name { get; set; }
        public string rpy_tel { get; set; }
        public string rpy_content { get; set; }
        public string attach1 { get; set; }
        public string attach2 { get; set; }
        public string attach3 { get; set; }
        public string msg_tel { get; set; }
        public string msg_type { get; set; }
    }
}
