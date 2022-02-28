﻿using Kentico.Content.Web.Mvc;

namespace MedioClinic.Models
{
    /// <summary>
    /// support for page metadata.
    /// </summary>
    public class PageMetadata : IPageMetadata
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Keywords { get; set; }
    }
}
