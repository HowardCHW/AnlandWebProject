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

namespace AnlandProject.Service
{
    public class CategoritesService : ICategoritesService
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        private IRepository<Categories> _categoritesRepository = new GenericRepository<Categories>();
        
        public List<CategoritesDataModel> CategoritesQueryAll()
        {
            var result = _categoritesRepository.GetAll().Select(q => new CategoritesDataModel()
            {
                ID = q.ID,
                Title = q.Title,
                Theme = q.theme,
                Cake = q.cake,
                Service = q.service,
                OpenWindow = q.openWindow,
                Url = q.url
            }).ToList();

            return result;
        }

        public CategoritesDataModel CategoritesQueryByID(int id)
        {
            var tempData = _categoritesRepository.Get(x => x.ID == id);

            CategoritesDataModel result = null;
            if (tempData != null)
            {
                result = new CategoritesDataModel()
                {
                    ID = tempData.ID,
                    Title = tempData.Title,
                    Subject = tempData.Subject,
                    Theme = tempData.theme,
                    Cake = tempData.cake,
                    Service = tempData.service,
                    Url = tempData.url,
                    OpenWindow = tempData.openWindow
                };
            }
            return result;
        }

        public bool CategoritesSave(CategoritesDataModel saveData)
        {
            int resultRow = 0;
            try
            {
                if (saveData.ID > 0)
                {
                    var originalData = _categoritesRepository.Get(n => n.ID == saveData.ID);
                    originalData.Title = saveData.Title;
                    originalData.Subject = saveData.Subject;
                    originalData.theme = saveData.Theme;
                    originalData.cake = saveData.Cake;
                    originalData.service = saveData.Service;
                    originalData.url = saveData.Url;
                    originalData.openWindow = saveData.OpenWindow;
                    resultRow = _categoritesRepository.Update(originalData);
                }
                else
                {
                    Categories newData = new Categories()
                    {
                        Title = saveData.Title,
                        Subject = saveData.Subject,
                        theme = saveData.Theme,
                        cake = saveData.Cake,
                        service = saveData.Service,
                        url = saveData.Url,
                        openWindow = saveData.OpenWindow,
                        credate = DateTime.Now
                    };
                    resultRow = _categoritesRepository.Create(newData);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }

        public bool CategoritesDelete(int id)
        {
            int result = 0;
            try
            {
                var deleteData = _categoritesRepository.Get(n => n.ID == id);
                result = _categoritesRepository.Delete(deleteData);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return result > 0;
        }

        public void Dispose()
        {
            if (_categoritesRepository != null)
            {
                _categoritesRepository.Dispose();
            }
        }
    }
}
