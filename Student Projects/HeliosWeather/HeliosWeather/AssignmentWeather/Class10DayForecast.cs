using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace heliosweather
{
    [XmlRoot(ElementName = "features")]
    public class Features
    {
        [XmlElement(ElementName = "feature")]
        public string Feature { get; set; }
    }

    [XmlRoot(ElementName = "forecastday")]
    public class Forecastday
    {
        [XmlElement(ElementName = "period")]
        public string Period { get; set; }
        [XmlElement(ElementName = "icon")]
        public string Icon { get; set; }
        [XmlElement(ElementName = "icon_url")]
        public string Icon_url { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "fcttext")]
        public string Fcttext { get; set; }
        [XmlElement(ElementName = "fcttext_metric")]
        public string Fcttext_metric { get; set; }
        [XmlElement(ElementName = "pop")]
        public string Pop { get; set; }
        [XmlElement(ElementName = "date")]
        public Date Date { get; set; }
        [XmlElement(ElementName = "high")]
        public High High { get; set; }
        [XmlElement(ElementName = "low")]
        public Low Low { get; set; }
        [XmlElement(ElementName = "conditions")]
        public string Conditions { get; set; }
        [XmlElement(ElementName = "skyicon")]
        public string Skyicon { get; set; }
        [XmlElement(ElementName = "qpf_allday")]
        public Qpf_allday Qpf_allday { get; set; }
        [XmlElement(ElementName = "qpf_day")]
        public Qpf_day Qpf_day { get; set; }
        [XmlElement(ElementName = "qpf_night")]
        public Qpf_night Qpf_night { get; set; }
        [XmlElement(ElementName = "snow_allday")]
        public Snow_allday Snow_allday { get; set; }
        [XmlElement(ElementName = "snow_day")]
        public Snow_day Snow_day { get; set; }
        [XmlElement(ElementName = "snow_night")]
        public Snow_night Snow_night { get; set; }
        [XmlElement(ElementName = "maxwind")]
        public Maxwind Maxwind { get; set; }
        [XmlElement(ElementName = "avewind")]
        public Avewind Avewind { get; set; }
        [XmlElement(ElementName = "avehumidity")]
        public string Avehumidity { get; set; }
        [XmlElement(ElementName = "maxhumidity")]
        public string Maxhumidity { get; set; }
        [XmlElement(ElementName = "minhumidity")]
        public string Minhumidity { get; set; }
    }

    [XmlRoot(ElementName = "forecastdays")]
    public class Forecastdays
    {
        [XmlElement(ElementName = "forecastday")]
        public List<Forecastday> Forecastday { get; set; }
    }

    [XmlRoot(ElementName = "txt_forecast")]
    public class Txt_forecast
    {
        [XmlElement(ElementName = "date")]
        public string Date { get; set; }
        [XmlElement(ElementName = "forecastdays")]
        public Forecastdays Forecastdays { get; set; }
    }

    [XmlRoot(ElementName = "date")]
    public class Date
    {
        [XmlElement(ElementName = "epoch")]
        public string Epoch { get; set; }
        [XmlElement(ElementName = "pretty_short")]
        public string Pretty_short { get; set; }
        [XmlElement(ElementName = "pretty")]
        public string Pretty { get; set; }
        [XmlElement(ElementName = "day")]
        public string Day { get; set; }
        [XmlElement(ElementName = "month")]
        public string Month { get; set; }
        [XmlElement(ElementName = "year")]
        public string Year { get; set; }
        [XmlElement(ElementName = "yday")]
        public string Yday { get; set; }
        [XmlElement(ElementName = "hour")]
        public string Hour { get; set; }
        [XmlElement(ElementName = "min")]
        public string Min { get; set; }
        [XmlElement(ElementName = "sec")]
        public string Sec { get; set; }
        [XmlElement(ElementName = "isdst")]
        public string Isdst { get; set; }
        [XmlElement(ElementName = "monthname")]
        public string Monthname { get; set; }
        [XmlElement(ElementName = "monthname_short")]
        public string Monthname_short { get; set; }
        [XmlElement(ElementName = "weekday_short")]
        public string Weekday_short { get; set; }
        [XmlElement(ElementName = "weekday")]
        public string Weekday { get; set; }
        [XmlElement(ElementName = "ampm")]
        public string Ampm { get; set; }
        [XmlElement(ElementName = "tz_short")]
        public string Tz_short { get; set; }
        [XmlElement(ElementName = "tz_long")]
        public string Tz_long { get; set; }
    }

    [XmlRoot(ElementName = "high")]
    public class High
    {
        [XmlElement(ElementName = "fahrenheit")]
        public string Fahrenheit { get; set; }
        [XmlElement(ElementName = "celsius")]
        public string Celsius { get; set; }
    }

    [XmlRoot(ElementName = "low")]
    public class Low
    {
        [XmlElement(ElementName = "fahrenheit")]
        public string Fahrenheit { get; set; }
        [XmlElement(ElementName = "celsius")]
        public string Celsius { get; set; }
    }

    [XmlRoot(ElementName = "qpf_allday")]
    public class Qpf_allday
    {
        [XmlElement(ElementName = "in")]
        public string In { get; set; }
        [XmlElement(ElementName = "mm")]
        public string Mm { get; set; }
    }

    [XmlRoot(ElementName = "qpf_day")]
    public class Qpf_day
    {
        [XmlElement(ElementName = "in")]
        public string In { get; set; }
        [XmlElement(ElementName = "mm")]
        public string Mm { get; set; }
    }

    [XmlRoot(ElementName = "qpf_night")]
    public class Qpf_night
    {
        [XmlElement(ElementName = "in")]
        public string In { get; set; }
        [XmlElement(ElementName = "mm")]
        public string Mm { get; set; }
    }

    [XmlRoot(ElementName = "snow_allday")]
    public class Snow_allday
    {
        [XmlElement(ElementName = "in")]
        public string In { get; set; }
        [XmlElement(ElementName = "cm")]
        public string Cm { get; set; }
    }

    [XmlRoot(ElementName = "snow_day")]
    public class Snow_day
    {
        [XmlElement(ElementName = "in")]
        public string In { get; set; }
        [XmlElement(ElementName = "cm")]
        public string Cm { get; set; }
    }

    [XmlRoot(ElementName = "snow_night")]
    public class Snow_night
    {
        [XmlElement(ElementName = "in")]
        public string In { get; set; }
        [XmlElement(ElementName = "cm")]
        public string Cm { get; set; }
    }

    [XmlRoot(ElementName = "maxwind")]
    public class Maxwind
    {
        [XmlElement(ElementName = "mph")]
        public string Mph { get; set; }
        [XmlElement(ElementName = "kph")]
        public string Kph { get; set; }
        [XmlElement(ElementName = "dir")]
        public string Dir { get; set; }
        [XmlElement(ElementName = "degrees")]
        public string Degrees { get; set; }
    }

    [XmlRoot(ElementName = "avewind")]
    public class Avewind
    {
        [XmlElement(ElementName = "mph")]
        public string Mph { get; set; }
        [XmlElement(ElementName = "kph")]
        public string Kph { get; set; }
        [XmlElement(ElementName = "dir")]
        public string Dir { get; set; }
        [XmlElement(ElementName = "degrees")]
        public string Degrees { get; set; }
    }

    [XmlRoot(ElementName = "simpleforecast")]
    public class Simpleforecast
    {
        [XmlElement(ElementName = "forecastdays")]
        public Forecastdays Forecastdays { get; set; }
    }

    [XmlRoot(ElementName = "forecast")]
    public class Forecast
    {
        [XmlElement(ElementName = "txt_forecast")]
        public Txt_forecast Txt_forecast { get; set; }
        [XmlElement(ElementName = "simpleforecast")]
        public Simpleforecast Simpleforecast { get; set; }
    }

    [XmlRoot(ElementName = "response")]
    public class Response
    {
        [XmlElement(ElementName = "version")]
        public string Version { get; set; }
        [XmlElement(ElementName = "termsofService")]
        public string TermsofService { get; set; }
        [XmlElement(ElementName = "features")]
        public Features Features { get; set; }
        [XmlElement(ElementName = "forecast")]
        public Forecast Forecast { get; set; }
    }

}

