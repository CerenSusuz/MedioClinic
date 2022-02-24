using Kentico.Content.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace XperienceAdapter.Models
{
    /// <summary>
    /// The class will be used to display pictures of various medical services in the Contact us page.
    /// </summary>
    public class MediaLibraryFile
    {
		public Guid Guid { get; set; }

		public string? Name { get; set; }

		public IMediaFileUrl? MediaFileUrl { get; set; }

		public string? Extension { get; set; }

		public bool IsImage { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }
	}
}
