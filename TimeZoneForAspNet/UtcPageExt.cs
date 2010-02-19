using System;
using System.Web;
using System.Web.UI;

namespace Cprieto.Utils
{
    public static class UtcPageExt
    {
        private const string CookieName = "TimeZoneOffset";

        public static DateTime LocalTimeFromTimeOffset(this Page page, DateTime utcTime) {
        	return page.Request.LocalTimeFromTimeOffset(utcTime);
        }

		public static DateTime LocalTimeFromTimeOffset(this HttpRequest request, DateTime utcTime)
		{
			if (IsCookieDefined(request))
			{
				var offset = GetUtcOffset(request);
				return utcTime.AddMinutes(offset);
			}
			return utcTime;
		}

		public static int UtcOffset(this HttpRequest request)
		{
			if (IsCookieDefined(request))
			{
				var minOffset = GetUtcOffset(request);
				return minOffset / 60; // return offset in hours, not minutes
			}
			return 0;
		}
        public static int UtcOffset(this Page page)
        {
            return UtcOffset( page.Request);
        }

        private static bool IsCookieDefined(HttpRequest request) {
            return request.Cookies[CookieName] != null;
        }

        private static int GetUtcOffset(HttpRequest request) {
            var cookie = request.Cookies[CookieName];
            var offset = (cookie == null) ? 0 : int.Parse(cookie.Value);
            return offset*-1;
        }
    }
}