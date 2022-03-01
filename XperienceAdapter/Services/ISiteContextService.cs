using Core;
using System;
using System.Collections.Generic;
using System.Text;
using XperienceAdapter.Models;

namespace XperienceAdapter.Services
{
	/// <summary>
	/// Captures information related the current site.
	/// </summary>
	public interface ISiteContextService : IService
	{
		/// <summary>
		/// Indicates if preview mode is enabled.
		/// </summary>
		bool IsPreviewEnabled { get; }
	}
}
