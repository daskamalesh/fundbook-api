using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using Fundbook.Helper;
using System.Threading.Tasks;

namespace Fundbook.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class BackupController : ApiController
    {
        #region "All Files Backup"
        [HttpGet]
        [ActionName("DownloadUtilityBackup")]
        public IHttpActionResult DownloadUtilityBackup()
        {
            try
            {
                var backupHelper = new BackupHelper();
                string base64Zip = backupHelper.GetUtilityFolderAsBase64();

                if (string.IsNullOrEmpty(base64Zip))
                {
                    return Content(HttpStatusCode.NotFound, "No files available for download.");
                }

                TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime nowUtc = DateTime.UtcNow;
                DateTime nowIst = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, istZone);

                string uniqueFileName = $"UtilityBackup_{nowIst:yyyyMMddHHmmss}.zip";

                return Ok(new { ZipData = base64Zip, FileName = uniqueFileName });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region "All Files Backup via Mail Manually"
        [HttpGet]
        [ActionName("ManualDailyBackupViaMail")]
        public async Task<IHttpActionResult> RunBackupManually()
        {
            try
            {
                await Task.Run(() => BackupHelper.ManualSendBackupEmail());
                return Ok("Backup email sent successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region "Download item json"
        [HttpGet]
        [ActionName("DownloadItemBackup")]
        public IHttpActionResult DownloadItemBackup([FromUri] string fileType)
        {
            try
            {
                var backupHelper = new BackupHelper();
                string base64File = "";
                string fileName = "";

                TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime nowUtc = DateTime.UtcNow;
                DateTime nowIst = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, istZone);
                
                if (fileType.ToLower() == "pdf")
                {
                    base64File = backupHelper.GetJsonAsPdfBase64();
                    fileName = $"ItemData{nowIst:yyyyMMddHHmmss}.pdf";
                }
                else if (fileType.ToLower() == "excel")
                {
                    base64File = backupHelper.GetJsonAsExcelBase64();
                    fileName = $"ItemData{nowIst:yyyyMMddHHmmss}.xlsx";
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Invalid file type. Use 'pdf' or 'excel'.");
                }

                if (string.IsNullOrEmpty(base64File))
                {
                    return Content(HttpStatusCode.NotFound, "No data available for download.");
                }

                return Ok(new { FileData = base64File, FileName = fileName });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
