﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class NewsModel
    {
        public int ID { get; set; }
        public string Theme { get; set; }
        public string Cake { get; set; }
        public string Service { get; set; }
        public string Subject { get; set; }
        public string Author { get; set; }
        public string HomepageMomo { get; set; }
        public string Homepage1 { get; set; }
        public string Homepage2 { get; set; }
        public string Homepage3 { get; set; }
        public string Homepage4 { get; set; }
        public string Homepage5 { get; set; }
        public string Homepage6 { get; set; }
        public string Body { get; set; }
        public DateTime? PostDate { get; set; }
        public DateTime? PostTime { get; set; }
        public int? Hit { get; set; }
        public string PostName { get; set; }
        public DateTime? EndDate { get; set; }
        public string PostGroup { get; set; }
        public string PostOut { get; set; }
        public string PostOutTxt { get; set; }
        public string PosterRight { get; set; }
        public string File1Momo { get; set; }
        public string File2Momo { get; set; }
        public string File3Momo { get; set; }
        public string File4Momo { get; set; }
        public string File5Momo { get; set; }
    }
}