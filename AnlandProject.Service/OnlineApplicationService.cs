using AnlandProject.Models;
using AnlandProject.Models.Interface;
using AnlandProject.Models.Repository;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnlandProject.Service
{
    public class OnlineApplicationService : IOnlineApplicationService
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        private IRepository<onlineapp> _onlineappRepository = new GenericRepository<onlineapp>();
        private IRepository<engcopy> _engcopyRepository = new GenericRepository<engcopy>();
        private IRepository<copy> _copyRepository = new GenericRepository<copy>();
        private IRepository<landprice> _landpriceRepository = new GenericRepository<landprice>();

        public List<OnlineApplicationModel> OnlineAppQueryAll()
        {
            var result = _onlineappRepository.GetAll().Select(o => new OnlineApplicationModel()
            {
                ID = o.ID,
                ApplyName = o.apply_name,
                ApplyBirth = o.apply_birth,
                ApplyID = o.apply_id,
                ApplyTel = o.apply_tel,
                ApplyEMail = o.apply_email,
                AgentName = o.agent_name,
                AgentBirth = o.agent_birth,
                AgentID = o.agent_id,
                AgentRe = o.agent_re,
                AppItem1 = o.app_item_1,
                AppItem2 = o.app_item_2,
                AppItem3 = o.app_item_3,
                Year1 = o.year_1,
                Year2 = o.year_2,
                Year3 = o.year_3,
                DocItem1 = o.docitem_1,
                DocItem2 = o.docitem_2,
                DocItem3 = o.docitem_3,
                No1 = o.no_1,
                No2 = o.no_2,
                No3 = o.no_3,
                Copy1 = o.copy_1,
                Copy2 = o.copy_2,
                Purpose1 = o.purpose_1,
                Purpose2 = o.purpose_2,
                Purpose3 = o.purpose_3,
                Purpose4 = o.purpose_4,
                Purpose5 = o.purpose_5,
                PurposeNote = o.purpose_note,
                WDate = o.wdate,
                Qa1 = o.qa_1,
                Qa2 = o.qa_2,
                Qa3 = o.qa_3,
                Qa4 = o.qa_4,
                Qa5 = o.qa_5,
                OtherNote = o.other_note,
                GetAppDate = o.get_appdate,
                GetAppTime = o.get_apptime,
                QaNote = o.qa_note
            }).ToList();

            return result;
        }

        public OnlineApplicationModel OnlineAppQueryByID(int id)
        {
            var tempData = _onlineappRepository.Get(o => o.ID == id);

            OnlineApplicationModel result = null;
            if (tempData != null)
            {
                result = new OnlineApplicationModel()
                {
                    ID = tempData.ID,
                    ApplyName = tempData.apply_name,
                    ApplyBirth = tempData.apply_birth,
                    ApplyID = tempData.apply_id,
                    ApplyTel = tempData.apply_tel,
                    ApplyEMail = tempData.apply_email,
                    AgentName = tempData.agent_name,
                    AgentBirth = tempData.agent_birth,
                    AgentID = tempData.agent_id,
                    AgentRe = tempData.agent_re,
                    AppItem1 = tempData.app_item_1,
                    AppItem2 = tempData.app_item_2,
                    AppItem3 = tempData.app_item_3,
                    Year1 = tempData.year_1,
                    Year2 = tempData.year_2,
                    Year3 = tempData.year_3,
                    DocItem1 = tempData.docitem_1,
                    DocItem2 = tempData.docitem_2,
                    DocItem3 = tempData.docitem_3,
                    No1 = tempData.no_1,
                    No2 = tempData.no_2,
                    No3 = tempData.no_3,
                    Copy1 = tempData.copy_1,
                    Copy2 = tempData.copy_2,
                    Purpose1 = tempData.purpose_1,
                    Purpose2 = tempData.purpose_2,
                    Purpose3 = tempData.purpose_3,
                    Purpose4 = tempData.purpose_4,
                    Purpose5 = tempData.purpose_5,
                    PurposeNote = tempData.purpose_note,
                    WDate = tempData.wdate,
                    Qa1 = tempData.qa_1,
                    Qa2 = tempData.qa_2,
                    Qa3 = tempData.qa_3,
                    Qa4 = tempData.qa_4,
                    Qa5 = tempData.qa_5,
                    OtherNote = tempData.other_note,
                    GetAppDate = tempData.get_appdate,
                    GetAppTime = tempData.get_apptime,
                    QaNote = tempData.qa_note
                };
            }
            return result;
        }

        public bool OnlineAppSave(OnlineApplicationModel saveData, out int newDataID)
        {
            int resultRow = 0;
            newDataID = 0;
            try
            {
                onlineapp newData = new onlineapp()
                {
                    apply_name = saveData.ApplyName,
                    apply_birth = saveData.ApplyBirth,
                    apply_id = saveData.ApplyID,
                    apply_tel = saveData.ApplyTel,
                    apply_email = saveData.ApplyEMail,
                    agent_name = saveData.AgentName,
                    agent_birth = saveData.AgentBirth,
                    agent_id = saveData.AgentID,
                    agent_re = saveData.AgentRe,
                    app_item_1 = saveData.AppItem1,
                    app_item_2 = saveData.AppItem2,
                    app_item_3 = saveData.AppItem3,
                    year_1 = saveData.Year1,
                    year_2 = saveData.Year2,
                    year_3 = saveData.Year3,
                    docitem_1 = saveData.DocItem1,
                    docitem_2 = saveData.DocItem2,
                    docitem_3 = saveData.DocItem3,
                    no_1 = saveData.No1,
                    no_2 = saveData.No2,
                    no_3 = saveData.No3,
                    copy_1 = saveData.Copy1,
                    copy_2 = saveData.Copy2,
                    purpose_1 = saveData.Purpose1,
                    purpose_2 = saveData.Purpose2,
                    purpose_3 = saveData.Purpose3,
                    purpose_4 = saveData.Purpose4,
                    purpose_5 = saveData.Purpose5,
                    purpose_note = saveData.PurposeNote,
                    wdate = saveData.WDate,
                    qa_1 = saveData.Qa1,
                    qa_2 = saveData.Qa2,
                    qa_3 = saveData.Qa3,
                    qa_4 = saveData.Qa4,
                    qa_5 = saveData.Qa5,
                    other_note = saveData.OtherNote,
                    get_appdate = saveData.GetAppDate,
                    get_apptime = saveData.GetAppTime,
                    qa_note = saveData.QaNote
                };

                var newResult = _onlineappRepository.CreateWithResult(newData, out resultRow);
                newDataID = newResult.ID;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public bool OnlineAppDelete(int id)
        {
            int result = 0;
            try
            {
                var deleteData = _onlineappRepository.Get(o => o.ID == id);
                result = _onlineappRepository.Delete(deleteData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }

        public List<OnlineApplicationModel> EngCopyQueryAll()
        {
            var result = _engcopyRepository.GetAll().Select(e => new OnlineApplicationModel()
            {
                ID = e.ID,
                ApplyName = e.apply_name,
                ApplyBirth = e.apply_birth,
                ApplyID = e.apply_id,
                ApplyTel = e.apply_tel,
                ApplyAdd = e.apply_add,
                ApplyEMail = e.apply_email,
                AgentName = e.agent_name,
                AgentBirth = e.agent_birth,
                AgentID = e.agent_id,
                AgentTel = e.agent_tel,
                AgentAdd = e.agent_add,
                AgentRe = e.agent_re,
                CompanyName = e.company_name,
                CompanyAdd = e.company_add,
                Sarea1 = e.sarea_1,
                Sarea2 = e.sarea_2,
                AreaNo1 = e.areano_1,
                AreaNo2 = e.areano_2,
                DocName1 = e.docname_1,
                DocName2 = e.docname_2,
                DocItem1 = e.docitem_1,
                DocItem2 = e.docitem_2,
                DocItem3 = e.docitem_3,
                DocItem4 = e.docitem_4,
                Purpose1 = e.purpose_1,
                Purpose2 = e.purpose_2,
                Purpose3 = e.purpose_3,
                Purpose4 = e.purpose_4,
                Purpose5 = e.purpose_5,
                PurposeNote = e.purpose_note,
                WDate = e.wdate,
                Qa1 = e.qa_1,
                Qa2 = e.qa_2,
                Qa3 = e.qa_3,
                Qa4 = e.qa_4,
                Qa5 = e.qa_5,
                QaNote = e.qa_note
            }).ToList();

            return result;
        }

        public OnlineApplicationModel EngCopyQueryByID(int id)
        {
            var tempData = _engcopyRepository.Get(e => e.ID == id);

            OnlineApplicationModel result = null;
            if (tempData != null)
            {
                result = new OnlineApplicationModel()
                {
                    ID = tempData.ID,
                    ApplyName = tempData.apply_name,
                    ApplyBirth = tempData.apply_birth,
                    ApplyID = tempData.apply_id,
                    ApplyTel = tempData.apply_tel,
                    ApplyAdd = tempData.apply_add,
                    ApplyEMail = tempData.apply_email,
                    AgentName = tempData.agent_name,
                    AgentBirth = tempData.agent_birth,
                    AgentID = tempData.agent_id,
                    AgentTel = tempData.agent_tel,
                    AgentAdd = tempData.agent_add,
                    AgentRe = tempData.agent_re,
                    CompanyName = tempData.company_name,
                    CompanyAdd = tempData.company_add,
                    Sarea1 = tempData.sarea_1,
                    Sarea2 = tempData.sarea_2,
                    AreaNo1 = tempData.areano_1,
                    AreaNo2 = tempData.areano_2,
                    DocName1 = tempData.docname_1,
                    DocName2 = tempData.docname_2,
                    DocItem1 = tempData.docitem_1,
                    DocItem2 = tempData.docitem_2,
                    DocItem3 = tempData.docitem_3,
                    DocItem4 = tempData.docitem_4,
                    Purpose1 = tempData.purpose_1,
                    Purpose2 = tempData.purpose_2,
                    Purpose3 = tempData.purpose_3,
                    Purpose4 = tempData.purpose_4,
                    Purpose5 = tempData.purpose_5,
                    PurposeNote = tempData.purpose_note,
                    WDate = tempData.wdate,
                    Qa1 = tempData.qa_1,
                    Qa2 = tempData.qa_2,
                    Qa3 = tempData.qa_3,
                    Qa4 = tempData.qa_4,
                    Qa5 = tempData.qa_5,
                    QaNote = tempData.qa_note
                };
            }
            return result;
        }

        public bool EngCopySave(OnlineApplicationModel saveData, out int newDataID)
        {
            int resultRow = 0;
            newDataID = 0;
            try
            {
                engcopy newData = new engcopy()
                {
                    apply_name = saveData.ApplyName,
                    apply_birth = saveData.ApplyBirth,
                    apply_id = saveData.ApplyID,
                    apply_tel = saveData.ApplyTel,
                    apply_add = saveData.ApplyAdd,
                    apply_email = saveData.ApplyEMail,
                    agent_name = saveData.AgentName,
                    agent_birth = saveData.AgentBirth,
                    agent_id = saveData.AgentID,
                    agent_tel = saveData.AgentTel,
                    agent_add = saveData.AgentAdd,
                    agent_re = saveData.AgentRe,
                    company_name = saveData.CompanyName,
                    company_add = saveData.CompanyAdd,
                    sarea_1 = saveData.Sarea1,
                    sarea_2 = saveData.Sarea2,
                    areano_1 = saveData.AreaNo1,
                    areano_2 = saveData.AreaNo2,
                    docname_1 = saveData.DocName1,
                    docname_2 = saveData.DocName2,
                    docitem_1 = saveData.DocItem1,
                    docitem_2 = saveData.DocItem2,
                    docitem_3 = saveData.DocItem3,
                    docitem_4 = saveData.DocItem4,
                    purpose_1 = saveData.Purpose1,
                    purpose_2 = saveData.Purpose2,
                    purpose_3 = saveData.Purpose3,
                    purpose_4 = saveData.Purpose4,
                    purpose_5 = saveData.Purpose5,
                    purpose_note = saveData.PurposeNote,
                    wdate = saveData.WDate,
                    qa_1 = saveData.Qa1,
                    qa_2 = saveData.Qa2,
                    qa_3 = saveData.Qa3,
                    qa_4 = saveData.Qa4,
                    qa_5 = saveData.Qa5,
                    qa_note = saveData.QaNote
                };

                var newResult = _engcopyRepository.CreateWithResult(newData, out resultRow);
                newDataID = newResult.ID;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public bool EngCopyDelete(int id)
        {
            int result = 0;
            try
            {
                var deleteData = _engcopyRepository.Get(o => o.ID == id);
                result = _engcopyRepository.Delete(deleteData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }

        public List<OnlineApplicationModel> CopyQueryAll()
        {
            var result = _copyRepository.GetAll().Select(c => new OnlineApplicationModel()
            {
                ID = c.ID,
                ApplyName = c.apply_name,
                ApplyBirth = c.apply_birth,
                ApplyID = c.apply_id,
                ApplyTel = c.apply_tel,
                ApplyAdd = c.apply_add,
                ApplyEMail = c.apply_email,
                AgentName = c.agent_name,
                AgentBirth = c.agent_birth,
                AgentID = c.agent_id,
                AgentTel = c.agent_tel,
                AgentAdd = c.agent_add,
                AgentRe = c.agent_re,
                CompanyName = c.company_name,
                CompanyAdd = c.company_add,
                DocNo1 = c.docno_1,
                DocNo2 = c.docno_2,
                DocNo3 = c.docno_3,
                DocNo4 = c.docno_4,
                DocName1 = c.docname_1,
                DocName2 = c.docname_2,
                DocName3 = c.docname_3,
                DocName4 = c.docname_4,
                DocItem1 = c.docitem_1,
                DocItem2 = c.docitem_2,
                DocItem3 = c.docitem_3,
                DocItem4 = c.docitem_4,
                Purpose1 = c.purpose_1,
                Purpose2 = c.purpose_2,
                Purpose3 = c.purpose_3,
                Purpose4 = c.purpose_4,
                Purpose5 = c.purpose_5,
                Purpose6 = c.purpose_6,
                PurposeNote = c.purpose_note,
                WDate = c.wdate,
                Qa1 = c.qa_1,
                Qa2 = c.qa_2,
                Qa3 = c.qa_3,
                Qa4 = c.qa_4,
                Qa5 = c.qa_5
            }).ToList();

            return result;
        }

        public OnlineApplicationModel CopyQueryByID(int id)
        {
            var tempData = _copyRepository.Get(c => c.ID == id);

            OnlineApplicationModel result = null;
            if (tempData != null)
            {
                result = new OnlineApplicationModel()
                {
                    ID = tempData.ID,
                    ApplyName = tempData.apply_name,
                    ApplyBirth = tempData.apply_birth,
                    ApplyID = tempData.apply_id,
                    ApplyTel = tempData.apply_tel,
                    ApplyAdd = tempData.apply_add,
                    ApplyEMail = tempData.apply_email,
                    AgentName = tempData.agent_name,
                    AgentBirth = tempData.agent_birth,
                    AgentID = tempData.agent_id,
                    AgentTel = tempData.agent_tel,
                    AgentAdd = tempData.agent_add,
                    AgentRe = tempData.agent_re,
                    CompanyName = tempData.company_name,
                    CompanyAdd = tempData.company_add,
                    DocNo1 = tempData.docno_1,
                    DocNo2 = tempData.docno_2,
                    DocNo3 = tempData.docno_3,
                    DocNo4 = tempData.docno_4,
                    DocName1 = tempData.docname_1,
                    DocName2 = tempData.docname_2,
                    DocName3 = tempData.docname_3,
                    DocName4 = tempData.docname_4,
                    DocItem1 = tempData.docitem_1,
                    DocItem2 = tempData.docitem_2,
                    DocItem3 = tempData.docitem_3,
                    DocItem4 = tempData.docitem_4,
                    Purpose1 = tempData.purpose_1,
                    Purpose2 = tempData.purpose_2,
                    Purpose3 = tempData.purpose_3,
                    Purpose4 = tempData.purpose_4,
                    Purpose5 = tempData.purpose_5,
                    Purpose6 = tempData.purpose_6,
                    PurposeNote = tempData.purpose_note,
                    WDate = tempData.wdate,
                    Qa1 = tempData.qa_1,
                    Qa2 = tempData.qa_2,
                    Qa3 = tempData.qa_3,
                    Qa4 = tempData.qa_4,
                    Qa5 = tempData.qa_5
                };
            }
            return result;
        }

        public bool CopySave(OnlineApplicationModel saveData, out int newDataID)
        {
            int resultRow = 0;
            newDataID = 0;
            try
            {
                copy newData = new copy()
                {
                    apply_name = saveData.ApplyName,
                    apply_birth = saveData.ApplyBirth,
                    apply_id = saveData.ApplyID,
                    apply_tel = saveData.ApplyTel,
                    apply_add = saveData.ApplyAdd,
                    apply_email = saveData.ApplyEMail,
                    agent_name = saveData.AgentName,
                    agent_birth = saveData.AgentBirth,
                    agent_id = saveData.AgentID,
                    agent_tel = saveData.AgentTel,
                    agent_add = saveData.AgentAdd,
                    agent_re = saveData.AgentRe,
                    company_name = saveData.CompanyName,
                    company_add = saveData.CompanyAdd,
                    docno_1 = saveData.DocNo1,
                    docno_2 = saveData.DocNo2,
                    docno_3 = saveData.DocNo3,
                    docno_4 = saveData.DocNo4,
                    docname_1 = saveData.DocName1,
                    docname_2 = saveData.DocName2,
                    docname_3 = saveData.DocName3,
                    docname_4 = saveData.DocName4,
                    docitem_1 = saveData.DocItem1,
                    docitem_2 = saveData.DocItem2,
                    docitem_3 = saveData.DocItem3,
                    docitem_4 = saveData.DocItem4,
                    purpose_1 = saveData.Purpose1,
                    purpose_2 = saveData.Purpose2,
                    purpose_3 = saveData.Purpose3,
                    purpose_4 = saveData.Purpose4,
                    purpose_5 = saveData.Purpose5,
                    purpose_6 = saveData.Purpose6,
                    purpose_note = saveData.PurposeNote,
                    wdate = saveData.WDate,
                    qa_1 = saveData.Qa1,
                    qa_2 = saveData.Qa2,
                    qa_3 = saveData.Qa3,
                    qa_4 = saveData.Qa4,
                    qa_5 = saveData.Qa5
                };

                var newResult = _copyRepository.CreateWithResult(newData, out resultRow);
                newDataID = newResult.ID;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public bool CopyDelete(int id)
        {
            int result = 0;
            try
            {
                var deleteData = _copyRepository.Get(o => o.ID == id);
                result = _copyRepository.Delete(deleteData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }

        public List<OnlineApplicationModel> LandPriceQueryAll()
        {
            var result = _landpriceRepository.GetAll().Select(l => new OnlineApplicationModel()
            {
                ID = l.ID,
                ApplyName = l.apply_name,
                ApplyID = l.apply_id,
                ApplyTel = l.apply_tel,
                ApplyAdd = l.apply_add,
                ApplyEMail = l.apply_email,
                Purpose = l.purpose,
                PurposeNote = l.purpose_note,
                Area1 = l.area_1,
                Area2 = l.area_2,
                Area3 = l.area_3,
                Area4 = l.area_4,
                Area5 = l.area_5,
                Sarea1 = l.sarea_1,
                Sarea2 = l.sarea_2,
                Sarea3 = l.sarea_3,
                Sarea4 = l.sarea_4,
                Sarea5 = l.sarea_5,
                AreaNo1 = l.areano_1,
                AreaNo2 = l.areano_2,
                AreaNo3 = l.areano_3,
                AreaNo4 = l.areano_4,
                AreaNo5 = l.areano_5,
                AreaYear1 = l.areayear_1,
                AreaYear2 = l.areayear_2,
                AreaYear3 = l.areayear_3,
                AreaYear4 = l.areayear_4,
                AreaYear5 = l.areayear_5,
                Note = l.note,
                WDate = l.wdate,
                Qa1 = l.qa_1,
                Qa2 = l.qa_2,
                Qa3 = l.qa_3,
                Qa4 = l.qa_4,
                Qa5 = l.qa_5
            }).ToList();

            return result;
        }

        public OnlineApplicationModel LandPriceQueryByID(int id)
        {
            var tempData = _landpriceRepository.Get(l => l.ID == id);

            OnlineApplicationModel result = null;
            if (tempData != null)
            {
                result = new OnlineApplicationModel()
                {
                    ID = tempData.ID,
                    ApplyName = tempData.apply_name,
                    ApplyID = tempData.apply_id,
                    ApplyTel = tempData.apply_tel,
                    ApplyAdd = tempData.apply_add,
                    ApplyEMail = tempData.apply_email,
                    Purpose = tempData.purpose,
                    PurposeNote = tempData.purpose_note,
                    Area1 = tempData.area_1,
                    Area2 = tempData.area_2,
                    Area3 = tempData.area_3,
                    Area4 = tempData.area_4,
                    Area5 = tempData.area_5,
                    Sarea1 = tempData.sarea_1,
                    Sarea2 = tempData.sarea_2,
                    Sarea3 = tempData.sarea_3,
                    Sarea4 = tempData.sarea_4,
                    Sarea5 = tempData.sarea_5,
                    AreaNo1 = tempData.areano_1,
                    AreaNo2 = tempData.areano_2,
                    AreaNo3 = tempData.areano_3,
                    AreaNo4 = tempData.areano_4,
                    AreaNo5 = tempData.areano_5,
                    AreaYear1 = tempData.areayear_1,
                    AreaYear2 = tempData.areayear_2,
                    AreaYear3 = tempData.areayear_3,
                    AreaYear4 = tempData.areayear_4,
                    AreaYear5 = tempData.areayear_5,
                    Note = tempData.note,
                    WDate = tempData.wdate,
                    Qa1 = tempData.qa_1,
                    Qa2 = tempData.qa_2,
                    Qa3 = tempData.qa_3,
                    Qa4 = tempData.qa_4,
                    Qa5 = tempData.qa_5
                };
            }
            return result;
        }

        public bool LandPriceSave(OnlineApplicationModel saveData, out int newDataID)
        {
            int resultRow = 0;
            newDataID = 0;
            try
            {
                landprice newData = new landprice()
                {
                    apply_name = saveData.ApplyName,
                    apply_id = saveData.ApplyID,
                    apply_tel = saveData.ApplyTel,
                    apply_add = saveData.ApplyAdd,
                    apply_email = saveData.ApplyEMail,
                    purpose = saveData.Purpose,
                    purpose_note = saveData.PurposeNote,
                    area_1 = saveData.Area1,
                    area_2 = saveData.Area2,
                    area_3 = saveData.Area3,
                    area_4 = saveData.Area4,
                    area_5 = saveData.Area5,
                    sarea_1 = saveData.Sarea1,
                    sarea_2 = saveData.Sarea2,
                    sarea_3 = saveData.Sarea3,
                    sarea_4 = saveData.Sarea4,
                    sarea_5 = saveData.Sarea5,
                    areano_1 = saveData.AreaNo1,
                    areano_2 = saveData.AreaNo2,
                    areano_3 = saveData.AreaNo3,
                    areano_4 = saveData.AreaNo4,
                    areano_5 = saveData.AreaNo5,
                    areayear_1 = saveData.AreaYear1,
                    areayear_2 = saveData.AreaYear2,
                    areayear_3 = saveData.AreaYear3,
                    areayear_4 = saveData.AreaYear4,
                    areayear_5 = saveData.AreaYear5,
                    note = saveData.Note,
                    wdate = saveData.WDate,
                    qa_1 = saveData.Qa1,
                    qa_2 = saveData.Qa2,
                    qa_3 = saveData.Qa3,
                    qa_4 = saveData.Qa4,
                    qa_5 = saveData.Qa5
                };

                var newResult = _landpriceRepository.CreateWithResult(newData, out resultRow);
                newDataID = newResult.ID;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public bool LandPriceDelete(int id)
        {
            int result = 0;
            try
            {
                var deleteData = _landpriceRepository.Get(o => o.ID == id);
                result = _landpriceRepository.Delete(deleteData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }

        public void Dispose()
        {
            if (_onlineappRepository != null)
            {
                _onlineappRepository.Dispose();
            }
            if (_engcopyRepository != null)
            {
                _engcopyRepository.Dispose();
            }
            if (_copyRepository != null)
            {
                _copyRepository.Dispose();
            }
            if (_landpriceRepository != null)
            {
                _landpriceRepository.Dispose();
            }
        }
    }
}
