using System;
using System.Globalization;

namespace NewShop
{
    public static class Shamsi
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
                pc.GetDayOfMonth(value).ToString("00");
        }
    }
}
