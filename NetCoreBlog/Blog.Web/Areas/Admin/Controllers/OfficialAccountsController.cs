using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.IService;
using Blog.Common;
using Blog.Models;
using Blog.Web.Filter;
using Blog.Web.Core;
using Senparc.Weixin.MP.Containers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class OfficialAccountsController : AdminBaseController
    {
        private IWC_OfficalAccountsService _officaService;
        public OfficialAccountsController(IWC_OfficalAccountsService officaService)
        {
            _officaService = officaService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [SetAction( ActionName ="index")]
        public IActionResult List(GridPager<WC_OfficalAccounts> pager)
        {

            pager.sort = "CreateTime";
            pager.order = "desc";

            var list  = _officaService.GetPageList(pager, b => true).Entities;
            return Json(new { code = 0, count = pager.totalRows, data = list });
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView();
        }
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult Add(WC_OfficalAccounts model)
        {
            Response res = null;
            if(ModelState.IsValid)
            {
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                try
                {
                    model.AccessToken = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetToken(model.AppId, model.AppSecret).access_token;

                }
                catch (Exception ex)
                {
                    return Json(new Response() { Code = ResponseCode.Fail, Message = "请求微信AccessToken出错," + ex.Message });
                }
                res = _officaService.Add(model);
                if(res.Code== ResponseCode.Success)
                {
                    
                    model.ApiUrl = model.ApiUrl + "?Id=" + model.Id;
                    _officaService.Edit(model);
                }
                if (!AccessTokenContainer.CheckRegistered(model.AppId))
                {
                    AccessTokenContainer.Register(model.AppId, model.AppSecret);
                }
            }
            else
            {
                res = new Common.Response() {  Code= ResponseCode.Fail, Message=Utils.ModelStateMessage(ModelState) };
            }
            return Json(res);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entity = _officaService.Find(o => o.Id == id);
            return PartialView(entity);
        }
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult Edit(WC_OfficalAccounts model)
        {
            Response res = null;
            if (ModelState.IsValid)
            {
                model.UpdateTime = DateTime.Now;
                model.ApiUrl=model.ApiUrl + "?Id=" + model.Id;
                res = _officaService.Edit(model);
            }
            else
            {
                res = new Common.Response() { Code = ResponseCode.Fail, Message = Utils.ModelStateMessage(ModelState) };
            }
            return Json(res);
        }

        public IActionResult Delete(int id) => Json(_officaService.Delete(id));

        [SetAction(ActionName = "setstatus")]
        public IActionResult SetEnable(int id)
        {
            var entity = _officaService.Find(o => o.Id == id);
            Response res = null;
            entity.Enable = !entity.Enable;
            if(_officaService.Edit(entity).Code== ResponseCode.Success)
            {
                res = new Common.Response() { Code= ResponseCode.Success, Message="设置成功！" };
            }
            else
            {
                res = new Common.Response() { Code = ResponseCode.Fail, Message = "设置失败！" };
            }
            return Json(res);
        }
        [SetAction(ActionName = "setstatus")]
        public IActionResult SetDefault(int id)
        {
            return Json(_officaService.SetDefault(id));
        }

        [HttpPost]
        [SetAction(ActionName = "edit")]
        public JsonResult GetToken()
        {

            List<WC_OfficalAccounts> list = _officaService.GetList(o=>o.Enable==true).ToList();
            foreach (var model in list)
            {
                if (!string.IsNullOrEmpty(model.AppId) && !string.IsNullOrEmpty(model.AppSecret))
                {
                    model.AccessToken = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetToken(model.AppId, model.AppSecret).access_token;
                    model.UpdateTime = DateTime.Now;
                    _officaService.Edit( model);
                }
            }

            return Json(true);
        }
    }
    
}
