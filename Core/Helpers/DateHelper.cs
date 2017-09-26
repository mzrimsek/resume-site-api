using System;

namespace Core.Helpers
{
    public static class DateHelper
    {
        public static string FormatDate(DateTime date)
        {
            return date.ToString("M/d/yyyy");
        }
    }
}