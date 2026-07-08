using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Timers;
using System.Data;
using Newtonsoft.Json;
using OfficeOpenXml;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Fundbook.Helper
{
    public class BackupHelper
    {
        #region "Daily Backup Manual Download"
        private readonly string _utilityFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility");

        public string GetUtilityFolderAsBase64()
        {
            try
            {
                if (!Directory.Exists(_utilityFolderPath))
                {
                    return string.Empty;
                }

                using (MemoryStream zipStream = new MemoryStream())
                {
                    using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                    {
                        foreach (string filePath in Directory.GetFiles(_utilityFolderPath))
                        {
                            string fileName = Path.GetFileName(filePath);
                            //ZipArchiveEntry entry = archive.CreateEntry(fileName, CompressionLevel.Optimal);
                            ZipArchiveEntry entry = archive.CreateEntry(fileName, System.IO.Compression.CompressionLevel.Optimal);
                            using (Stream entryStream = entry.Open())
                            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                            {
                                fileStream.CopyTo(entryStream);
                            }
                        }
                    }

                    zipStream.Position = 0; // Ensure we're reading from the beginning
                    return Convert.ToBase64String(zipStream.ToArray()); // Encode ZIP as Base64 string
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
                throw ex;
            }
        }
        #endregion

        #region "Automatic Backup via Email"
        private static Timer _timer;
        private static readonly string recipientEmail = "fundbook.2025rkdsb@gmail.com";
        private static readonly string senderEmail = "kamaleshdasitorbital@gmail.com";
        private static readonly string senderPassword = "etnw ihkl pbkq bcni"; // Store securely

        ////public static void StartScheduler()
        ////{
        ////    if (_timer == null)
        ////    {
        ////        _timer = new Timer(GetNextInterval());
        ////        _timer.Elapsed += async (sender, e) => await Task.Run(() => SendBackupEmail());
        ////        _timer.AutoReset = false; // Ensures it runs once per interval
        ////    }

        ////    _timer.Start();
        ////}

        ////private static double GetNextInterval()
        ////{
        ////    TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        ////    DateTime nowUtc = DateTime.UtcNow;
        ////    DateTime nowIst = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, istZone);

        ////    DateTime nextRun = nowIst.Date.AddHours(2).AddMinutes(0); // Set to 2:00 AM IST

        ////    if (nowIst > nextRun)
        ////    {
        ////        nextRun = nextRun.AddDays(1);
        ////    }

        ////    return (nextRun - nowIst).TotalMilliseconds;
        ////}

        ////private static async Task SendBackupEmail()
        ////{
        ////    try
        ////    {
        ////        var backupHelper = new BackupHelper();
        ////        string base64Zip = backupHelper.GetUtilityFolderAsBase64();

        ////        if (string.IsNullOrEmpty(base64Zip))
        ////        {
        ////            return;
        ////        }

        ////        TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        ////        DateTime nowUtc = DateTime.UtcNow;
        ////        DateTime nowIst = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, istZone);

        ////        string uniqueFileName = $"UtilityBackup_{nowIst:yyyyMMddHHmmss}.zip";

        ////        byte[] zipBytes = Convert.FromBase64String(base64Zip);
        ////        await SendEmailWithAttachment(zipBytes, uniqueFileName);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////    finally
        ////    {
        ////        if (_timer != null)
        ////        {
        ////            _timer.Interval = GetNextInterval(); // Get next interval in IST
        ////            _timer.Start();
        ////        }
        ////    }
        ////}

        ////private static async Task SendEmailWithAttachment(byte[] fileBytes, string fileName)
        ////{
        ////    try
        ////    {
        ////        using (MemoryStream ms = new MemoryStream(fileBytes))
        ////        {
        ////            ms.Position = 0; // Reset stream before sending

        ////            using (MailMessage mail = new MailMessage())
        ////            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)) // Update SMTP details
        ////            {
        ////                mail.From = new MailAddress(senderEmail);
        ////                mail.To.Add(recipientEmail);
        ////                mail.Subject = "Daily Backup File";
        ////                mail.Body = "Attached is the daily backup file.";
        ////                mail.Attachments.Add(new Attachment(ms, fileName, "application/zip"));

        ////                smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
        ////                smtp.EnableSsl = true;

        ////                await smtp.SendMailAsync(mail);
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////       throw ex;
        ////    }
        ////}
        ////public static async Task ManualSendBackupEmail()
        ////{
        ////    await SendBackupEmail();
        ////}
        public static void StartScheduler()
        {
            if (_timer == null)
            {
                _timer = new Timer(GetNextInterval());
                _timer.Elapsed += async (sender, e) => await SendBackupEmail();
                _timer.AutoReset = true; // Ensures it continues running
            }

            Console.WriteLine("Backup Scheduler Started...");
            _timer.Start();
        }

        private static double GetNextInterval()
        {
            TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime nowUtc = DateTime.UtcNow;
            DateTime nowIst = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, istZone);

            DateTime nextRun = nowIst.Date.AddHours(2).AddMinutes(0); // Schedule at 2:00 AM IST

            if (nowIst > nextRun)
            {
                nextRun = nextRun.AddDays(1); // Move to next day's 2 AM
            }

            double interval = (nextRun - nowIst).TotalMilliseconds;
            Console.WriteLine($"Next Backup Scheduled at: {nextRun} (IST)");
            return Math.Max(interval, 1000); // Ensure at least 1-second delay
        }

        private static async Task SendBackupEmail()
        {
            try
            {
                Console.WriteLine("Generating Backup File...");

                var backupHelper = new BackupHelper();
                string base64Zip = backupHelper.GetUtilityFolderAsBase64();

                if (string.IsNullOrEmpty(base64Zip))
                {
                    Console.WriteLine("No backup data found. Skipping email.");
                    RestartScheduler();
                    return;
                }

                TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime nowUtc = DateTime.UtcNow;
                DateTime nowIst = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, istZone);

                string uniqueFileName = $"UtilityBackup_{nowIst:yyyyMMddHHmmss}.zip";
                byte[] zipBytes = Convert.FromBase64String(base64Zip);

                await SendEmailWithAttachment(zipBytes, uniqueFileName);

                Console.WriteLine("Backup Email Sent Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in sending email: {ex.Message}");
            }
            finally
            {
                RestartScheduler(); // Restart the timer for the next execution
            }
        }

        private static async Task SendEmailWithAttachment(byte[] fileBytes, string fileName)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(fileBytes))
                {
                    ms.Position = 0;

                    using (MailMessage mail = new MailMessage())
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        mail.From = new MailAddress(senderEmail);
                        mail.To.Add(recipientEmail);
                        mail.Subject = "Daily Backup File";
                        mail.Body = "Attached is the daily backup file.";
                        mail.Attachments.Add(new Attachment(ms, fileName, "application/zip"));

                        smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                        smtp.EnableSsl = true;
                        smtp.Timeout = 20000;

                        Console.WriteLine("Sending Email...");
                        await smtp.SendMailAsync(mail);
                        Console.WriteLine("Email Sent Successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        private static void RestartScheduler()
        {
            _timer.Stop();
            _timer.Interval = GetNextInterval();
            _timer.Start();

            Console.WriteLine("Scheduler restarted for the next run.");
        }

        public static async Task ManualSendBackupEmail()
        {
            Console.WriteLine("Manual Backup Triggered...");
            await SendBackupEmail();
        }
    #endregion

    #region"Download Item json"
    private readonly string _jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "Item.json");

        // Convert JSON to Base64 PDF
        public string GetJsonAsPdfBase64()
        {
            try
            {
                if (!File.Exists(_jsonFilePath))
                {
                    throw new FileNotFoundException("Item.json not found.");
                }

                string jsonData = File.ReadAllText(_jsonFilePath);
                DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(jsonData);

                using (MemoryStream stream = new MemoryStream())
                {
                    Document document = new Document();
                    PdfWriter.GetInstance(document, stream);
                    document.Open();

                    PdfPTable table = new PdfPTable(dataTable.Columns.Count);
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        table.AddCell(new Phrase(column.ColumnName));
                    }

                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (var cell in row.ItemArray)
                        {
                            table.AddCell(new Phrase(cell?.ToString() ?? ""));
                        }
                    }

                    document.Add(table);
                    document.Close();

                    return Convert.ToBase64String(stream.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Convert JSON to Base64 Excel
        public string GetJsonAsExcelBase64()
        {
            try
            {
                if (!File.Exists(_jsonFilePath))
                {
                    throw new FileNotFoundException("Item.json not found.");
                }

                string jsonData = File.ReadAllText(_jsonFilePath);
                DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(jsonData);

                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("ItemData");
                    worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        excelPackage.SaveAs(stream);
                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
