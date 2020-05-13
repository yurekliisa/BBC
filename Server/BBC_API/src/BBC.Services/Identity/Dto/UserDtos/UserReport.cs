using System.Collections.Generic;

namespace BBC.Services.Identity.Dto.UserDtos
{
    public class UserReport
    {
        public UserReportHeaderWidget HeaderWidget { get; set; }
        public List<UserReportMonthlyTAR> MonthlyTAR { get; set; }
        public UserReport()
        {
            MonthlyTAR = new List<UserReportMonthlyTAR>();
        }
    }
}
