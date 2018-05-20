using AnlandProject.Models;
using AnlandProject.Models.Interface;
using AnlandProject.Models.Repository;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Service
{
    public class MenuService : IMenuService
    {
        private IRepository<main_menu> _menuRepository = new GenericRepository<main_menu>();

        public List<MainMenuModel> MenuQuery()
        {
            var result = _menuRepository.GetAll().Select(m => new MainMenuModel()
            {
                id = m.id,
                MenuName = m.menuname,
                Level = m.menulevel,
                Order = m.levelorder,
                ParentId = m.parentid,
                Action = m.action,
                Controller = m.controller
            }).ToList();            

            return result;
        }
    }
}
