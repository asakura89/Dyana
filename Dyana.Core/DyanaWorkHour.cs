using System;

namespace Dyana.Core {
    public sealed class DyanaWorkHour {
        public Double TotalHours { get; set; }
        public Int32 ResponsibilityHours { get; set; }
        public DateTime EndOfResponsibility { get; set; }
    }
}
