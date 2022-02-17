using Kentico.Content.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace XperienceAdapter.Models
{
    public class PageAttachment
    {
		public int Id { get; set; }
		public Guid Guid { get; set; }
		public string? Title { get; set; }
		public string? FileName { get; set; }
		public string? Extension { get; set; }
		public string? MimeType { get; set; }
		public IPageAttachmentUrl? AttachmentUrl { get; set; }
	}
}
