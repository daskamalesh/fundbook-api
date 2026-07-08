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
    public class UtilityController : ApiController
    {

        #region "Water"

        [HttpPost]
        [ActionName("AddWater")]
        public IHttpActionResult AddWater(UtilityEntity newItem)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var CustomMessage = new CustomMessage();
            var utilityHelper = new UtilityHelper();
            objResult.ResultKey = "AddWater";
            objResult.ResultValue = "0";

            try
            {
                int newItemId = utilityHelper.AddWater(newItem);
                objResult.ResultValue = Convert.ToString(newItemId);
                return Ok(objResult); // Return the newly created item's ID
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ActionName("GetAllWater")]
        public IHttpActionResult GetAllWater(RequestDataEntity requestData)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var CustomMessage = new CustomMessage();
            var utilityHelper = new UtilityHelper();
            objResult.ResultKey = "GetAllWater";
            objResult.ResultValue = "0";

            try
            {
                List<UtilityEntity> items = utilityHelper.GetAllWater(requestData.showActiveOnly);

                if (items != null)
                {
                    return Ok(items); // Return the list of water items
                }
                else
                {
                    objResult.ResultKey = CustomMessage.messageInvalid;
                    objResult.ResultValue = Convert.ToString(CustomMessage.valueInvalid);
                    return Ok(objResult); // Return message indicating no items found
                }
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [ActionName("EditWater")]
        public IHttpActionResult EditWater(UtilityEntity updatedItem)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var utilityHelper = new UtilityHelper(); // Assuming WaterHelper handles water-related operations
            objResult.ResultKey = "EditWater";
            objResult.ResultValue = "0";

            try
            {
                int updatedItemId = utilityHelper.EditWater(updatedItem);
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
        [ActionName("GetWaterByID")]
        public IHttpActionResult GetWaterByID(int waterID)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var utilityHelper = new UtilityHelper(); // Assuming WaterHelper handles water-related operations
            objResult.ResultKey = "GetWaterByID";

            try
            {
                var waterEntity = utilityHelper.GetWaterByID(waterID);
                if (waterEntity == null)
                {
                    objResult.ResultValue = "Item not found.";
                    return NotFound(); // Item not found
                }
                objResult.ResultValue = JsonConvert.SerializeObject(waterEntity);
                return Ok(objResult); // Successfully fetched item
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex); // Handle the error
            }
        }


        #endregion

        #region "Electric"

        [HttpPost]
        [ActionName("AddElectric")]
        public IHttpActionResult AddElectric(ElectricEntity newItem)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var CustomMessage = new CustomMessage();
            var utilityHelper = new UtilityHelper();
            objResult.ResultKey = "AddElectric";
            objResult.ResultValue = "0";

            try
            {
                int newItemId = utilityHelper.AddElectricUnit(newItem);
                objResult.ResultValue = Convert.ToString(newItemId);
                return Ok(objResult); // Return the newly created item's ID
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ActionName("GetAllElectric")]
        public IHttpActionResult GetAllElectric(RequestDataEntity requestData)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var CustomMessage = new CustomMessage();
            var utilityHelper = new UtilityHelper();
            objResult.ResultKey = "GetAllElectric";
            objResult.ResultValue = "0";

            try
            {
                List<ElectricEntity> items = utilityHelper.GetAllElectricUnit(requestData.showActiveOnly);

                if (items != null)
                {
                    return Ok(items); // Return the list of electric items
                }
                else
                {
                    objResult.ResultKey = CustomMessage.messageInvalid;
                    objResult.ResultValue = Convert.ToString(CustomMessage.valueInvalid);
                    return Ok(objResult); // Return message indicating no items found
                }
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [ActionName("EditElectricUnit")]
        public IHttpActionResult EditElectricUnit(ElectricEntity updatedItem)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var utilityHelper = new UtilityHelper(); // Assuming ElectricHelper handles electric-related operations
            objResult.ResultKey = "EditElectricUnit";
            objResult.ResultValue = "0";

            try
            {
                int updatedItemId = utilityHelper.EditElectricUnit(updatedItem);
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
        [ActionName("GetElectricByID")]
        public IHttpActionResult GetElectricByID(int electricID)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var utilityHelper = new UtilityHelper(); // Assuming ElectricHelper handles electric-related operations
            objResult.ResultKey = "GetElectricByID";

            try
            {
                var electricEntity = utilityHelper.GetElectricByID(electricID);
                if (electricEntity == null)
                {
                    objResult.ResultValue = "Item not found.";
                    return NotFound(); // Item not found
                }
                objResult.ResultValue = JsonConvert.SerializeObject(electricEntity);
                return Ok(objResult); // Successfully fetched item
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex); // Handle the error
            }
        }
        #endregion

        #region "Rent"

        [HttpPost]
        [ActionName("AddRent")]
        public IHttpActionResult AddRent(RentEntity newItem)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var CustomMessage = new CustomMessage();
            var utilityHelper = new UtilityHelper();
            objResult.ResultKey = "AddRent";
            objResult.ResultValue = "0";

            try
            {
                int newItemId = utilityHelper.AddRent(newItem);
                objResult.ResultValue = Convert.ToString(newItemId);
                return Ok(objResult); // Return the newly created item's ID
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ActionName("GetAllRent")]
        public IHttpActionResult GetAllRent(RequestDataEntity requestData)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var CustomMessage = new CustomMessage();
            var utilityHelper = new UtilityHelper();
            objResult.ResultKey = "GetAllRent";
            objResult.ResultValue = "0";

            try
            {
                List<RentEntity> items = utilityHelper.GetAllRent(requestData.showActiveOnly);

                if (items != null)
                {
                    return Ok(items); // Return the list of rent items
                }
                else
                {
                    objResult.ResultKey = CustomMessage.messageInvalid;
                    objResult.ResultValue = Convert.ToString(CustomMessage.valueInvalid);
                    return Ok(objResult); // Return message indicating no items found
                }
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [ActionName("EditRent")]
        public IHttpActionResult EditRent(RentEntity updatedItem)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var utilityHelper = new UtilityHelper(); // Assuming RentHelper handles rent-related operations
            objResult.ResultKey = "EditRent";
            objResult.ResultValue = "0";

            try
            {
                int updatedItemId = utilityHelper.EditRent(updatedItem);
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
        [ActionName("GetRentByID")]
        public IHttpActionResult GetRentByID(int rentID)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var utilityHelper = new UtilityHelper(); // Assuming RentHelper handles rent-related operations
            objResult.ResultKey = "GetRentByID";

            try
            {
                var rentEntity = utilityHelper.GetRentByID(rentID);
                if (rentEntity == null)
                {
                    objResult.ResultValue = "Item not found.";
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

        #endregion

        #region "Grocery"

        [HttpPost]
        [ActionName("AddGrocery")]
        public IHttpActionResult AddGrocery(GroceryEntity newItem)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var CustomMessage = new CustomMessage();
            var utilityHelper = new UtilityHelper();
            objResult.ResultKey = "AddGrocery";
            objResult.ResultValue = "0";

            try
            {
                int newItemId = utilityHelper.AddGrocery(newItem);
                objResult.ResultValue = Convert.ToString(newItemId);
                return Ok(objResult); // Return the newly created item's ID
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ActionName("GetAllGrocery")]
        public IHttpActionResult GetAllGrocery(RequestDataEntity requestData)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var CustomMessage = new CustomMessage();
            var utilityHelper = new UtilityHelper();
            objResult.ResultKey = "GetAllGrocery";
            objResult.ResultValue = "0";

            try
            {
                List<GroceryEntity> items = utilityHelper.GetAllGrocery(requestData.showActiveOnly);

                if (items != null)
                {
                    return Ok(items); // Return the list of rent items
                }
                else
                {
                    objResult.ResultKey = CustomMessage.messageInvalid;
                    objResult.ResultValue = Convert.ToString(CustomMessage.valueInvalid);
                    return Ok(objResult); // Return message indicating no items found
                }
            }
            catch (Exception ex)
            {
                MyTraceHelper.LogException(ex);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ActionName("DeleteGrocery")]
        public IHttpActionResult DeleteGrocery(DeleteDataEntity deleteEntity)
        {
            ReturnResultEntity objResult = new ReturnResultEntity();
            var CustomMessage = new CustomMessage();
            var utilityHelper = new UtilityHelper();
            objResult.ResultKey = "DeleteGrocery";
            objResult.ResultValue = "0";

            try
            {
                // Call the DeleteGrocery method and get the result
                int deletedId = utilityHelper.DeleteGrocery(deleteEntity.ID);

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

    }
}

