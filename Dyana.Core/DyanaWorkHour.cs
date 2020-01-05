using System;

namespace Dyana.Core {
    public sealed class DyanaWorkHour {
        public Int32 TotalHours { get; set; }
        public Int32 TotalMins { get; set; }
        public Int32 ResponsibilityHours { get; set; }
        public DateTime EndOfResponsibility { get; set; }
    }
}
