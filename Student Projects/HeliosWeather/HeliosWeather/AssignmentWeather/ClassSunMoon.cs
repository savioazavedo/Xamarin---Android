using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace heliosweather
{
    [XmlRoot(ElementName = "features")]
    public class S_Features
    {
        [XmlElement(ElementName = "feature")]
        public string Feature { get; set; }
    }

    [XmlRoot(ElementName = "current_time")]
    public class S_Current_time
    {
        [XmlElement(ElementName = "hour")]
        public string Hour { get; set; }
        [XmlElement(ElementName = "minute")]
        public string Minute { get; set; }
    }

    [XmlRoot(ElementName = "sunset")]
    public class Sunset
    {
        [XmlElement(ElementName = "hour")]
        public string Hour { get; set; }
        [XmlElement(ElementName = "minute")]
        public string Minute { get; set; }
    }

    [XmlRoot(ElementName = "sunrise")]
    public class Sunrise
    {
        [XmlElement(ElementName = "hour")]
        public string Hour { get; set; }
        [XmlElement(ElementName = "minute")]
        public string Minute { get; set; }
    }

    [XmlRoot(ElementName = "moon_phase")]
    public class Moon_phase
    {
        [XmlElement(ElementName = "percentIlluminated")]
        public string PercentIlluminated { get; set; }
        [XmlElement(ElementName = "ageOfMoon")]
        public string AgeOfMoon { get; set; }
        [XmlElement(ElementName = "current_time")]
        public S_Current_time Current_time { get; set; }
        [XmlElement(ElementName = "sunset")]
        public Sunset Sunset { get; set; }
        [XmlElement(ElementName = "sunrise")]
        public Sunrise Sunrise { get; set; }
    }

    [XmlRoot(ElementName = "sun_phase")]
    public class Sun_phase
    {
        [XmlElement(ElementName = "sunset")]
        public Sunset Sunset { get; set; }
        [XmlElement(ElementName = "sunrise")]
        public Sunrise Sunrise { get; set; }
    }

    [XmlRoot(ElementName = "response")]
    public class S_Response
    {
        [XmlElement(ElementName = "version")]
        public string Version { get; set; }
        [XmlElement(ElementName = "termsofService")]
        public string TermsofService { get; set; }
        [XmlElement(ElementName = "features")]
        public S_Features Features { get; set; }
        [XmlElement(ElementName = "moon_phase")]
        public Moon_phase Moon_phase { get; set; }
        [XmlElement(ElementName = "sun_phase")]
        public Sun_phase Sun_phase { get; set; }
    }

}
