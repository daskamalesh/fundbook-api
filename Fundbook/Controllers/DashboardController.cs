using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Web.Http;
using System.Web.Http.Cors;
using Orbital.TraceHelper;
using Newtonsoft.Json;
using Fundbook.Models;
using Fundbook.Helper;

namespace Fundbook.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class DashboardController : ApiController
    {
        #region "Dashboard"
        [HttpPost]
        [ActionName("GetUtilityDataForDashboard")]
        public IHttpActionResult GetUtilityDataForDashboard(RequestDataEntity postData)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var dashboardHelper = new DashboardHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "GetUtilityDataForDashboard";
            objResult.ResultValue = "0";

            try
            {
                // Call the GetUtilityDataForDashboard method with showActiveOnly flag from postData
                var jsonData = dashboardHelper.GetUtilityDataForDashboard(postData.showActiveOnly);

                if (jsonData != null)
                {
                    return Ok(new { Data = jsonData });
                }
                else
                {
                    objResult.ResultKey = CustomMessage.messageInvalid;
                    objResult.ResultValue = Convert.ToString(CustomMessage.valueInvalid);
                    return Ok(objResult); // Return a message indicating no items found
                }
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex); // Use InternalServerError for exceptions
            }
        }

        [HttpPost]
        [ActionName("GetUtilityDataForDashboardDetailView")]
        public IHttpActionResult GetUtilityDataForDashboardDetailView(RequestDataEntity postData)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var dashboardHelper = new DashboardHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "GetUtilityDataForDashboardDetailView";
            objResult.ResultValue = "0";

            try
            {
                // Call the GetUtilityDataForDashboardDetailView method with showActiveOnly flag from postData
                var jsonData = dashboardHelper.GetUtilityDataForDashboardDetailView(postData.showActiveOnly);

                if (jsonData != null)
                {
                    return Ok(new { Data = jsonData });
                }
                else
                {
                    objResult.ResultKey = CustomMessage.messageInvalid;
                    objResult.ResultValue = Convert.ToString(CustomMessage.valueInvalid);
                    return Ok(objResult); // Return a message indicating no items found
                }
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex); // Use InternalServerError for exceptions
            }
        }

        #endregion

    }
}
