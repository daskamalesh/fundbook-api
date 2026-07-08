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

    public class AuthenticationController : ApiController
    {
        private readonly string _loginPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Authentication.json");

        IHttpActionResult response;
        #region"Login Authenticate"

        [ActionName("LoginAuthenticate")]
        public IHttpActionResult LoginAuthenticate(MembershipFilterEntity postData)
        {

            ReturnResultEntity objResult = new ReturnResultEntity();
            var loginHelper = new LoginHelper();
            var CustomMessage = new CustomMessage();
            int returnResult = 0;
            objResult.ResultKey = "LoginAuthenticate";
            objResult.ResultValue = Convert.ToString(returnResult);
            try
            {
                if (postData != null)
                {
                    postData.UserName = postData.UserName.Replace(" ", "+");
                    postData.Password = postData.Password.Replace(" ", "+");

                    var objEntity = loginHelper.AuthenticateUser(postData.UserName, postData.Password);

                    if (objEntity != null)
                    {
                        return Ok(new
                        {
                            Message = "Login successful",
                            UserID = objEntity
                        });
                    }
                    else
                    {
                        objResult.ResultKey = CustomMessage.messageLoginAuthenticationFailed; // "Login authentication failed";
                        objResult.ResultValue = Convert.ToString(CustomMessage.valueLoginAuthenticationFailed);
                        return Ok(objResult);
                    }
                }
                else
                {
                    objResult.ResultKey = CustomMessage.messageLoginAuthenticationFailed; // "Login authentication failed";
                    objResult.ResultValue = Convert.ToString(CustomMessage.valueLoginAuthenticationFailed);
                    return Ok(objResult);
                }
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                response = BadRequest(ex.InnerException.ToString());
            }
            return response;
        }

        [HttpPost]
        [ActionName("LoginAuthenticateDetail")]
        public IHttpActionResult LoginAuthenticateDetail(AuthenticationDetailEntity postData)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var loginHelper = new LoginHelper();
            var CustomMessage = new CustomMessage();
            int returnResult = 0;
            objResult.ResultKey = "LoginAuthenticateDetail";
            objResult.ResultValue = Convert.ToString(returnResult);
            try
            {
                if (postData != null)
                {
                    postData.UserID = postData.UserID;

                    var objEntity = loginHelper.LoginAuthenticateDetail(postData.UserID);

                    if (objEntity != null)
                    {
                        return Ok(objEntity);
                    }
                    else
                    {
                        objResult.ResultKey = CustomMessage.messageLoginAuthenticationFailed; // "Login authentication failed";
                        objResult.ResultValue = Convert.ToString(CustomMessage.valueLoginAuthenticationFailed);
                        return Ok(objResult);
                    }
                }
                else
                {
                    objResult.ResultKey = CustomMessage.messageLoginAuthenticationFailed; // "Login authentication failed";
                    objResult.ResultValue = Convert.ToString(CustomMessage.valueLoginAuthenticationFailed);
                    return Ok(objResult);
                }
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                response = BadRequest(ex.InnerException.ToString());
            }
            return response;
        }

        [HttpPost]
        [ActionName("FetchLoginMenuRole")]
        public IHttpActionResult FetchLoginMenuRole(RequestDataEntity postData)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var loginHelper = new LoginHelper();
            var CustomMessage = new CustomMessage();
            int returnResult = 0;
            objResult.ResultKey = "FetchLoginMenuRole";
            objResult.ResultValue = Convert.ToString(returnResult);
            try
            {
                if (postData.LoginRoleID > 0)
                {

                    var objEntity = loginHelper.FetchLoginMenuRole(postData.LoginRoleID);

                    if (objEntity != null)
                    {
                        return Ok(objEntity);
                    }
                    else
                    {
                        objResult.ResultKey = CustomMessage.messageLoginAuthenticationFailed; // "Login authentication failed";
                        objResult.ResultValue = Convert.ToString(CustomMessage.valueLoginAuthenticationFailed);
                        return Ok(objResult);
                    }
                }
                else
                {
                    objResult.ResultKey = CustomMessage.messageLoginAuthenticationFailed; // "Login authentication failed";
                    objResult.ResultValue = Convert.ToString(CustomMessage.valueLoginAuthenticationFailed);
                    return Ok(objResult);
                }
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                response = BadRequest(ex.InnerException.ToString());
            }
            return response;
        }
        #endregion
        [HttpPost]
        [ActionName("AddUser")]
        public IHttpActionResult AddUser(MembershipFilterEntity newUser)
        {
            try
            {
                var loginHelper = new LoginHelper();
                int result = loginHelper.AddUser(newUser);

                if (result == -1)
                {
                    return Conflict(); // Return conflict if UserID already exists
                }
                else if (result > 0)
                {
                    return Ok(new { Message = "User added successfully.", UserID = result });
                }
                else
                {
                    return BadRequest("Failed to add user.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Update user name
        [HttpPost]
        [ActionName("UpdateUserName")]
        public IHttpActionResult UpdateUserName(UpdateCredDataEntity postData)
        {
            try
            {
                var loginHelper = new LoginHelper();
                int result = loginHelper.UpdateUserName(postData.UserID, postData.NewUserName);

                if (result > 0)
                {
                    return Ok(new { Message = "User name updated successfully.", UserID = result });
                }
                else
                {
                    return NotFound(); // Return 404 if user not found
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Update user password
        [HttpPost]
        [ActionName("UpdatePassword")]
        public IHttpActionResult UpdatePassword(UpdateCredDataEntity postData)
        {
            try
            {
                var loginHelper = new LoginHelper();
                int result = loginHelper.UpdatePassword(postData.UserID, postData.NewPassword);

                if (result > 0)
                {
                    return Ok(new { Message = "Password updated successfully.", UserID = result });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ActionName("GetAllUser")]
        public IHttpActionResult GetAllUser() // Use a boolean parameter for filtering
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var loginHelper = new LoginHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "GetAllUser";
            objResult.ResultValue = "0";

            IHttpActionResult response = null;

            try
            {
                List<AuthenticationDetailEntity> items = loginHelper.GetAllUser();

                if (items != null && items.Count > 0)
                {
                    return Ok(items); // Return the list of items
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
                response = InternalServerError(ex); // Use InternalServerError for exceptions
            }

            return response;
        }


    }
}
