using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Qt.Core;
using Qt.Domain.Entity;
using Qt.Web.ControllerHelpers;
using Qt.Web.Models;

namespace Qt.Web.Controllers
{
    public class QueryController : QtBaseController
    {
        
        public ActionResult Index()
        {
            var model = GetBaseModel();
            LoadViewBag(model);
            return View(model);
        }

        public ActionResult Show([DataSourceRequest] DataSourceRequest request)
        {
            var queries = new QueryManagementHelper().GetQueryList();

            return Json(queries.ToDataSourceResult(request));
        }

        public ActionResult Group(long groupId)
        {
            var model = GetBaseModel(groupId);
            LoadViewBag(model);
            return View("Index",model);
        }

        public ActionResult ShowGroup([DataSourceRequest] DataSourceRequest request,int groupId)
        {
            var queries = new QueryManagementHelper().GetQueryListForGroup(groupId);

            return Json(queries.ToDataSourceResult(request));
        }

        //
        // POST: /Query/Create
        [HttpPost]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request)
        {
            var q = new Query();

            if (TryUpdateModel(q, includeProperties: new string[] { "Name", "Text", "Description" }))
            {
                new QueryManagementHelper().CreateQuery(q);
            }
            return Json(new[] { q }.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, long id)
        {
            var queryManager = new QueryManagementHelper();
            var queryToUpdate = queryManager.GetQuery(id);
            if (TryUpdateModel(queryToUpdate, includeProperties: new string[] { "Name", "Text","Description" }))
            {
                queryManager.UpdateQuery(queryToUpdate);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        //
        // POST: /Query/Delete/5
        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, int id)
        {
            var deleted = new QueryManagementHelper().DeleteQuery(id);
            return Json(ModelState.ToDataSourceResult());
        }

        public QueryMainModel GetBaseModel()
        {
            var allGroups = ConverGroupList(new QueryManagementHelper().GetAllGroups());
            var model = new QueryMainModel() { AllGroups = allGroups};
            return model;
        }

        public QueryMainModel GetBaseModel(long groupId)
        {
            var group = new QueryManagementHelper().GetQueryGroup(groupId);
            var allGroups = ConverGroupList(new QueryManagementHelper().GetAllGroups());

            var model = new QueryMainModel()
                            {
                                GroupId = group.Id, 
                                GroupName = group.Name,
                                AllGroups = allGroups
                            };
            
            return model;
        }

        private void LoadViewBag(QueryMainModel model)
        {

            model.AllGroups.Insert(0, new BaseDto() { Id = -1, Name = "Select Group" });
            ViewBag.GroupList = new SelectList(model.AllGroups,"Id","Name");
        }

        private List<BaseDto> ConverGroupList(List<QueryGroup> groups )
        {
            return groups.Select(q => new BaseDto() {Id = q.Id, Name = q.Name}).ToList();
        }

    }
}
