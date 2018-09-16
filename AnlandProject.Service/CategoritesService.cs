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
                Title = q.Title,
                Theme = q.theme,
                Cake = q.cake,
                Service = q.service,
                Url = q.url
            }).ToList();

            return result;
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
