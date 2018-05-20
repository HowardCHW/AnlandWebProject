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
    public class NewsService : INewsService
    {
        private IRepository<an_news> _newsRepository = new GenericRepository<an_news>();

        public List<NewsModel> NewsQueryAll()
        {
            var result = _newsRepository.GetAll().Select(n => new NewsModel()
            {
                ID = n.ID,
                Theme = n.Theme,
                Cake = n.Cake,
                Service = n.Service,
                Subject = n.subject,
                Author = n.author,
                HomepageMomo = n.homepage_momo,
                Homepage1 = n.homepage1,
                Homepage2 = n.homepage2,
                Homepage3 = n.homepage3,
                Homepage4 = n.homepage4,
                Homepage5 = n.homepage5,
                Homepage6 = n.homepage6,
                Body = n.Body,
                PostDate = n.postdate,
                PostTime = n.posttime,
                Hit = n.hit,
                PostName = n.postname,
                EndDate = n.end_date,
                PostGroup = n.post_group,
                PostOut = n.post_out,
                PostOutTxt = n.post_out_txt,
                PosterRight = n.poster_right,
                File1Momo = n.file1_momo,
                File2Momo = n.file2_momo,
                File3Momo = n.file3_momo,
                File4Momo = n.file4_momo,
                File5Momo = n.file5_momo
            }).ToList();

            return result;
        }

        public NewsModel NewsQueryByID(int id)
        {
            var tempData = _newsRepository.Get(x => x.ID == id);

            NewsModel result = null;
            if (tempData != null)
            {
                result = new NewsModel()
                {
                    ID = tempData.ID,
                    Theme = tempData.Theme,
                    Cake = tempData.Cake,
                    Service = tempData.Service,
                    Subject = tempData.subject,
                    Author = tempData.author,
                    HomepageMomo = tempData.homepage_momo,
                    Homepage1 = tempData.homepage1,
                    Homepage2 = tempData.homepage2,
                    Homepage3 = tempData.homepage3,
                    Homepage4 = tempData.homepage4,
                    Homepage5 = tempData.homepage5,
                    Homepage6 = tempData.homepage6,
                    Body = tempData.Body,
                    PostDate = tempData.postdate,
                    PostTime = tempData.posttime,
                    Hit = tempData.hit,
                    PostName = tempData.postname,
                    EndDate = tempData.end_date,
                    PostGroup = tempData.post_group,
                    PostOut = tempData.post_out,
                    PostOutTxt = tempData.post_out_txt,
                    PosterRight = tempData.poster_right,
                    File1Momo = tempData.file1_momo,
                    File2Momo = tempData.file2_momo,
                    File3Momo = tempData.file3_momo,
                    File4Momo = tempData.file4_momo,
                    File5Momo = tempData.file5_momo
                };
            }
            return result;
        }
    }
}
