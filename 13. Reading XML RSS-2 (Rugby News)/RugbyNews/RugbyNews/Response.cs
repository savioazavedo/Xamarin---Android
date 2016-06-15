using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace RugbyNews
{
	[XmlRoot(ElementName="image")]
	public class Image {
		[XmlElement(ElementName="title")]
		public string Title { get; set; }
		[XmlElement(ElementName="url")]
		public string Url { get; set; }
		[XmlElement(ElementName="link")]
		public string Link { get; set; }
	}

	[XmlRoot(ElementName="item")]
	public class Item {
		[XmlElement(ElementName="title")]
		public string Title { get; set; }
		[XmlElement(ElementName="link")]
		public string Link { get; set; }
		[XmlElement(ElementName="description")]
		public string Description { get; set; }
		[XmlElement(ElementName="author")]
		public string Author { get; set; }
		[XmlElement(ElementName="pubDate")]
		public string PubDate { get; set; }
		[XmlElement(ElementName="enclosure")]
		public Enclosure Enclosure { get; set; }
	}

	[XmlRoot(ElementName="enclosure")]
	public class Enclosure {
		[XmlAttribute(AttributeName="url")]
		public string Url { get; set; }
		[XmlAttribute(AttributeName="length")]
		public string Length { get; set; }
		[XmlAttribute(AttributeName="type")]
		public string Type { get; set; }
	}

	[XmlRoot(ElementName="channel")]
	public class Channel {
		[XmlElement(ElementName="title")]
		public string Title { get; set; }
		[XmlElement(ElementName="link")]
		public string Link { get; set; }
		[XmlElement(ElementName="description")]
		public string Description { get; set; }
		[XmlElement(ElementName="language")]
		public string Language { get; set; }
		[XmlElement(ElementName="copyright")]
		public string Copyright { get; set; }
		[XmlElement(ElementName="lastBuildDate")]
		public string LastBuildDate { get; set; }
		[XmlElement(ElementName="docs")]
		public string Docs { get; set; }
		[XmlElement(ElementName="generator")]
		public string Generator { get; set; }
		[XmlElement(ElementName="managingEditor")]
		public string ManagingEditor { get; set; }
		[XmlElement(ElementName="webMaster")]
		public string WebMaster { get; set; }
		[XmlElement(ElementName="image")]
		public Image Image { get; set; }
		[XmlElement(ElementName="item")]
		public List<Item> Item { get; set; }
	}

	[XmlRoot(ElementName="rss")]
	public class Rss {
		[XmlElement(ElementName="channel")]
		public Channel Channel { get; set; }
		[XmlAttribute(AttributeName="version")]
		public string Version { get; set; }
	}

}
