using Kentico.Content.Web.Mvc;

namespace MedioClinic.Models
{
    public class PageViewModel
    {
        public IPageMetadata Metadata { get; set; } = new PageMetadata();

        public UserMessage UserMessage { get; set; } = new UserMessage();

		/// <summary>
		/// method that returns a PageViewModel object based on the necessary values.
		/// </summary>
		/// <param name="pageMetadata"></param>
		/// <param name="message"></param>
		/// <param name="displayMessage"></param>
		/// <param name="displayAsRaw"></param>
		/// <param name="messageType"></param>
		/// <returns></returns>
		public static PageViewModel GetPageViewModel(
		IPageMetadata pageMetadata,
		string? message = default,
		bool displayMessage = true,
		bool displayAsRaw = default,
		MessageType messageType = MessageType.Info) =>
		new PageViewModel()
		{
			Metadata = pageMetadata,
			UserMessage = new UserMessage
			{
				Message = message,
				MessageType = messageType,
				DisplayAsRaw = displayAsRaw,
				Display = displayMessage
			}
		};

	}
}
