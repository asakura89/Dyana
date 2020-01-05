using System;
using System.Text.RegularExpressions;

namespace Dyana.Core {
    public static class D {
        public static DyanaWorkHour CalculateWorkHours(String arriveTime, String departTime, String workHour, String breakHour) {
            var digitRgx = new Regex(@"\d+", RegexOptions.Compiled);

            if (String.IsNullOrEmpty(arriveTime) || !arriveTime.Contains(":"))
                throw new InvalidOperationException("Arrive hours is not valid.");
            String[] arrive = arriveTime.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (arrive.Length < 2)
                throw new InvalidOperationException("Arrive hours is not valid.");
            if (!digitRgx.IsMatch(arrive[0]) || !digitRgx.IsMatch(arrive[1]))
                throw new InvalidOperationException("Arrive hours is not valid.");
            Int32 arrhour = Convert.ToInt32(arrive[0]);
            Int32 arrmin = Convert.ToInt32(arrive[1]);
            if (arrhour < 0 || arrhour > 23)
                throw new InvalidOperationException("Arrive hours is not valid.");
            if (arrmin < 0 || arrmin > 59)
                throw new InvalidOperationException("Arrive minutes is not valid.");

            if (String.IsNullOrEmpty(departTime) || !departTime.Contains(":"))
                throw new InvalidOperationException("Depart hours is not valid.");
            String[] depart = departTime.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (depart.Length < 2)
                throw new InvalidOperationException("Depart hours is not valid.");
            if (!digitRgx.IsMatch(depart[0]) || !digitRgx.IsMatch(depart[1]))
                throw new InvalidOperationException("Depart hours is not valid.");
            Int32 dephour = Convert.ToInt32(depart[0]);
            Int32 depmin = Convert.ToInt32(depart[1]);
            if (dephour < 0 || dephour > 23)
                throw new InvalidOperationException("Depart hours is not valid.");
            if (depmin < 0 || depmin > 59)
                throw new InvalidOperationException("Depart minutes is not valid.");

            if (String.IsNullOrEmpty(workHour) || !digitRgx.IsMatch(workHour))
                throw new InvalidOperationException("Work hour is not valid.");
            if (String.IsNullOrEmpty(breakHour) || !digitRgx.IsMatch(breakHour))
                throw new InvalidOperationException("Break is not valid.");

            Int32 workhour = Convert.ToInt32(workHour);
            Int32 breakhour = Convert.ToInt32(breakHour);
            Int32 responsibility = workhour + breakhour;

            DateTime now = DateTime.Now;
            var start = new DateTime(now.Year, now.Month, now.Day, arrhour, arrmin, 00);
            var end = new DateTime(now.Year, now.Month, now.Day, dephour, depmin, 00);
            TimeSpan total = end.Subtract(start);

            return new DyanaWorkHour {
                TotalHours = total.Hours,
                TotalMins = total.Minutes,
                ResponsibilityHours = responsibility,
                EndOfResponsibility = start.AddHours(responsibility)
            };
        }

        public static String GetAppropriateMessage(String arriveTime, String departTime, String workHour, String breakHour) {
            DyanaWorkHour wh = CalculateWorkHours(arriveTime, departTime, workHour, breakHour);
            return GetAppropriateMessage(wh);
        }

        public static String GetAppropriateMessage(DyanaWorkHour wh) {
            if (Math.Abs(wh.TotalHours) == Math.Abs(wh.ResponsibilityHours))
                return "It's time to go home :)";
            if (wh.TotalHours < wh.ResponsibilityHours)
                return $"Oy! Work more! should be at {wh.EndOfResponsibility.ToString("HH:mm")}";
            if (wh.TotalHours > wh.ResponsibilityHours && wh.TotalHours > wh.ResponsibilityHours +1)
                return $"Overtimeeee by {wh.TotalHours-wh.ResponsibilityHours}h {wh.TotalMins}m Waaaaaaattttt...";

            return "Just go home already!";
        }
    }
}
