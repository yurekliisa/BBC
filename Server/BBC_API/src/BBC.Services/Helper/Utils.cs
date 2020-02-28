using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Helper
{
    public static class Utils
    {
        public static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() -
                                      new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
        }
    }
}
