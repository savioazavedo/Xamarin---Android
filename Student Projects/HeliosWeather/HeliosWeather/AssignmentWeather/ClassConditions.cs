
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace heliosweather
{
    [XmlRoot(ElementName = "features")]
    public class C_Features
    {
        [XmlElement(ElementName = "feature")]
        public string Feature { get; set; }
    }

    [XmlRoot(ElementName = "image")]
    public class C_Image
    {
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "link")]
        public string Link { get; set; }
    }

    [XmlRoot(ElementName = "display_location")]
    public class C_Display_location
    {
        [XmlElement(ElementName = "full")]
        public string Full { get; set; }
        [XmlElement(ElementName = "city")]
        public string City { get; set; }
        [XmlElement(ElementName = "state")]
        public string State { get; set; }
        [XmlElement(ElementName = "state_name")]
        public string State_name { get; set; }
        [XmlElement(ElementName = "country")]
        public string Country { get; set; }
        [XmlElement(ElementName = "country_iso3166")]
        public string Country_iso3166 { get; set; }
        [XmlElement(ElementName = "zip")]
        public string Zip { get; set; }
        [XmlElement(ElementName = "magic")]
        public string Magic { get; set; }
        [XmlElement(ElementName = "wmo")]
        public string Wmo { get; set; }
        [XmlElement(ElementName = "latitude")]
        public string Latitude { get; set; }
        [XmlElement(ElementName = "longitude")]
        public string Longitude { get; set; }
        [XmlElement(ElementName = "elevation")]
        public string Elevation { get; set; }
    }

    [XmlRoot(ElementName = "observation_location")]
    public class C_Observation_location
    {
        [XmlElement(ElementName = "full")]
        public string Full { get; set; }
        [XmlElement(ElementName = "city")]
        public string City { get; set; }
        [XmlElement(ElementName = "state")]
        public string State { get; set; }
        [XmlElement(ElementName = "country")]
        public string Country { get; set; }
        [XmlElement(ElementName = "country_iso3166")]
        public string Country_iso3166 { get; set; }
        [XmlElement(ElementName = "latitude")]
        public string Latitude { get; set; }
        [XmlElement(ElementName = "longitude")]
        public string Longitude { get; set; }
        [XmlElement(ElementName = "elevation")]
        public string Elevation { get; set; }
    }

    [XmlRoot(ElementName = "current_observation")]
    public class C_Current_observation
    {
        [XmlElement(ElementName = "image")]
        public C_Image Image { get; set; }
        [XmlElement(ElementName = "display_location")]
        public C_Display_location Display_location { get; set; }
        [XmlElement(ElementName = "observation_location")]
        public C_Observation_location Observation_location { get; set; }
        [XmlElement(ElementName = "estimated")]
        public string Estimated { get; set; }
        [XmlElement(ElementName = "station_id")]
        public string Station_id { get; set; }
        [XmlElement(ElementName = "observation_time")]
        public string Observation_time { get; set; }
        [XmlElement(ElementName = "observation_time_rfc822")]
        public string Observation_time_rfc822 { get; set; }
        [XmlElement(ElementName = "observation_epoch")]
        public string Observation_epoch { get; set; }
        [XmlElement(ElementName = "local_time_rfc822")]
        public string Local_time_rfc822 { get; set; }
        [XmlElement(ElementName = "local_epoch")]
        public string Local_epoch { get; set; }
        [XmlElement(ElementName = "local_tz_short")]
        public string Local_tz_short { get; set; }
        [XmlElement(ElementName = "local_tz_long")]
        public string Local_tz_long { get; set; }
        [XmlElement(ElementName = "local_tz_offset")]
        public string Local_tz_offset { get; set; }
        [XmlElement(ElementName = "weather")]
        public string Weather { get; set; }
        [XmlElement(ElementName = "temperature_string")]
        public string Temperature_string { get; set; }
        [XmlElement(ElementName = "temp_f")]
        public string Temp_f { get; set; }
        [XmlElement(ElementName = "temp_c")]
        public string Temp_c { get; set; }
        [XmlElement(ElementName = "relative_humidity")]
        public string Relative_humidity { get; set; }
        [XmlElement(ElementName = "wind_string")]
        public string Wind_string { get; set; }
        [XmlElement(ElementName = "wind_dir")]
        public string Wind_dir { get; set; }
        [XmlElement(ElementName = "wind_degrees")]
        public string Wind_degrees { get; set; }
        [XmlElement(ElementName = "wind_mph")]
        public string Wind_mph { get; set; }
        [XmlElement(ElementName = "wind_gust_mph")]
        public string Wind_gust_mph { get; set; }
        [XmlElement(ElementName = "wind_kph")]
        public string Wind_kph { get; set; }
        [XmlElement(ElementName = "wind_gust_kph")]
        public string Wind_gust_kph { get; set; }
        [XmlElement(ElementName = "pressure_mb")]
        public string Pressure_mb { get; set; }
        [XmlElement(ElementName = "pressure_in")]
        public string Pressure_in { get; set; }
        [XmlElement(ElementName = "pressure_trend")]
        public string Pressure_trend { get; set; }
        [XmlElement(ElementName = "dewpoint_string")]
        public string Dewpoint_string { get; set; }
        [XmlElement(ElementName = "dewpoint_f")]
        public string Dewpoint_f { get; set; }
        [XmlElement(ElementName = "dewpoint_c")]
        public string Dewpoint_c { get; set; }
        [XmlElement(ElementName = "heat_index_string")]
        public string Heat_index_string { get; set; }
        [XmlElement(ElementName = "heat_index_f")]
        public string Heat_index_f { get; set; }
        [XmlElement(ElementName = "heat_index_c")]
        public string Heat_index_c { get; set; }
        [XmlElement(ElementName = "windchill_string")]
        public string Windchill_string { get; set; }
        [XmlElement(ElementName = "windchill_f")]
        public string Windchill_f { get; set; }
        [XmlElement(ElementName = "windchill_c")]
        public string Windchill_c { get; set; }
        [XmlElement(ElementName = "feelslike_string")]
        public string Feelslike_string { get; set; }
        [XmlElement(ElementName = "feelslike_f")]
        public string Feelslike_f { get; set; }
        [XmlElement(ElementName = "feelslike_c")]
        public string Feelslike_c { get; set; }
        [XmlElement(ElementName = "visibility_mi")]
        public string Visibility_mi { get; set; }
        [XmlElement(ElementName = "visibility_km")]
        public string Visibility_km { get; set; }
        [XmlElement(ElementName = "solarradiation")]
        public string Solarradiation { get; set; }
        [XmlElement(ElementName = "UV")]
        public string UV { get; set; }
        [XmlElement(ElementName = "precip_1hr_string")]
        public string Precip_1hr_string { get; set; }
        [XmlElement(ElementName = "precip_1hr_in")]
        public string Precip_1hr_in { get; set; }
        [XmlElement(ElementName = "precip_1hr_metric")]
        public string Precip_1hr_metric { get; set; }
        [XmlElement(ElementName = "precip_today_string")]
        public string Precip_today_string { get; set; }
        [XmlElement(ElementName = "precip_today_in")]
        public string Precip_today_in { get; set; }
        [XmlElement(ElementName = "precip_today_metric")]
        public string Precip_today_metric { get; set; }
        [XmlElement(ElementName = "icon")]
        public string Icon { get; set; }
        [XmlElement(ElementName = "icon_url")]
        public string Icon_url { get; set; }
        [XmlElement(ElementName = "forecast_url")]
        public string Forecast_url { get; set; }
        [XmlElement(ElementName = "history_url")]
        public string History_url { get; set; }
        [XmlElement(ElementName = "ob_url")]
        public string Ob_url { get; set; }
    }

    [XmlRoot(ElementName = "response")]
    public class C_Response
    {
        [XmlElement(ElementName = "version")]
        public string Version { get; set; }
        [XmlElement(ElementName = "termsofService")]
        public string TermsofService { get; set; }
        [XmlElement(ElementName = "features")]
        public C_Features Features { get; set; }
        [XmlElement(ElementName = "current_observation")]
        public C_Current_observation Current_observation { get; set; }
    }

}
