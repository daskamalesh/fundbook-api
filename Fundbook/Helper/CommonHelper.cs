using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Fundbook.Models;

namespace Fundbook.Helper
{
    public class CommonHelper
    {
        public DateTime GetCurrentDate()
        {
            try
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "India Standard Time").Date;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DateTime GetCurrentDateTime()
        {
            try
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "India Standard Time");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
