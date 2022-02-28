namespace MedioClinic.Models
{
	/// <summary>
	/// This class captures toast notifications to the site visitors.
	/// </summary>
	public class UserMessage
	{
		public MessageType MessageType { get; set; }

		public string? Message { get; set; }

		public bool DisplayAsRaw { get; set; }

		public bool Display { get; set; }
	}

	public enum MessageType
	{
		Info,
		Warning,
		Error
	}
}
