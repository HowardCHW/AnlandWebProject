using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class IndexDataModel
    {
        public List<DefaultDataModel> Top5News { get; set; }
        public List<LawsModel> Top5Laws { get; set; }
    }
}
