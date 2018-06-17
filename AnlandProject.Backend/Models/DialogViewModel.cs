using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnlandProject.Backend.Models
{
    public class DialogViewModel
    {
        /// <summary>
        /// 對話框 ID
        /// </summary>
        public string ID { set; get; }

        /// <summary>
        /// 對話框標題
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// 對話框內文
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// 是否提供取消按鈕
        /// </summary>
        public bool OfferCancelButton { set; get; } = true;

        /// <summary>
        /// 確認按鈕文字
        /// </summary>
        public string ConfirmText { set; get; } = "確認";

        /// <summary>
        /// 對話框確認後 POST 的目的 Controller
        /// </summary>
        public string ControllerName { set; get; }

        /// <summary>
        /// 對話框確認後要執行的 Action
        /// </summary>
        public string ActionName { set; get; }

        /// <summary>
        /// ajax request成功後要執行的javascript function
        /// </summary>
        public string OnSuccessFunction { set; get; }

        /// <summary>
        /// ajax request失敗後要執行的javascript function
        /// </summary>
        public string OnFailureFunction { set; get; }
    }
}