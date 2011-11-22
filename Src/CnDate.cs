/**
 * file depends: 
 * 
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-11-21 Created 
 * */
using System;

namespace Src
{
    /// <summary>
    /// 农历（中国日历）
    /// </summary>
    public class CnDate
    {
        /// <summary>
        /// 初始化<see cref="CnDate"/>的实例
        /// </summary>
        /// <param name="date"><see cref="DateTime"/>实例</param>
        public CnDate(DateTime date)
        {
            Source = date;
            Compute();
        }

        private void Compute()
        {
            ChineseDate chinese = new ChineseDate(Source);
            chinese.l_CalcLunarDate();
            Year = chinese.FormatLunarYear();
            Month = chinese.FormatMonth();
            Day = chinese.FormatLunarDay();
            Constellation = chinese.GetConstellationName();
            SolarTerm = chinese.GetLunarHolDayName();
            IsSolarTerm = SolarTerm != null;
            Week = GetCnWeek(Source.DayOfWeek);
        }

        private string GetCnWeek(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    return "星期一";
                case DayOfWeek.Tuesday:
                    return "星期二";
                case DayOfWeek.Wednesday:
                    return "星期三";
                case DayOfWeek.Thursday:
                    return "星期四";
                case DayOfWeek.Friday:
                    return "星期五";
                case DayOfWeek.Saturday:
                    return "星期六";
                default:
                    return "星期日";
            }
        }

        /// <summary>
        /// 返回农历月份（正月、二月...冬月、腊月）
        /// </summary>
        public string Month { get; private set; }

        /// <summary>
        /// 返回农历日（初一...）
        /// </summary>
        public string Day { get; private set; }

        /// <summary>
        /// 返回农历年份（甲子年、乙丑年...）
        /// </summary>
        public string Year { get; private set; }

        /// <summary>
        /// 返回星期（星期一、星期二...星期日）
        /// </summary>
        public string Week { get; private set; }

        /// <summary>
        /// 如果是节气，返回节气名（立春、雨水...），否则返回 null
        /// </summary>
        public string SolarTerm { get; private set; }

        /// <summary>
        /// 是否是节气
        /// </summary>
        public bool IsSolarTerm { get; private set; }

        /// <summary>
        /// 获取星座
        /// </summary>
        public string Constellation { get; private set; }

        /// <summary>
        /// 返回 DateTime 源数据
        /// </summary>
        public DateTime Source { get; private set; }

        /// <summary>
        /// 返回 XX年 X月X日, 星期X
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Format("Y MD, W");
        }

        /// <summary>
        /// 使用指定格式化<see cref="CnDate"/>对象
        /// </summary>
        /// <param name="format">格式化字符串
        /// <remarks>
        /// <c>Y</c>年
        /// <c>M</c>月
        /// <c>D</c>日
        /// <c>W</c>星期
        /// <c>S</c>节气
        /// <c>C</c>星座
        /// </remarks>
        /// </param>
        /// <returns>格式化数据</returns>
        public string ToString(string format)
        {
            return Format(format);
        }

        private string Format(string format)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }
            string result = format;
            return result.Replace("Y", Year)
                .Replace("M", Month)
                .Replace("D", Day)
                .Replace("W", Week)
                .Replace("S", SolarTerm)
                .Replace("C", Constellation);
        }

        #region Nested type: ChineseDate

        /// <summary>
        /// 实现日期转换，摘自网络。
        /// </summary>
        private class ChineseDate
        {
            private const ushort END_YEAR = 2050;
            private const ushort START_YEAR = 1901;

            private DateTime m_Date;

            #region Data

            private readonly string[] ConstellationName =
                {
                    "白羊座", "金牛座", "双子座",
                    "巨蟹座", "狮子座", "处女座",
                    "天秤座", "天蝎座", "射手座",
                    "摩羯座", "水瓶座", "双鱼座"
                };

            //数组gLanarHoliDay存放每年的二十四节气对应的阳历日期 
            //每年的二十四节气对应的阳历日期几乎固定，平均分布于十二个月中 
            // 1月 2月 3月 4月 5月 6月 
            //小寒 大寒 立春 雨水 惊蛰 春分 清明 谷雨 立夏 小满 芒种 夏至 
            // 7月 8月 9月 10月 11月 12月 
            //小暑 大暑 立秋 处暑 白露 秋分 寒露 霜降 立冬 小雪 大雪 冬至 
            //********************************************************************************* 
            // 节气无任何确定规律,所以只好存表,要节省空间,所以.... 
            //**********************************************************************************} 
            //数据格式说明: 
            //如1901年的节气为 
            // 1月 2月 3月 4月 5月 6月 7月 8月 9月 10月 11月 12月 
            // 6, 21, 4, 19, 6, 21, 5, 21, 6,22, 6,22, 8, 23, 8, 24, 8, 24, 8, 24, 8, 23, 8, 22 
            // 9, 6, 11,4, 9, 6, 10,6, 9,7, 9,7, 7, 8, 7, 9, 7, 9, 7, 9, 7, 8, 7, 15 
            //上面第一行数据为每月节气对应日期,15减去每月第一个节气,每月第二个节气减去15得第二行 
            // 这样每月两个节气对应数据都小于16,每月用一个字节存放,高位存放第一个节气数据,低位存放 
            //第二个节气的数据,可得下表 
            #region 节气数据


            private readonly byte[] gLunarHolDay = {
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1901 
                                                       0x96, 0xA4, 0x96, 0x96, 0x97, 0x87, 0x79, 0x79, 0x79, 0x69, 0x78,
                                                       0x78, //1902 
                                                       0x96, 0xA5, 0x87, 0x96, 0x87, 0x87, 0x79, 0x69, 0x69, 0x69, 0x78,
                                                       0x78, //1903 
                                                       0x86, 0xA5, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x79, 0x78,
                                                       0x87, //1904 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1905 
                                                       0x96, 0xA4, 0x96, 0x96, 0x97, 0x97, 0x79, 0x79, 0x79, 0x69, 0x78,
                                                       0x78, //1906 
                                                       0x96, 0xA5, 0x87, 0x96, 0x87, 0x87, 0x79, 0x69, 0x69, 0x69, 0x78,
                                                       0x78, //1907 
                                                       0x86, 0xA5, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x69, 0x78,
                                                       0x87, //1908 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1909 
                                                       0x96, 0xA4, 0x96, 0x96, 0x97, 0x97, 0x79, 0x79, 0x79, 0x69, 0x78,
                                                       0x78, //1910 
                                                       0x96, 0xA5, 0x87, 0x96, 0x87, 0x87, 0x79, 0x69, 0x69, 0x69, 0x78,
                                                       0x78, //1911 
                                                       0x86, 0xA5, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x69, 0x78,
                                                       0x87, //1912 
                                                       0x95, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1913 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x79, 0x79, 0x79, 0x69, 0x78,
                                                       0x78, //1914 
                                                       0x96, 0xA5, 0x97, 0x96, 0x97, 0x87, 0x79, 0x79, 0x69, 0x69, 0x78,
                                                       0x78, //1915 
                                                       0x96, 0xA5, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x79, 0x77,
                                                       0x87, //1916 
                                                       0x95, 0xB4, 0x96, 0xA6, 0x96, 0x97, 0x78, 0x79, 0x78, 0x69, 0x78,
                                                       0x87, //1917 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x79, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1918 
                                                       0x96, 0xA5, 0x97, 0x96, 0x97, 0x87, 0x79, 0x79, 0x69, 0x69, 0x78,
                                                       0x78, //1919 
                                                       0x96, 0xA5, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x79, 0x77,
                                                       0x87, //1920 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x78, 0x79, 0x78, 0x69, 0x78,
                                                       0x87, //1921 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x79, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1922 
                                                       0x96, 0xA4, 0x96, 0x96, 0x97, 0x87, 0x79, 0x79, 0x69, 0x69, 0x78,
                                                       0x78, //1923 
                                                       0x96, 0xA5, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x79, 0x77,
                                                       0x87, //1924 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x78, 0x79, 0x78, 0x69, 0x78,
                                                       0x87, //1925 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1926 
                                                       0x96, 0xA4, 0x96, 0x96, 0x97, 0x87, 0x79, 0x79, 0x79, 0x69, 0x78,
                                                       0x78, //1927 
                                                       0x96, 0xA5, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x78, 0x78, 0x78, 0x87,
                                                       0x87, //1928 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x79, 0x77,
                                                       0x87, //1929 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1930 
                                                       0x96, 0xA4, 0x96, 0x96, 0x97, 0x87, 0x79, 0x79, 0x79, 0x69, 0x78,
                                                       0x78, //1931 
                                                       0x96, 0xA5, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x78, 0x78, 0x78, 0x87,
                                                       0x87, //1932 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x69, 0x78,
                                                       0x87, //1933 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1934 
                                                       0x96, 0xA4, 0x96, 0x96, 0x97, 0x97, 0x79, 0x79, 0x79, 0x69, 0x78,
                                                       0x78, //1935 
                                                       0x96, 0xA5, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x78, 0x78, 0x78, 0x87,
                                                       0x87, //1936 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x69, 0x78,
                                                       0x87, //1937 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1938 
                                                       0x96, 0xA4, 0x96, 0x96, 0x97, 0x97, 0x79, 0x79, 0x79, 0x69, 0x78,
                                                       0x78, //1939 
                                                       0x96, 0xA5, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x78, 0x78, 0x78, 0x87,
                                                       0x87, //1940 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x69, 0x78,
                                                       0x87, //1941 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1942 
                                                       0x96, 0xA4, 0x96, 0x96, 0x97, 0x97, 0x79, 0x79, 0x79, 0x69, 0x78,
                                                       0x78, //1943 
                                                       0x96, 0xA5, 0x96, 0xA5, 0xA6, 0x96, 0x88, 0x78, 0x78, 0x78, 0x87,
                                                       0x87, //1944 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x79, 0x77,
                                                       0x87, //1945 
                                                       0x95, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x78, 0x69, 0x78,
                                                       0x77, //1946 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x79, 0x79, 0x79, 0x69, 0x78,
                                                       0x78, //1947 
                                                       0x96, 0xA5, 0xA6, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x78, 0x78, 0x87,
                                                       0x87, //1948 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x79, 0x78, 0x79, 0x77,
                                                       0x87, //1949 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x78, 0x79, 0x78, 0x69, 0x78,
                                                       0x77, //1950 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x79, 0x79, 0x79, 0x69, 0x78,
                                                       0x78, //1951 
                                                       0x96, 0xA5, 0xA6, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x78, 0x78, 0x87,
                                                       0x87, //1952 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x79, 0x77,
                                                       0x87, //1953 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x78, 0x79, 0x78, 0x68, 0x78,
                                                       0x87, //1954 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1955 
                                                       0x96, 0xA5, 0xA5, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x78, 0x78, 0x87,
                                                       0x87, //1956 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x79, 0x77,
                                                       0x87, //1957 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x69, 0x78,
                                                       0x87, //1958 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1959 
                                                       0x96, 0xA4, 0xA5, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //1960 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x78, 0x78, 0x78, 0x87,
                                                       0x87, //1961 
                                                       0x96, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x69, 0x78,
                                                       0x87, //1962 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1963 
                                                       0x96, 0xA4, 0xA5, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //1964 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x78, 0x78, 0x78, 0x87,
                                                       0x87, //1965 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x69, 0x78,
                                                       0x87, //1966 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1967 
                                                       0x96, 0xA4, 0xA5, 0xA5, 0xA6, 0xA6, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //1968 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x78, 0x78, 0x78, 0x87,
                                                       0x87, //1969 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x69, 0x78,
                                                       0x87, //1970 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x79, 0x69, 0x78,
                                                       0x77, //1971 
                                                       0x96, 0xA4, 0xA5, 0xA5, 0xA6, 0xA6, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //1972 
                                                       0xA5, 0xB5, 0x96, 0xA5, 0xA6, 0x96, 0x88, 0x78, 0x78, 0x78, 0x87,
                                                       0x87, //1973 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x69, 0x78,
                                                       0x87, //1974 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x97, 0x97, 0x78, 0x79, 0x78, 0x69, 0x78,
                                                       0x77, //1975 
                                                       0x96, 0xA4, 0xA5, 0xB5, 0xA6, 0xA6, 0x88, 0x89, 0x88, 0x78, 0x87,
                                                       0x87, //1976 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x88, 0x78, 0x78, 0x87,
                                                       0x87, //1977 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x79, 0x78,
                                                       0x87, //1978 
                                                       0x96, 0xB4, 0x96, 0xA6, 0x96, 0x97, 0x78, 0x79, 0x78, 0x69, 0x78,
                                                       0x77, //1979 
                                                       0x96, 0xA4, 0xA5, 0xB5, 0xA6, 0xA6, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //1980 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x78, 0x78, 0x77,
                                                       0x87, //1981 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x79, 0x77,
                                                       0x87, //1982 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x78, 0x79, 0x78, 0x69, 0x78,
                                                       0x77, //1983 
                                                       0x96, 0xB4, 0xA5, 0xB5, 0xA6, 0xA6, 0x87, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //1984 
                                                       0xA5, 0xB4, 0xA6, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x78, 0x78, 0x87,
                                                       0x87, //1985 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x79, 0x77,
                                                       0x87, //1986 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x79, 0x78, 0x69, 0x78,
                                                       0x87, //1987 
                                                       0x96, 0xB4, 0xA5, 0xB5, 0xA6, 0xA6, 0x87, 0x88, 0x88, 0x78, 0x87,
                                                       0x86, //1988 
                                                       0xA5, 0xB4, 0xA5, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //1989 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x78, 0x78, 0x79, 0x77,
                                                       0x87, //1990 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x86, 0x97, 0x88, 0x78, 0x78, 0x69, 0x78,
                                                       0x87, //1991 
                                                       0x96, 0xB4, 0xA5, 0xB5, 0xA6, 0xA6, 0x87, 0x88, 0x88, 0x78, 0x87,
                                                       0x86, //1992 
                                                       0xA5, 0xB3, 0xA5, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //1993 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x78, 0x78, 0x78, 0x87,
                                                       0x87, //1994 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x76, 0x78, 0x69, 0x78,
                                                       0x87, //1995 
                                                       0x96, 0xB4, 0xA5, 0xB5, 0xA6, 0xA6, 0x87, 0x88, 0x88, 0x78, 0x87,
                                                       0x86, //1996 
                                                       0xA5, 0xB3, 0xA5, 0xA5, 0xA6, 0xA6, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //1997 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x78, 0x78, 0x78, 0x87,
                                                       0x87, //1998 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x69, 0x78,
                                                       0x87, //1999 
                                                       0x96, 0xB4, 0xA5, 0xB5, 0xA6, 0xA6, 0x87, 0x88, 0x88, 0x78, 0x87,
                                                       0x86, //2000 
                                                       0xA5, 0xB3, 0xA5, 0xA5, 0xA6, 0xA6, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //2001 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x78, 0x78, 0x78, 0x87,
                                                       0x87, //2002 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x69, 0x78,
                                                       0x87, //2003 
                                                       0x96, 0xB4, 0xA5, 0xB5, 0xA6, 0xA6, 0x87, 0x88, 0x88, 0x78, 0x87,
                                                       0x86, //2004 
                                                       0xA5, 0xB3, 0xA5, 0xA5, 0xA6, 0xA6, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //2005 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x78, 0x78, 0x87,
                                                       0x87, //2006 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x69, 0x78,
                                                       0x87, //2007 
                                                       0x96, 0xB4, 0xA5, 0xB5, 0xA6, 0xA6, 0x87, 0x88, 0x87, 0x78, 0x87,
                                                       0x86, //2008 
                                                       0xA5, 0xB3, 0xA5, 0xB5, 0xA6, 0xA6, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //2009 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x78, 0x78, 0x87,
                                                       0x87, //2010 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x79, 0x78,
                                                       0x87, //2011 
                                                       0x96, 0xB4, 0xA5, 0xB5, 0xA5, 0xA6, 0x87, 0x88, 0x87, 0x78, 0x87,
                                                       0x86, //2012 
                                                       0xA5, 0xB3, 0xA5, 0xB5, 0xA6, 0xA6, 0x87, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //2013 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x78, 0x78, 0x87,
                                                       0x87, //2014 
                                                       0x95, 0xB4, 0x96, 0xA5, 0x96, 0x97, 0x88, 0x78, 0x78, 0x79, 0x77,
                                                       0x87, //2015 
                                                       0x95, 0xB4, 0xA5, 0xB4, 0xA5, 0xA6, 0x87, 0x88, 0x87, 0x78, 0x87,
                                                       0x86, //2016 
                                                       0xA5, 0xC3, 0xA5, 0xB5, 0xA6, 0xA6, 0x87, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //2017 
                                                       0xA5, 0xB4, 0xA6, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x78, 0x78, 0x87,
                                                       0x87, //2018 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x78, 0x78, 0x79, 0x77,
                                                       0x87, //2019 
                                                       0x95, 0xB4, 0xA5, 0xB4, 0xA5, 0xA6, 0x97, 0x87, 0x87, 0x78, 0x87,
                                                       0x86, //2020 
                                                       0xA5, 0xC3, 0xA5, 0xB5, 0xA6, 0xA6, 0x87, 0x88, 0x88, 0x78, 0x87,
                                                       0x86, //2021 
                                                       0xA5, 0xB4, 0xA5, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //2022 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x78, 0x78, 0x79, 0x77,
                                                       0x87, //2023 
                                                       0x95, 0xB4, 0xA5, 0xB4, 0xA5, 0xA6, 0x97, 0x87, 0x87, 0x78, 0x87,
                                                       0x96, //2024 
                                                       0xA5, 0xC3, 0xA5, 0xB5, 0xA6, 0xA6, 0x87, 0x88, 0x88, 0x78, 0x87,
                                                       0x86, //2025 
                                                       0xA5, 0xB3, 0xA5, 0xA5, 0xA6, 0xA6, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //2026 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x78, 0x78, 0x78, 0x87,
                                                       0x87, //2027 
                                                       0x95, 0xB4, 0xA5, 0xB4, 0xA5, 0xA6, 0x97, 0x87, 0x87, 0x78, 0x87,
                                                       0x96, //2028 
                                                       0xA5, 0xC3, 0xA5, 0xB5, 0xA6, 0xA6, 0x87, 0x88, 0x88, 0x78, 0x87,
                                                       0x86, //2029 
                                                       0xA5, 0xB3, 0xA5, 0xA5, 0xA6, 0xA6, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //2030 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0x96, 0x96, 0x88, 0x78, 0x78, 0x78, 0x87,
                                                       0x87, //2031 
                                                       0x95, 0xB4, 0xA5, 0xB4, 0xA5, 0xA6, 0x97, 0x87, 0x87, 0x78, 0x87,
                                                       0x96, //2032 
                                                       0xA5, 0xC3, 0xA5, 0xB5, 0xA6, 0xA6, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x86, //2033 
                                                       0xA5, 0xB3, 0xA5, 0xA5, 0xA6, 0xA6, 0x88, 0x78, 0x88, 0x78, 0x87,
                                                       0x87, //2034 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x78, 0x78, 0x87,
                                                       0x87, //2035 
                                                       0x95, 0xB4, 0xA5, 0xB4, 0xA5, 0xA6, 0x97, 0x87, 0x87, 0x78, 0x87,
                                                       0x96, //2036 
                                                       0xA5, 0xC3, 0xA5, 0xB5, 0xA6, 0xA6, 0x87, 0x88, 0x88, 0x78, 0x87,
                                                       0x86, //2037 
                                                       0xA5, 0xB3, 0xA5, 0xA5, 0xA6, 0xA6, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //2038 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x78, 0x78, 0x87,
                                                       0x87, //2039 
                                                       0x95, 0xB4, 0xA5, 0xB4, 0xA5, 0xA6, 0x97, 0x87, 0x87, 0x78, 0x87,
                                                       0x96, //2040 
                                                       0xA5, 0xC3, 0xA5, 0xB5, 0xA5, 0xA6, 0x87, 0x88, 0x87, 0x78, 0x87,
                                                       0x86, //2041 
                                                       0xA5, 0xB3, 0xA5, 0xB5, 0xA6, 0xA6, 0x88, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //2042 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x78, 0x78, 0x87,
                                                       0x87, //2043 
                                                       0x95, 0xB4, 0xA5, 0xB4, 0xA5, 0xA6, 0x97, 0x87, 0x87, 0x88, 0x87,
                                                       0x96, //2044 
                                                       0xA5, 0xC3, 0xA5, 0xB4, 0xA5, 0xA6, 0x87, 0x88, 0x87, 0x78, 0x87,
                                                       0x86, //2045 
                                                       0xA5, 0xB3, 0xA5, 0xB5, 0xA6, 0xA6, 0x87, 0x88, 0x88, 0x78, 0x87,
                                                       0x87, //2046 
                                                       0xA5, 0xB4, 0x96, 0xA5, 0xA6, 0x96, 0x88, 0x88, 0x78, 0x78, 0x87,
                                                       0x87, //2047 
                                                       0x95, 0xB4, 0xA5, 0xB4, 0xA5, 0xA5, 0x97, 0x87, 0x87, 0x88, 0x86,
                                                       0x96, //2048 
                                                       0xA4, 0xC3, 0xA5, 0xA5, 0xA5, 0xA6, 0x97, 0x87, 0x87, 0x78, 0x87,
                                                       0x86, //2049 
                                                       0xA5, 0xC3, 0xA5, 0xB5, 0xA6, 0xA6, 0x87, 0x88, 0x78, 0x78, 0x87,
                                                       0x87
                                                   }; //2050 
            #endregion


            private readonly byte[] gLunarMonth = {
                                                      0x00, 0x50, 0x04, 0x00, 0x20, //1910 
                                                      0x60, 0x05, 0x00, 0x20, 0x70, //1920 
                                                      0x05, 0x00, 0x40, 0x02, 0x06, //1930 
                                                      0x00, 0x50, 0x03, 0x07, 0x00, //1940 
                                                      0x60, 0x04, 0x00, 0x20, 0x70, //1950 
                                                      0x05, 0x00, 0x30, 0x80, 0x06, //1960 
                                                      0x00, 0x40, 0x03, 0x07, 0x00, //1970 
                                                      0x50, 0x04, 0x08, 0x00, 0x60, //1980 
                                                      0x04, 0x0a, 0x00, 0x60, 0x05, //1990 
                                                      0x00, 0x30, 0x80, 0x05, 0x00, //2000 
                                                      0x40, 0x02, 0x07, 0x00, 0x50, //2010 
                                                      0x04, 0x09, 0x00, 0x60, 0x04, //2020 
                                                      0x00, 0x20, 0x60, 0x05, 0x00, //2030 
                                                      0x30, 0xb0, 0x06, 0x00, 0x50, //2040 
                                                      0x02, 0x07, 0x00, 0x50, 0x03
                                                  }; //2050 
            #region 农历年份天数

            private readonly int[] gLunarMonthDay = {
                                                        //测试数据只有1901.1.1 --2050.12.31 
                                                        0x4ae0, 0xa570, 0x5268, 0xd260, 0xd950, 0x6aa8, 0x56a0, 0x9ad0,
                                                        0x4ae8, 0x4ae0, //1910 
                                                        0xa4d8, 0xa4d0, 0xd250, 0xd548, 0xb550, 0x56a0, 0x96d0, 0x95b0,
                                                        0x49b8, 0x49b0, //1920 
                                                        0xa4b0, 0xb258, 0x6a50, 0x6d40, 0xada8, 0x2b60, 0x9570, 0x4978,
                                                        0x4970, 0x64b0, //1930 
                                                        0xd4a0, 0xea50, 0x6d48, 0x5ad0, 0x2b60, 0x9370, 0x92e0, 0xc968,
                                                        0xc950, 0xd4a0, //1940 
                                                        0xda50, 0xb550, 0x56a0, 0xaad8, 0x25d0, 0x92d0, 0xc958, 0xa950,
                                                        0xb4a8, 0x6ca0, //1950 
                                                        0xb550, 0x55a8, 0x4da0, 0xa5b0, 0x52b8, 0x52b0, 0xa950, 0xe950,
                                                        0x6aa0, 0xad50, //1960 
                                                        0xab50, 0x4b60, 0xa570, 0xa570, 0x5260, 0xe930, 0xd950, 0x5aa8,
                                                        0x56a0, 0x96d0, //1970 
                                                        0x4ae8, 0x4ad0, 0xa4d0, 0xd268, 0xd250, 0xd528, 0xb540, 0xb6a0,
                                                        0x96d0, 0x95b0, //1980 
                                                        0x49b0, 0xa4b8, 0xa4b0, 0xb258, 0x6a50, 0x6d40, 0xada0, 0xab60,
                                                        0x9370, 0x4978, //1990 
                                                        0x4970, 0x64b0, 0x6a50, 0xea50, 0x6b28, 0x5ac0, 0xab60, 0x9368,
                                                        0x92e0, 0xc960, //2000 
                                                        0xd4a8, 0xd4a0, 0xda50, 0x5aa8, 0x56a0, 0xaad8, 0x25d0, 0x92d0,
                                                        0xc958, 0xa950, //2010 
                                                        0xb4a0, 0xb550, 0xb550, 0x55a8, 0x4ba0, 0xa5b0, 0x52b8, 0x52b0,
                                                        0xa930, 0x74a8, //2020 
                                                        0x6aa0, 0xad50, 0x4da8, 0x4b60, 0x9570, 0xa4e0, 0xd260, 0xe930,
                                                        0xd530, 0x5aa0, //2030 
                                                        0x6b50, 0x96d0, 0x4ae8, 0x4ad0, 0xa4d0, 0xd258, 0xd250, 0xd520,
                                                        0xdaa0, 0xb5a0, //2040 
                                                        0x56d0, 0x4ad8, 0x49b0, 0xa4b8, 0xa4b0, 0xaa50, 0xb528, 0x6d20,
                                                        0xada0, 0x55b0
                                                    }; //2050 

            #endregion

            #region 节气

            private readonly string[] LunarHolDayName =
                {
                    "小寒", "大寒", "立春", "雨水",
                    "惊蛰", "春分", "清明", "谷雨",
                    "立夏", "小满", "芒种", "夏至",
                    "小暑", "大暑", "立秋", "处暑",
                    "白露", "秋分", "寒露", "霜降",
                    "立冬", "小雪", "大雪", "冬至"
                };

            #endregion

            #endregion

            public ChineseDate(DateTime dt)
            {
                if (dt.Year > END_YEAR || dt.Year < START_YEAR)
                {
                    throw new ArgumentOutOfRangeException("dt", string.Format("只能介于 {0}-01-01 与 {1}-12-31 之间", START_YEAR, END_YEAR));
                }
                m_Date = dt.Date;
            }


            //计算指定日期的星座序号 
            public int GetConstellation()
            {
                int M = m_Date.Month;
                int D = m_Date.Day;
                int Y = M * 100 + D;
                if (((Y >= 321) && (Y <= 419)))
                {
                    return 0;
                }
                if ((Y >= 420) && (Y <= 520))
                {
                    return 1;
                }
                if ((Y >= 521) && (Y <= 620))
                {
                    return 2;
                }
                if ((Y >= 621) && (Y <= 722))
                {
                    return 3;
                }
                if ((Y >= 723) && (Y <= 822))
                {
                    return 4;
                }
                if ((Y >= 823) && (Y <= 922))
                {
                    return 5;
                }
                if ((Y >= 923) && (Y <= 1022))
                {
                    return 6;
                }
                if ((Y >= 1023) && (Y <= 1121))
                {
                    return 7;
                }
                if ((Y >= 1122) && (Y <= 1221))
                {
                    return 8;
                }
                if ((Y >= 1222) || (Y <= 119))
                {
                    return 9;
                }
                if ((Y >= 120) && (Y <= 218))
                {
                    return 10;
                }
                if ((Y >= 219) && (Y <= 320))
                {
                    return 11;
                }
                return -1;
            }

            //计算指定日期的星座名称 
            public string GetConstellationName()
            {
                int Constellation = GetConstellation();
                return (Constellation >= 0) && (Constellation <= 11) ? ConstellationName[Constellation] : "";
            }

            //计算公历当天对应的节气 0-23，-1表示不是节气 
            public int l_GetLunarHolDay()
            {
                int Day;
                int iYear = m_Date.Year;
                int iMonth = m_Date.Month;
                int iDay = m_Date.Day;
                byte Flag = gLunarHolDay[(iYear - START_YEAR) * 12 + iMonth - 1];
                if (iDay < 15)
                {
                    Day = 15 - ((Flag >> 4) & 0x0f);
                }
                else
                {
                    Day = (Flag & 0x0f) + 15;
                }
                if (iDay == Day)
                {
                    return iDay > 15 ? (iMonth - 1) * 2 + 1 : (iMonth - 1) * 2;
                }
                return -1;
            }

            public string FormatMonth()
            {
                const string szText = "正二三四五六七八九十冬腊";
                return szText[iMonth - 1] + "月";
            }

            public string FormatLunarDay()
            {
                const string szText1 = "初十廿三";
                const string szText2 = "一二三四五六七八九十";
                string strDay;
                if ((iDay != 20) && (iDay != 30))
                {
                    strDay = szText1.Substring((iDay - 1) / 10, 1);
                    strDay = strDay + szText2.Substring((iDay - 1) % 10, 1);
                }
                else
                {
                    strDay = szText1.Substring(iDay / 10, 1);
                    strDay = strDay + "十";
                }
                return strDay;
            }

            private ushort iYear;
            private ushort iMonth;
            private ushort iDay;

            public string GetLunarHolDayName()
            {
                int i = l_GetLunarHolDay();
                if ((i >= 0) && (i <= 23))
                {
                    return LunarHolDayName[i];
                }
                return null;
            }

            //返回阴历iLunarYear年的闰月月份，如没有返回0 1901年1月---2050年12月 
            public int GetLeapMonth(ushort iLunarYear)
            {
                if ((iLunarYear < START_YEAR) || (iLunarYear > END_YEAR))
                {
                    return 0;
                }
                byte Flag = gLunarMonth[(iLunarYear - START_YEAR) / 2];
                return (iLunarYear - START_YEAR) % 2 == 0 ? Flag >> 4 : Flag & 0x0F;
            }

            //返回阴历iLunarYer年阴历iLunarMonth月的天数，如果iLunarMonth为闰月， 
            //高字为第二个iLunarMonth月的天数，否则高字为0 1901年1月---2050年12月 
            public uint LunarMonthDays(ushort iLunarYear, ushort iLunarMonth)
            {
                if ((iLunarYear < START_YEAR) || (iLunarYear > END_YEAR))
                {
                    return 30;
                }
                int Height = 0;
                int Low = 29;
                int iBit = 16 - iLunarMonth;
                if ((iLunarMonth > GetLeapMonth(iLunarYear)) && (GetLeapMonth(iLunarYear) > 0))
                {
                    iBit--;
                }
                if ((gLunarMonthDay[iLunarYear - START_YEAR] & (1 << iBit)) > 0)
                {
                    Low++;
                }
                if (iLunarMonth == GetLeapMonth(iLunarYear))
                {
                    Height = (gLunarMonthDay[iLunarYear - START_YEAR] & (1 << (iBit - 1))) > 0 ? 30 : 29;
                }
                return ((uint)(Low) | (uint)(Height) << 16); //合成为uint 
            }

            //返回阴历iLunarYear年的总天数 1901年1月---2050年12月 
            public int LunarYearDays(ushort iLunarYear)
            {
                if ((iLunarYear < START_YEAR) || (iLunarYear > END_YEAR))
                {
                    return 0;
                }
                int Days = 0;
                for (ushort i = 1; i <= 12; i++)
                {
                    uint tmp = LunarMonthDays(iLunarYear, i);
                    Days = Days + ((ushort)(tmp >> 16) & 0xFFFF); //取高位 
                    Days = Days + (ushort)(tmp); //取低位 
                }
                return Days;
            }

            //计算从1901年1月1日过iSpanDays天后的阴历日期 
            public void l_CalcLunarDate()
            {
                uint span = (uint)(m_Date - (new DateTime(START_YEAR, 1, 1))).Days;
                //阳历1901年2月19日为阴历1901年正月初一 
                //阳历1901年1月1日到2月19日共有49天 
                if (span < 49)
                {
                    iYear = START_YEAR - 1;
                    if (span < 19)
                    {
                        iMonth = 11;
                        iDay = (ushort)(11 + span);
                    }
                    else
                    {
                        iMonth = 12;
                        iDay = (ushort)(span - 18);
                    }
                    return;
                }
                //下面从阴历1901年正月初一算起 
                span = span - 49;
                iYear = START_YEAR;
                iMonth = 1;
                iDay = 1;
                //计算年 
                uint tmp = (uint)LunarYearDays(iYear);
                while (span >= tmp)
                {
                    span = span - tmp;
                    iYear++;
                    tmp = (uint)LunarYearDays(iYear);
                }
                //计算月 
                tmp = LunarMonthDays(iYear, iMonth); //取低位 
                while (span >= tmp)
                {
                    span = span - tmp;
                    if (iMonth == GetLeapMonth(iYear))
                    {
                        tmp = (LunarMonthDays(iYear, iMonth) >> 16) & 0xFFFF; //取高位 
                        if (span < tmp)
                        {
                            break;
                        }
                        span = span - tmp;
                    }
                    iMonth++;
                    tmp = LunarMonthDays(iYear, iMonth); //取低位 
                }
                //计算日 
                iDay = (ushort)(iDay + span);
            }

            //把iYear年格式化成天干记年法表示的字符串 
            public string FormatLunarYear()
            {
                const string szText1 = "甲乙丙丁戊己庚辛壬癸";
                const string szText2 = "子丑寅卯辰巳午未申酉戌亥";
                //string szText3 = "鼠牛虎免龙蛇马羊猴鸡狗猪";
                ushort iYear = (ushort)(m_Date.Year);
                string strYear = szText1.Substring((iYear - 4) % 10, 1);
                strYear = strYear + szText2.Substring((iYear - 4) % 12, 1);
                strYear = strYear + "年";
                return strYear;
            }
        }

        #endregion
    }
}