using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Configuration
{
	/// <summary>
	/// Xperience-related configuration options.
	/// </summary>
	public class XperienceOptions
	{
		/// <summary>
		/// Friendly name of the company.
		/// </summary>
		public string? CompanyName { get; set; }

		/// <summary>
		/// Site code name.
		/// </summary>
		public string? SiteCodeName { get; set; }

		public MediaLibraryOptions? MediaLibraryOptions { get; set; }
	}
}
