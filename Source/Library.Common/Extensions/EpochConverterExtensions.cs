using System;

namespace Library.Common.Extensions
{
    /// <summary>
    /// Epoch unix converter
    /// </summary>
    public static class EpochConverterExtensions
    {
        /// <summary>
        /// 01.01.1970
        /// </summary>
        public static DateTime EpochStartTime => new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

        /// <summary>
        /// Unix timestamp to dotnet DateTime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(this double unixTimeStamp) => 
            EpochStartTime.AddSeconds(unixTimeStamp).ToLocalTime();

        /// <summary>
        /// Unixes the time stamp to date time from microseconds.
        /// </summary>
        /// <param name="unixTimeStamp">The unix time stamp.</param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTimeFromMicroseconds(this long unixTimeStamp)
            => EpochStartTime.AddTicks(unixTimeStamp * 10);

        /// <summary>
        /// Unix timestamp to dotnet DateTime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(this long unixTimeStamp) =>
            UnixTimeStampToDateTime((double) unixTimeStamp);
        
        /// <summary>
        /// Unix timestamp to dotnet DateTime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(this string unixTimeStamp)
        {
            bool successParse = Int64.TryParse(unixTimeStamp, out var unitTimeStampLong);
            return successParse ? UnixTimeStampToDateTime(unitTimeStampLong) : UnixTimeStampToDateTime(0);
        }
        
        /// <summary>
        /// dotnet DateTime <paramref name="dateTime"/> to unix time stamp
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long DatetimeToUnixTimeStamp(this DateTime dateTime) =>
            (long)Math.Round((dateTime.ToUniversalTime() - EpochStartTime).TotalSeconds);
    }
}
