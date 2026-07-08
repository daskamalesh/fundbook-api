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
    public class CommonController : ApiController
    {

        #region "Date"

        [ActionName("GetCurrentDate")]
        public IHttpActionResult GetCurrentDate()
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var commonHelper = new CommonHelper();
            var CustomMessage = new CustomMessage();
            DateTime currentDate;
            objResult.ResultKey = "GetCurrentDate";
            objResult.ResultValue = Convert.ToString(0); // Default value in case of error

            try
            {
                currentDate = commonHelper.GetCurrentDate();
                objResult.ResultValue = currentDate.ToString("dd-MM-yyyy"); // Formatting the date

                return Ok(new
                {
                    Date = objResult.ResultValue
                });
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                objResult.ResultKey = CustomMessage.messageLoginAuthenticationFailed;
                objResult.ResultValue = Convert.ToString(CustomMessage.valueLoginAuthenticationFailed);
                return Ok(objResult); // Return failure result with error message
            }
        }

        [ActionName("GetCurrentDateTime")]
        public IHttpActionResult GetCurrentDateTime()
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var commonHelper = new CommonHelper();
            var CustomMessage = new CustomMessage();
            DateTime currentDateTime;
            objResult.ResultKey = "GetCurrentDateTime";
            objResult.ResultValue = Convert.ToString(0); // Default value in case of error

            try
            {
                currentDateTime = commonHelper.GetCurrentDateTime();
                objResult.ResultValue = currentDateTime.ToString("dd-MM-yyyy HH:mm:ss"); // Formatting date and time

                return Ok(new
                {
                    DateTime = objResult.ResultValue
                });
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                objResult.ResultKey = CustomMessage.messageLoginAuthenticationFailed;
                objResult.ResultValue = Convert.ToString(CustomMessage.valueLoginAuthenticationFailed);
                return Ok(objResult); // Return failure result with error message
            }
        }

        #endregion
    }
}

