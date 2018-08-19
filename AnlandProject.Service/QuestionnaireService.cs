using AnlandProject.Models;
using AnlandProject.Models.Interface;
using AnlandProject.Models.Repository;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AnlandProject.Service
{
    public class QuestionnaireService : IQuestionnaireService
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        private IRepository<suggest> _suggestRepository = new GenericRepository<suggest>();

        public List<QuestionnaireDataModel> QuestionnaireQueryAll()
        {
            var result = _suggestRepository.GetMany(s => s.check_ok).Select(n => new QuestionnaireDataModel()
            {
                QuestionID = n.question_id,
                QuestionName = n.question_name,
                QuestionStatus = n.startday.Value > DateTime.Now ? "未開放" : n.stopday.Value < DateTime.Now ? "已關閉" : "問卷調查進行中",
                IsOpen = n.is_open,
            }).ToList();

            return result;
        }

        public QuestionnaireDataModel QuestionnaireQueryByID(int id)
        {
            var tempData = _suggestRepository.Get(x => x.question_id == id);

            QuestionnaireDataModel result = null;
            if (tempData != null)
            {
                result = new QuestionnaireDataModel()
                {
                    QuestionID = tempData.question_id,
                    QuestionName = tempData.question_name,
                    Owner = tempData.owner,
                    StartDay = tempData.startday,
                    StopDay = tempData.stopday,
                    IsOpen = tempData.is_open,
                    Content = tempData.content,
                    Questions = tempData.suggest2.Select(s => new QuestionDataModel
                    {
                        ID = s.id,
                        QuestionID = s.question_id.Value,
                        QuestionNo = s.question_number,
                        Question = s.question,
                        Type = s.type1,
                        Options = s.type1 == "問答" ? null : s.option1.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries).ToList()
                    }).ToList()
                };
            }
            return result;
        }

        public QuestionnaireDataModel QuestionnaireAnswerByID(int id)
        {
            var tempData = _suggestRepository.Get(x => x.question_id == id);

            QuestionnaireDataModel result = null;
            if (tempData != null)
            {
                result = new QuestionnaireDataModel()
                {
                    QuestionID = tempData.question_id,
                    QuestionName = tempData.question_name,
                    StartDay = tempData.startday,
                    StopDay = tempData.stopday,
                    Content = tempData.content,
                    TotalReceivedAnswers = tempData.suggest3.Count,
                    Answers = tempData.suggest2.Where(s => s.type1 != "問答").Select(s => new AnswerDataModel
                    {
                        QuestionNo = s.question_number,
                        Question = s.question,
                        QuestionType = s.type1,
                        AllAnswers = s.option1.Split(new string[] { "||" }, StringSplitOptions.None).Select(o => new OptionAnswerDataModel
                        {
                            Option = o
                        }).ToList()
                    }).ToList()
                };

                foreach (var item in result.Answers)
                {
                    List<string> answers = tempData.suggest3.Select(s3 => s3.answer.Split(new string[] { "||" }, StringSplitOptions.None)[int.Parse(item.QuestionNo) - 1]).ToList();
                    for (int i = 0; i < item.AllAnswers.Count; i++)
                    {
                        double answerCount = (double)answers.Count(a => a.Trim() == (i + 1).ToString()) / (double)result.TotalReceivedAnswers;
                        item.AllAnswers[i].AnswerPercent = answerCount.ToString("P0");
                    }
                }
            }
            return result;
        }

        public bool QuestionnaireSave(QuestionnaireDataModel saveData)
        {
            int resultRow = 0;
            try
            {
                if (saveData.QuestionID > 0)
                {
                    var originalData = _suggestRepository.Get(n => n.question_id == saveData.QuestionID);
                    originalData.question_name = saveData.QuestionName;
                    originalData.owner = saveData.Owner;
                    originalData.startday = saveData.StartDay;
                    originalData.stopday = saveData.StopDay;
                    originalData.is_open = saveData.IsOpen;
                    originalData.content = saveData.Content;
                    foreach (var item in saveData.Questions)
                    {
                        var option = originalData.suggest2.First(s => s.id == item.ID);
                        option.question = item.Question;
                        if (item.Type != "問答")
                        {
                            option.type1 = item.Type;
                            option.option1 = string.Join("||", item.Options);
                        }
                    }
                    resultRow = _suggestRepository.Update(originalData);
                }
                else
                {
                    suggest newData = new suggest()
                    {
                        question_name = saveData.QuestionName,
                        owner = saveData.Owner,
                        startday = saveData.StartDay,
                        stopday = saveData.StopDay,
                        is_open = saveData.IsOpen,
                        content = saveData.Content,
                        check_ok = true,
                        who = saveData.Who,
                        create_date = saveData.CreateDate,
                        suggest2 = saveData.Questions.Select(s => new suggest2
                        {
                            question_number = s.QuestionNo,
                            question = s.Question,
                            type1 = s.Type,
                            option1 = s.Options == null ? "" : string.Join("||", s.Options)
                        }).ToList()
                    };
                    resultRow = _suggestRepository.Create(newData);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public bool QuestionnaireDelete(int id)
        {
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    IRepository<suggest2> _suggest2Repository = new GenericRepository<suggest2>();
                    IRepository<suggest3> _suggest3Repository = new GenericRepository<suggest3>();

                    var deleteSug2 = _suggest2Repository.GetMany(s2 => s2.question_id == id);
                    var sug2Count = deleteSug2.Count();
                    foreach (var item in deleteSug2)
                    {
                        result += _suggest2Repository.Delete(item);
                    }
                    
                    var deleteSug3 = _suggest3Repository.GetMany(s3 => s3.question_id == id);
                    var sug3Count = deleteSug3.Count();
                    foreach (var item in deleteSug3)
                    {
                        result += _suggest3Repository.Delete(item);
                    }
                    
                    var deleteData = _suggestRepository.Get(n => n.question_id == id);
                    result += _suggestRepository.Delete(deleteData);

                    if (result == (sug2Count+ sug3Count + 1))
                    {
                        scope.Complete();
                    }                    
                }
            }
            catch (TransactionAbortedException ex)
            {
                logger.Error("TransactionAbortedException Message: {0}", ex);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }

        public bool QuestionNameCheck(string name)
        {
            return _suggestRepository.IsAny(s => s.question_name == name);
        }

        public void Dispose()
        {
            if (_suggestRepository != null)
            {
                _suggestRepository.Dispose();
            }
        }
    }
}
