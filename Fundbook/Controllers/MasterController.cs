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

    public class MasterController : ApiController
    {

        IHttpActionResult response;
        #region"Month"

        [HttpPost]
        [ActionName("AddMonth")]
        public IHttpActionResult AddMonth(MasterEntity newItem)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var masterHelper = new MasterHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "AddMonth";
            objResult.ResultValue = "0";

            try
            {
                int newItemId = masterHelper.AddMonth(newItem);
                objResult.ResultValue = Convert.ToString(newItemId);
                return Ok(objResult); // Return the newly created item's ID
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                response = InternalServerError(ex);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ActionName("ConfigureMonth")]
        public IHttpActionResult ConfigureMonth(MasterEntity updatedItem)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var masterHelper = new MasterHelper(); // Assuming RentHelper handles rent-related operations
            objResult.ResultKey = "ConfigureMonth";
            objResult.ResultValue = "0";

            try
            {
                int updatedItemId = masterHelper.ConfigureMonth(updatedItem);
                if (updatedItemId == -1)
                {
                    objResult.ResultValue = "Month not found or error occurred.";
                    return NotFound(); // Item not found or an error occurred
                }
                objResult.ResultValue = Convert.ToString(updatedItemId);
                return Ok(objResult); // Successfully updated
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex); // Handle the error
            }
        }

        [HttpPost]
        [ActionName("GetMonthConfigureByID")]
        public IHttpActionResult GetMonthConfigureByID(ReqPostDataEntity postData)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var masterHelper = new MasterHelper(); // Assuming RentHelper handles rent-related operations
            objResult.ResultKey = "GetMonthConfigureByID";

            try
            {
                var rentEntity = masterHelper.GetMonthConfigureByID(postData.ID);
                if (rentEntity == null)
                {
                    objResult.ResultValue = "Month not found.";
                    return NotFound(); // Item not found
                }
                objResult.ResultValue = JsonConvert.SerializeObject(rentEntity);
                return Ok(objResult); // Successfully fetched item
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex); // Handle the error
            }
        }

        [HttpPost]
        [ActionName("GetAllMonths")]
        public IHttpActionResult GetAllMonths(RequestDataEntity postData) // Use a boolean parameter for filtering
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var masterHelper = new MasterHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "GetAllMonths";
            objResult.ResultValue = "0";

            IHttpActionResult response = null;

            try
            {
                List<MasterEntity> items = masterHelper.GetAllMonths(postData.showActiveOnly);

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

        #endregion

        #region'Target Amount'
        [HttpPost]
        [ActionName("AddTargetAmount")]
        public IHttpActionResult AddTargetAmount(TargetAmountEntity newItem)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var masterHelper = new MasterHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "AddTargetAmount";
            objResult.ResultValue = "0";

            try
            {
                int newItemId = masterHelper.AddTargetAmount(newItem);
                objResult.ResultValue = Convert.ToString(newItemId);
                return Ok(objResult); // Return the newly created item's ID
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                response = InternalServerError(ex);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ActionName("GetAllTargetAmount")]
        public IHttpActionResult GetAllTargetAmount(RequestDataEntity postData) // Use a boolean parameter for filtering
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var masterHelper = new MasterHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "GetAllTargetAmount";
            objResult.ResultValue = "0";

            IHttpActionResult response = null;

            try
            {
                List<TargetAmountEntity> items = masterHelper.GetAllTargetAmount(postData.showActiveOnly);

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
        [HttpPost]
        [ActionName("EditTargetAmount")]
        public IHttpActionResult EditTargetAmount(TargetAmountEntity updatedItem)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var masterHelper = new MasterHelper(); // Assuming RentHelper handles rent-related operations
            objResult.ResultKey = "EditTargetAmount";
            objResult.ResultValue = "0";

            try
            {
                int updatedItemId = masterHelper.EditTargetAmount(updatedItem);
                if (updatedItemId == -1)
                {
                    objResult.ResultValue = "Item not found or error occurred.";
                    return NotFound(); // Item not found or an error occurred
                }
                objResult.ResultValue = Convert.ToString(updatedItemId);
                return Ok(objResult); // Successfully updated
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex); // Handle the error
            }
        }

        [HttpPost]
        [ActionName("GetTargetAmountByID")]
        public IHttpActionResult GetTargetAmountByID(ReqPostDataEntity postData)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var masterHelper = new MasterHelper(); // Assuming RentHelper handles rent-related operations
            objResult.ResultKey = "GetTargetAmountByID";

            try
            {
                var rentEntity = masterHelper.GetTargetAmountByID(postData.ID);
                if (rentEntity == null)
                {
                    objResult.ResultValue = "Target Amount not found.";
                    return NotFound(); // Item not found
                }
                objResult.ResultValue = JsonConvert.SerializeObject(rentEntity);
                return Ok(objResult); // Successfully fetched item
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex); // Handle the error
            }
        }

        [HttpPost]
        [ActionName("DeleteTargetAmount")]
        public IHttpActionResult DeleteTargetAmount(DeleteDataEntity deleteEntity)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var CustomMessage = new CustomMessage();
            var masterHelper = new MasterHelper();
            objResult.ResultKey = "DeleteTargetAmount";
            objResult.ResultValue = "0";

            try
            {
                // Call the DeleteGrocery method and get the result
                int deletedId = masterHelper.DeleteTargetAmount(deleteEntity.ID);

                if (deletedId > 0)
                {
                    objResult.ResultKey = "Deleted Successfully.";
                    objResult.ResultValue = Convert.ToString(deletedId);
                    return Ok(objResult); // Return success message with deleted item's ID
                }
                else
                {
                    objResult.ResultKey = CustomMessage.messageInvalid;
                    objResult.ResultValue = Convert.ToString(CustomMessage.valueInvalid);
                    return Ok(objResult); // Return message indicating the item was not found
                }
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex); // Return error response in case of an exception
            }
        }
        #endregion

        #region'Quantity Type'
        [HttpPost]
        [ActionName("AddQuantityType")]
        public IHttpActionResult AddQuantityType(QuantityTypeEntity newItem)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var masterHelper = new MasterHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "AddQuantityType";
            objResult.ResultValue = "0";

            try
            {
                int newItemId = masterHelper.AddQuantityType(newItem);
                objResult.ResultValue = Convert.ToString(newItemId);
                return Ok(objResult); // Return the newly created item's ID
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                response = InternalServerError(ex);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ActionName("GetAllQuantityType")]
        public IHttpActionResult GetAllQuantityType(RequestDataEntity postData) // Use a boolean parameter for filtering
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var masterHelper = new MasterHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "GetAllQuantityType";
            objResult.ResultValue = "0";

            IHttpActionResult response = null;

            try
            {
                List<QuantityTypeEntity> items = masterHelper.GetAllQuantityType(postData.showActiveOnly);

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

        #endregion
    }
}
