using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUDUsingSQLCommand.Models.Commons;
using CRUDUsingSQLCommand.Models;
using CRUDUsingSQLCommand.Models.CRUDStoreProcedures;

namespace CRUDUsingSQLCommand.Controllers
{
    public class CRUDStoreProceduresController : Controller
    {
        public const int DATA_ROW_INDEX_START = 1;
        private CRUDStoreProceduresRepo con = new CRUDStoreProceduresRepo();
        public ActionResult Index()
        {
            int currentPage = 1;
            int recordPerpage = PagingModel.DEFAULT_RECORD_PER_PAGE;

            CRUDStoreProcedures paging = new CRUDStoreProcedures();

            Search(paging, currentPage, recordPerpage);

            return View();
        }

        #region Search
        public ActionResult Search(CRUDStoreProcedures data, int currentPage, int recordPerpage)//IList<UploadExcel2> listData
        {
            AjaxResult ajaxResult = new AjaxResult();
            try
            {

                //long a = con.test(data);
                PagingModel pmodel = new PagingModel(con.getCount(data), currentPage, recordPerpage);
                ViewData["Paging"] = pmodel;

                IList<CRUDStoreProcedures> listData = con.getAllDataUsingProcedure(data, Convert.ToInt32(pmodel.Start), Convert.ToInt32(pmodel.End));
                ViewData["ListData"] = listData;
            }
            catch (Exception ex)
            {
                return Json("Error : " + ex.Message, JsonRequestBehavior.AllowGet);
            }
            return PartialView("_GridView", data);
        }
        #endregion

        #region
        public JsonResult Insert(CRUDStoreProcedures Data)
        {
            AjaxResult ajaxResult = new AjaxResult();
            ajaxResult = Validation(Data);
            RepoResult repoResult = null;

            if (AjaxResult.VALUE_SUCCESS.Equals(ajaxResult.Result))
            {
                repoResult = con.Insert(Data);
            }
            CopyPropertiesRepoToAjaxResult(repoResult, ajaxResult);

            return Json(ajaxResult);
        }

        #region
        public JsonResult Update(CRUDStoreProcedures Data)
        {
            AjaxResult ajaxResult = new AjaxResult();
            ajaxResult = Validation(Data);
            RepoResult repoResult = null;

            if (AjaxResult.VALUE_SUCCESS.Equals(ajaxResult.Result))
            {
                repoResult = con.Update(Data);
            }
            CopyPropertiesRepoToAjaxResult(repoResult, ajaxResult);

            return Json(ajaxResult);
        }
        #endregion

        #region
        public JsonResult Delete(CRUDStoreProcedures Data)
        {
            AjaxResult ajaxResult = new AjaxResult();
            ajaxResult = Validation(Data);
            RepoResult repoResult = null;

            if (AjaxResult.VALUE_SUCCESS.Equals(ajaxResult.Result))
            {
                repoResult = con.Delete(Data);
            }
            CopyPropertiesRepoToAjaxResult(repoResult, ajaxResult);

            return Json(ajaxResult);
        }
        #endregion

        public AjaxResult Validation(CRUDStoreProcedures data)
        {
            AjaxResult ajaxResult = new AjaxResult();

            ajaxResult.ErrMesgs = new string[1];
            ajaxResult.Result = AjaxResult.VALUE_ERROR;

            if (data.Name == null || data.Name == "")
            {
                ajaxResult.ErrMesgs[0] = "ERROR Controller: Name should not be empty";
            }
            else if (data.Email == null || data.Email == "")
            {
                ajaxResult.ErrMesgs[0] = "ERROR Controller: Email should not be empty";
            }
            else
            {
                ajaxResult.Result = AjaxResult.VALUE_SUCCESS;
            }

            return ajaxResult;
        }
        #endregion

        protected void CopyPropertiesRepoToAjaxResult(RepoResult source, AjaxResult dest)
        {
            if (source == null)
            {
                dest = null;
                return;
            }

            if (dest != null && source != null)
            {
                dest.Result = source.Result;
                dest.ProcessId = source.ProcessId;
                dest.SuccMesgs = source.SuccMesgs;
                dest.ErrMesgs = source.ErrMesgs;
                dest.Params = source.Params;
            }
        }
    }
}
