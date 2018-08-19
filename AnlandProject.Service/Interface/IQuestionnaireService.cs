using AnlandProject.Models;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service.Interface
{
    public interface IQuestionnaireService : IDisposable
    {
        List<QuestionnaireDataModel> QuestionnaireQueryAll();

        QuestionnaireDataModel QuestionnaireQueryByID(int id);

        QuestionnaireDataModel QuestionnaireAnswerByID(int id);

        bool QuestionnaireSave(QuestionnaireDataModel saveData);

        bool QuestionnaireDelete(int id);

        bool QuestionNameCheck(string name);
        
    }
}
