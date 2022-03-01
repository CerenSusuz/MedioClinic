using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XperienceAdapter.Models
{
	/// <summary>
	/// Basic page model.
	/// </summary>
	public class BasicPage
	{
		public virtual IEnumerable<string> SourceColumns => new List<string>
	{
		"DocumentID",
		"DocumentGUID",
		"DocumentName",
		"DocumentCulture",
		"NodeID",
		"NodeGUID",
		"NodeAliasPath",
		"NodeParentID",
		"NodeSiteID",
		"NodeLevel",
		"NodeOrder"
	};

		public int NodeId { get; set; }
		public Guid Guid { get; set; }
		public string? Name { get; set; }
		public string? NodeAliasPath { get; set; }
		public int? ParentId { get; set; }
		/// <summary>
		/// In the form of RFC 5646 (e.g. "en-US").
		/// </summary>
		public SiteCulture? Culture { get; set; }
		public IList<PageAttachment> Attachments { get; } = new List<PageAttachment>();
	}
}

