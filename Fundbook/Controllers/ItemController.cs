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

    public class ItemController : ApiController
    {

        IHttpActionResult response;
        #region"Add Item"

        [HttpPost]
        [ActionName("AddItem")]
        public IHttpActionResult AddItem(ItemEntity newItem)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var itemHelper = new ItemHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "AddItem";
            objResult.ResultValue = "0";

            try
            {
                int newItemId = itemHelper.AddItem(newItem);
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
        [ActionName("GetAllItems")]
        public IHttpActionResult GetAllItems(RequestDataEntity newItem) // Use a boolean parameter for filtering
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var itemHelper = new ItemHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "GetAllItems";
            objResult.ResultValue = "0";

            IHttpActionResult response = null;

            try
            {
                List<ItemEntity> items = itemHelper.GetAllItems(newItem.showActiveOnly);

                if (items != null)
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
        [ActionName("GetItemsMonthWise")]
        public IHttpActionResult GetItemsMonthWise(ItemEntity newItem) // Use a boolean parameter for filtering
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var itemHelper = new ItemHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "GetItemsMonthWise";
            objResult.ResultValue = "0";

            IHttpActionResult response = null;

            try
            {
                List<ItemEntity> items = itemHelper.GetItemsMonthWise(newItem.MonthID, newItem.TransactionTypeID);

                if (items != null)
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
        [ActionName("GetItemsMonthUserWise")]
        public IHttpActionResult GetItemsMonthUserWise(ItemEntity newItem) // Use a boolean parameter for filtering
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var itemHelper = new ItemHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "GetItemsMonthUserWise";
            objResult.ResultValue = "0";

            IHttpActionResult response = null;

            try
            {
                List<ItemEntity> items = itemHelper.GetItemsMonthUserWise(newItem.MonthID, newItem.UserID, newItem.TransactionTypeID);

                if (items != null)
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
        [ActionName("GetItemsIndividualUserWise")]
        public IHttpActionResult GetItemsIndividualUserWise() // Use a boolean parameter for filtering
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var itemHelper = new ItemHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "GetItemsIndividualUserWise";
            objResult.ResultValue = "0";

            IHttpActionResult response = null;

            try
            {
                List<ItemSummaryEntity> items = itemHelper.GetItemsIndividualUserWise();

                if (items != null)
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
        [ActionName("GetSettlementData")]
        public IHttpActionResult GetSettlementData() // Use a boolean parameter for filtering
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var itemHelper = new ItemHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "GetSettlementData";
            objResult.ResultValue = "0";

            IHttpActionResult response = null;

            try
            {
                List<ItemSettlementEntity> items = itemHelper.GetSettlementData();

                if (items != null)
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
        [ActionName("GetSettlementDataUserWise")]
        public IHttpActionResult GetSettlementDataUserWise() // Use a boolean parameter for filtering
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var itemHelper = new ItemHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "GetSettlementDataUserWise";
            objResult.ResultValue = "0";

            IHttpActionResult response = null;

            try
            {
                List<UserSettlementEntity> items = itemHelper.GetSettlementDataUserWise();

                if (items != null)
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
        [ActionName("AddSettlementData")]
        public IHttpActionResult AddSettlementData(ItemEntity newItem)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var itemHelper = new ItemHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "AddSettlementData";
            objResult.ResultValue = "0";

            try
            {
                int newItemId = itemHelper.AddSettlementData(newItem);
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
        [ActionName("GetSettlementDataByUserID")]
        public IHttpActionResult GetSettlementDataByUserID(ItemPostDataEntity postData) // Use a boolean parameter for filtering
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var itemHelper = new ItemHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "GetSettlementDataByUserID";
            objResult.ResultValue = "0";

            IHttpActionResult response = null;

            try
            {
                List<ItemEntity> items = itemHelper.GetSettlementDataByUserID(postData.UserID);

                if (items != null)
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
        [ActionName("GetSettlementProcessRequestData")]
        public IHttpActionResult GetSettlementProcessRequestData()
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var itemHelper = new ItemHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "GetSettlementProcessRequestData";
            objResult.ResultValue = "0";

            IHttpActionResult response = null;

            try
            {
                List<ItemEntity> items = itemHelper.GetSettlementProcessRequestData();

                if (items != null)
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
        [ActionName("EditSettlementData")]
        public IHttpActionResult EditSettlementData(ItemPostDataEntity postData) // Use a boolean parameter for filtering
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var itemHelper = new ItemHelper();
            var CustomMessage = new CustomMessage();

            objResult.ResultKey = "EditSettlementData";
            objResult.ResultValue = "0";

            IHttpActionResult response = null;

            try
            {
                int result = itemHelper.EditSettlementData(postData.ItemID);

                if (result != 0)
                {
                    return Ok(result); // Return the ItemID on success
                }
                else
                {
                    objResult.ResultKey = CustomMessage.messageInvalid;
                    objResult.ResultValue = Convert.ToString(CustomMessage.valueInvalid);
                    return Ok(objResult); // Return a message indicating no items found or edit failed
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
