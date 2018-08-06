using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.BusinessModel
{
    public class ClassificationTypeEnum
    {
        public enum ClassificationSearch
        {
            Theme,
            Cake,
            Service
        }

        public enum Classification
        {
            NewsClass,
            LawClass,
            QaClass,
            BoardClass,
            MeetingClass,
            DocumentClass
        }
    }
}
