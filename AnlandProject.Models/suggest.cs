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
    
    public partial class suggest
    {
        public int question_id { get; set; }
        public string question_name { get; set; }
        public string owner { get; set; }
        public Nullable<System.DateTime> startday { get; set; }
        public Nullable<System.DateTime> stopday { get; set; }
        public bool is_open { get; set; }
        public string content { get; set; }
        public bool check_ok { get; set; }
        public string who { get; set; }
        public Nullable<System.DateTime> create_date { get; set; }
    }
}
