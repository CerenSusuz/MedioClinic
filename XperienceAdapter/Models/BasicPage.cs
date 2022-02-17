using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XperienceAdapter.Models
{
	/// </summary>
	public interface IService
	{
	}

	/// <summary>
	/// Provides data from a data source in the form of a DTO object.
	/// </summary>
	/// <typeparam name="TDto">Type of the DTO.</typeparam>
	public interface IRepository<TDto>
	{
		/// <summary>
		/// Gets all items from the source.
		/// </summary>
		/// <returns>All items.</returns>
		IEnumerable<TDto> GetAll();

		/// <summary>
		/// Gets all items from the source asynchronously.
		/// </summary>
		/// <returns>All items.</returns>
		Task<IEnumerable<TDto>> GetAllAsync(CancellationToken? cancellationToken = default);
	}
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

