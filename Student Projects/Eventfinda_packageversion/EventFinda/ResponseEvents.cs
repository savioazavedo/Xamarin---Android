using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace EventFinda
{

		[XmlRoot(ElementName="point")]
		public class Point {
			[XmlElement(ElementName="lat")]
			public string Lat { get; set; }
			[XmlElement(ElementName="lng")]
			public string Lng { get; set; }
		}

		[XmlRoot(ElementName="transform")]
		public class Transform {
			[XmlElement(ElementName="transformation_id")]
			public string Transformation_id { get; set; }
			[XmlElement(ElementName="url")]
			public string Url { get; set; }
			[XmlElement(ElementName="width")]
			public string Width { get; set; }
			[XmlElement(ElementName="height")]
			public string Height { get; set; }
		}

		[XmlRoot(ElementName="transforms")]
		public class Transforms {
			[XmlElement(ElementName="transform")]
			public List<Transform> Transform { get; set; }
			[XmlAttribute(AttributeName="count")]
			public string Count { get; set; }
		}

		[XmlRoot(ElementName="image")]
		public class Image {
			[XmlElement(ElementName="id")]
			public string Id { get; set; }
			[XmlElement(ElementName="is_primary")]
			public string Is_primary { get; set; }
			[XmlElement(ElementName="transforms")]
			public Transforms Transforms { get; set; }
		}

		[XmlRoot(ElementName="images")]
		public class Images {
			[XmlElement(ElementName="image")]
			public List<Image> Image { get; set; }
			[XmlAttribute(AttributeName="count")]
			public string Count { get; set; }
		}

		[XmlRoot(ElementName="location")]
		public class Location {
			[XmlElement(ElementName="id")]
			public string Id { get; set; }
			[XmlElement(ElementName="name")]
			public string Name { get; set; }
			[XmlElement(ElementName="summary")]
			public string Summary { get; set; }
			[XmlElement(ElementName="is_venue")]
			public string Is_venue { get; set; }
			[XmlElement(ElementName="count_current_events")]
			public string Count_current_events { get; set; }
			[XmlElement(ElementName="description")]
			public string Description { get; set; }
			[XmlElement(ElementName="images")]
			public Images Images { get; set; }
			[XmlElement(ElementName="point")]
			public Point Point { get; set; }
		}

		[XmlRoot(ElementName="category")]
		public class Category {
			[XmlElement(ElementName="id")]
			public string Id { get; set; }
			[XmlElement(ElementName="name")]
			public string Name { get; set; }
			[XmlElement(ElementName="url_slug")]
			public string Url_slug { get; set; }
			[XmlElement(ElementName="parent_id")]
			public string Parent_id { get; set; }
			[XmlElement(ElementName="count_current_events")]
			public string Count_current_events { get; set; }
		}

		[XmlRoot(ElementName="session_ticket")]
		public class Session_ticket {
			[XmlElement(ElementName="id")]
			public string Id { get; set; }
			[XmlElement(ElementName="name")]
			public string Name { get; set; }
			[XmlElement(ElementName="price")]
			public string Price { get; set; }
			[XmlElement(ElementName="is_ticketed")]
			public string Is_ticketed { get; set; }
			[XmlElement(ElementName="description")]
			public string Description { get; set; }
			[XmlElement(ElementName="minimum_purchase_quantity")]
			public string Minimum_purchase_quantity { get; set; }
			[XmlElement(ElementName="is_addon")]
			public string Is_addon { get; set; }
			[XmlElement(ElementName="onsale_at")]
			public string Onsale_at { get; set; }
			[XmlElement(ElementName="offsale_at")]
			public string Offsale_at { get; set; }
			[XmlElement(ElementName="is_sold_out")]
			public string Is_sold_out { get; set; }
			[XmlElement(ElementName="is_limited_remaining")]
			public string Is_limited_remaining { get; set; }
		}

		[XmlRoot(ElementName="session_tickets")]
		public class Session_tickets {
			[XmlElement(ElementName="session_ticket")]
			public List<Session_ticket> Session_ticket { get; set; }
			[XmlAttribute(AttributeName="count")]
			public string Count { get; set; }
		}

		[XmlRoot(ElementName="session")]
		public class Session {
			[XmlElement(ElementName="id")]
			public string Id { get; set; }
			[XmlElement(ElementName="timezone")]
			public string Timezone { get; set; }
			[XmlElement(ElementName="datetime_start")]
			public string Datetime_start { get; set; }
			[XmlElement(ElementName="datetime_end")]
			public string Datetime_end { get; set; }
			[XmlElement(ElementName="is_cancelled")]
			public string Is_cancelled { get; set; }
			[XmlElement(ElementName="is_ticketed")]
			public string Is_ticketed { get; set; }
			[XmlElement(ElementName="is_currently_onsale")]
			public string Is_currently_onsale { get; set; }
			[XmlElement(ElementName="is_for_tickets_only")]
			public string Is_for_tickets_only { get; set; }
			[XmlElement(ElementName="ticketing_label")]
			public string Ticketing_label { get; set; }
			[XmlElement(ElementName="offsale_at")]
			public string Offsale_at { get; set; }
			[XmlElement(ElementName="session_tickets")]
			public Session_tickets Session_tickets { get; set; }
			[XmlElement(ElementName="datetime_summary")]
			public string Datetime_summary { get; set; }
		}

		[XmlRoot(ElementName="sessions")]
		public class Sessions {
			[XmlElement(ElementName="session")]
			public List<Session> Session { get; set; }
			[XmlAttribute(AttributeName="count")]
			public string Count { get; set; }
		}

		[XmlRoot(ElementName="ticket_type")]
		public class Ticket_type {
			[XmlElement(ElementName="id")]
			public string Id { get; set; }
			[XmlElement(ElementName="name")]
			public string Name { get; set; }
			[XmlElement(ElementName="price")]
			public string Price { get; set; }
			[XmlElement(ElementName="is_ticketed")]
			public string Is_ticketed { get; set; }
			[XmlElement(ElementName="description")]
			public string Description { get; set; }
			[XmlElement(ElementName="minimum_purchase_quantity")]
			public string Minimum_purchase_quantity { get; set; }
			[XmlElement(ElementName="is_addon")]
			public string Is_addon { get; set; }
			[XmlElement(ElementName="onsale_at")]
			public string Onsale_at { get; set; }
			[XmlElement(ElementName="offsale_at")]
			public string Offsale_at { get; set; }
		}

		[XmlRoot(ElementName="ticket_types")]
		public class Ticket_types {
			[XmlElement(ElementName="ticket_type")]
			public List<Ticket_type> Ticket_type { get; set; }
			[XmlAttribute(AttributeName="count")]
			public string Count { get; set; }
		}

		[XmlRoot(ElementName="artists")]
		public class Artists {
			[XmlAttribute(AttributeName="count")]
			public string Count { get; set; }
			[XmlElement(ElementName="artist")]
			public Artist Artist { get; set; }
		}

		[XmlRoot(ElementName="web_site")]
		public class Web_site {
			[XmlElement(ElementName="url")]
			public string Url { get; set; }
			[XmlElement(ElementName="name")]
			public string Name { get; set; }
		}

		[XmlRoot(ElementName="web_sites")]
		public class Web_sites {
			[XmlElement(ElementName="web_site")]
			public List<Web_site> Web_site { get; set; }
			[XmlAttribute(AttributeName="count")]
			public string Count { get; set; }
		}

		[XmlRoot(ElementName="event")]
		public class Event {
			[XmlElement(ElementName="id")]
			public string Id { get; set; }
			[XmlElement(ElementName="url")]
			public string Url { get; set; }
			[XmlElement(ElementName="url_slug")]
			public string Url_slug { get; set; }
			[XmlElement(ElementName="name")]
			public string Name { get; set; }
			[XmlElement(ElementName="presented_by")]
			public string Presented_by { get; set; }
			[XmlElement(ElementName="description")]
			public string Description { get; set; }
			[XmlElement(ElementName="datetime_start")]
			public string Datetime_start { get; set; }
			[XmlElement(ElementName="datetime_end")]
			public string Datetime_end { get; set; }
			[XmlElement(ElementName="location_summary")]
			public string Location_summary { get; set; }
			[XmlElement(ElementName="address")]
			public string Address { get; set; }
			[XmlElement(ElementName="is_free")]
			public string Is_free { get; set; }
			[XmlElement(ElementName="is_featured")]
			public string Is_featured { get; set; }
			[XmlElement(ElementName="is_cancelled")]
			public string Is_cancelled { get; set; }
			[XmlElement(ElementName="restrictions")]
			public string Restrictions { get; set; }
			[XmlElement(ElementName="point")]
			public Point Point { get; set; }
			[XmlElement(ElementName="username")]
			public string Username { get; set; }
			[XmlElement(ElementName="timezone")]
			public string Timezone { get; set; }
			[XmlElement(ElementName="location")]
			public Location Location { get; set; }
			[XmlElement(ElementName="category")]
			public Category Category { get; set; }
			[XmlElement(ElementName="datetime_summary")]
			public string Datetime_summary { get; set; }
			[XmlElement(ElementName="sessions")]
			public Sessions Sessions { get; set; }
			[XmlElement(ElementName="ticket_types")]
			public Ticket_types Ticket_types { get; set; }
			[XmlElement(ElementName="artists")]
			public Artists Artists { get; set; }
			[XmlElement(ElementName="web_sites")]
			public Web_sites Web_sites { get; set; }
			[XmlElement(ElementName="images")]
			public Images Images { get; set; }
		}

		[XmlRoot(ElementName="artist")]
		public class Artist {
			[XmlElement(ElementName="id")]
			public string Id { get; set; }
			[XmlElement(ElementName="name")]
			public string Name { get; set; }
			[XmlElement(ElementName="url_slug")]
			public string Url_slug { get; set; }
			[XmlElement(ElementName="description")]
			public string Description { get; set; }
			[XmlElement(ElementName="url")]
			public string Url { get; set; }
			[XmlElement(ElementName="web_sites")]
			public Web_sites Web_sites { get; set; }
			[XmlElement(ElementName="images")]
			public Images Images { get; set; }
			[XmlElement(ElementName="location")]
			public Location Location { get; set; }
			[XmlElement(ElementName="category")]
			public Category Category { get; set; }
		}

		[XmlRoot(ElementName="events")]
		public class Events {
			[XmlElement(ElementName="event")]
			public List<Event> Event { get; set; }
			[XmlAttribute(AttributeName="count")]
			public string Count { get; set; }
		}

	}




