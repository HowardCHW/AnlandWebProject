﻿using AnlandProject.Models;
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
    public class NewsService : INewsService
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
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
                PostOut = n.post_out.Value,
                PostOutTxt = n.post_out_txt,
                PosterRight = n.poster_right,
                File1Momo = n.file1_momo,
                File2Momo = n.file2_momo,
                File3Momo = n.file3_momo,
                File4Momo = n.file4_momo,
                File5Momo = n.file5_momo,
                CreatedDepID = n.created_dept_id,
                CreatedUser = n.created_user_name,
                CreatedUserPhone = n.created_user_phone
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
                    PostOut = tempData.post_out.Value,
                    PostOutTxt = tempData.post_out_txt,
                    PosterRight = tempData.poster_right,
                    File1Momo = tempData.file1_momo,
                    File2Momo = tempData.file2_momo,
                    File3Momo = tempData.file3_momo,
                    File4Momo = tempData.file4_momo,
                    File5Momo = tempData.file5_momo,
                    CreatedDepID = tempData.created_dept_id,
                    CreatedUser = tempData.created_user_name,
                    CreatedUserPhone = tempData.created_user_phone
                };
            }
            return result;
        }

        public bool NewsSave(NewsModel saveData)
        {
            int resultRow = 0;
            try
            {
                if (saveData.ID > 0)
                {
                    var originalData = _newsRepository.Get(n => n.ID == saveData.ID);
                    originalData.ID = saveData.ID;
                    originalData.Theme = saveData.Theme;
                    originalData.Cake = saveData.Cake; ;
                    originalData.Service = saveData.Service;
                    originalData.subject = saveData.Subject;
                    originalData.author = saveData.Author;
                    originalData.homepage_momo = saveData.HomepageMomo;
                    originalData.homepage1 = saveData.Homepage1;
                    originalData.homepage2 = saveData.Homepage2;
                    originalData.homepage3 = saveData.Homepage3;
                    originalData.homepage4 = saveData.Homepage4;
                    originalData.homepage5 = saveData.Homepage5;
                    originalData.homepage6 = saveData.Homepage6;
                    originalData.Body = saveData.Body;
                    originalData.postdate = saveData.PostDate;
                    originalData.posttime = saveData.PostTime;
                    originalData.hit = saveData.Hit;
                    originalData.postname = saveData.PostName;
                    originalData.end_date = saveData.EndDate;
                    originalData.post_group = saveData.PostGroup;
                    originalData.post_out = saveData.PostOut;
                    originalData.post_out_txt = saveData.PostOutTxt;
                    originalData.poster_right = saveData.PosterRight;
                    originalData.file1_momo = saveData.File1Momo;
                    originalData.file2_momo = saveData.File2Momo;
                    originalData.file3_momo = saveData.File3Momo;
                    originalData.file4_momo = saveData.File4Momo;
                    originalData.file5_momo = saveData.File5Momo;
                    originalData.created_dept_id = saveData.CreatedDepID;
                    originalData.created_user_name = saveData.CreatedUser;
                    originalData.created_user_phone = saveData.CreatedUserPhone;
                    resultRow = _newsRepository.Update(originalData);
                }
                else
                {
                    an_news newData = new an_news()
                    {
                        Theme = saveData.Theme,
                        Cake = saveData.Cake,
                        Service = saveData.Service,
                        subject = saveData.Subject,
                        author = saveData.Author,
                        homepage_momo = saveData.HomepageMomo,
                        homepage1 = saveData.Homepage1,
                        homepage2 = saveData.Homepage2,
                        homepage3 = saveData.Homepage3,
                        homepage4 = saveData.Homepage4,
                        homepage5 = saveData.Homepage5,
                        homepage6 = saveData.Homepage6,
                        Body = saveData.Body,
                        postdate = saveData.PostDate,
                        posttime = saveData.PostTime,
                        hit = saveData.Hit,
                        postname = saveData.PostName,
                        end_date = saveData.EndDate,
                        post_group = saveData.PostGroup,
                        post_out = saveData.PostOut,
                        post_out_txt = saveData.PostOutTxt,
                        poster_right = saveData.PosterRight,
                        file1_momo = saveData.File1Momo,
                        file2_momo = saveData.File2Momo,
                        file3_momo = saveData.File3Momo,
                        file4_momo = saveData.File4Momo,
                        file5_momo = saveData.File5Momo,
                        created_dept_id = saveData.CreatedDepID,
                        created_user_name = saveData.CreatedUser,
                        created_user_phone = saveData.CreatedUserPhone
                    };
                    resultRow = _newsRepository.Create(newData);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return resultRow > 0;
        }
    }
}
