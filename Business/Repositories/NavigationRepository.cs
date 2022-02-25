using Business.Models;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.SiteProvider;
using CMS.Helpers.Caching;
using Kentico.Content.Web.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XperienceAdapter.Models;
using XperienceAdapter.Repositories;

namespace Business.Repositories
{
   public class NavigationRepository : INavigationRepository
{
	private const string RootPath = "/";

	private static readonly string[] NodeOrdering = new string[] { "NodeLevel", "NodeOrder" };

	private readonly IMemoryCache _memoryCache;

	private readonly ICacheDependencyAdapter _cacheDependencyAdapter;

	private readonly IPageUrlRetriever _pageUrlRetriever;

	private readonly IPageRepository<BasicPage, TreeNode> _basePageRepository;

	private readonly ISiteCultureRepository _cultureRepository;

	public IEnumerable<string> NavigationEnabledPageTypes => DataClassInfoProvider
		.GetClasses()
		.Where(classInfo => classInfo.ClassIsNavigationItem)
		.Select(classInfo => classInfo.ClassName);

	public IEnumerable<string> NavigationEnabledTypeDependencies => NavigationEnabledPageTypes
		.Select(pageType => $"nodes|{SiteContext.CurrentSiteName}|{pageType}|all");

	private NavigationItem RootDto => _basePageRepository.GetPagesInCurrentCulture(query =>
		query
			.Path(RootPath, PathTypeEnum.Single)
			.TopN(1),
		buildCacheAction: cache => cache
			.Key($"{nameof(NavigationRepository)}|{nameof(RootDto)}"))
			.Select(basePageDto => new NavigationItem
			{
				NodeId = basePageDto.NodeId,
				Name = basePageDto.Name
			})
			.FirstOrDefault();

	public NavigationRepository(
		IMemoryCache memoryCache,
		ICacheDependencyAdapter cacheDependencyAdapter,
		IPageUrlRetriever pageUrlRetriever,
		IPageRepository<BasicPage, TreeNode> basePageRepository,
		ISiteCultureRepository siteCultureRepository)
	{
		_memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
		_cacheDependencyAdapter = cacheDependencyAdapter ?? throw new ArgumentNullException(nameof(cacheDependencyAdapter));
		_pageUrlRetriever = pageUrlRetriever ?? throw new ArgumentNullException(nameof(pageUrlRetriever));
		_basePageRepository = basePageRepository ?? throw new ArgumentNullException(nameof(basePageRepository));
		_cultureRepository = siteCultureRepository ?? throw new ArgumentNullException(nameof(siteCultureRepository));
	}

        public Task<Dictionary<SiteCulture, NavigationItem>> GetWholeNavigationAsync(CancellationToken? cancellationToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<NavigationItem> GetNavigationAsync(SiteCulture? siteCulture = null, CancellationToken? cancellationToken = null)
        {
            throw new NotImplementedException();
        }

        public NavigationItem? GetNavigationItemByNodeId(int nodeId, NavigationItem startPointItem)
        {
            throw new NotImplementedException();
        }


		/// </summary>
		/// <remarks>Represents a navigation item in both modes: the content tree-based mode and the conventional routing one.</remarks>
		public class NavigationItem : BasicPage
		{
			/// <summary>
			/// Relative URL.
			/// </summary>
			public string? RelativeUrl { get; set; }

			/// <summary>
			/// Parent navigation item, if any.
			/// </summary>
			public NavigationItem? Parent { get; set; }

			/// <summary>
			/// All parent navigation items, up to the root.
			/// </summary>
			public List<NavigationItem> AllParents { get; } = new List<NavigationItem>();

			/// <summary>
			/// Child navigation items.
			/// </summary>
			public List<NavigationItem> ChildItems { get; } = new List<NavigationItem>();
		}
		/// <summary>
		/// Stores navigation.
		/// </summary>
		public interface INavigationRepository
		{
			/// <summary>
			/// Code names of page types with the 'Navigation item' feature enabled.
			/// </summary>
			IEnumerable<string> NavigationEnabledPageTypes { get; }

			/// <summary>
			/// Cache dependency keys computed out of <see cref="NavigationEnabledPageTypes"/>.
			/// </summary>
			IEnumerable<string> NavigationEnabledTypeDependencies { get; }

			/// <summary>
			/// Gets navigation hierarchies of all site cultures.
			/// </summary>
			/// <returns>Dictionary with navigation hierarchies per each site culture.</returns>
			Task<Dictionary<SiteCulture, NavigationItem>> GetWholeNavigationAsync(CancellationToken? cancellationToken = default);

			/// <summary>
			/// Gets a navigation hierarchy for a specified or actual site culture, further constrained by the starting node alias path.
			/// </summary>
			/// <param name="siteCulture">Site culture.</param>
			/// <returns>Navigation item in a given culture.</returns>
			Task<NavigationItem> GetNavigationAsync(SiteCulture? siteCulture = default, CancellationToken? cancellationToken = default);

			/// <summary>
			/// Traverses the hierarchy to find a navigation item by node ID.
			/// </summary>
			/// <param name="nodeId">Node ID.</param>
			/// <param name="startPointItem">Starting point navigation item.</param>
			/// <returns>Navigation item.</returns>
			NavigationItem? GetNavigationItemByNodeId(int nodeId, NavigationItem startPointItem);
		}
		public class NavigationRepository : INavigationRepository
		{
			private const string RootPath = "/";

			private static readonly string[] NodeOrdering = new string[] { "NodeLevel", "NodeOrder" };

			private readonly IMemoryCache _memoryCache;

			private readonly ICacheDependencyAdapter _cacheDependencyAdapter;

			private readonly IPageUrlRetriever _pageUrlRetriever;

			private readonly IPageRepository<BasicPage, TreeNode> _basePageRepository;

			private readonly ISiteCultureRepository _cultureRepository;

			public IEnumerable<string> NavigationEnabledPageTypes => DataClassInfoProvider
				.GetClasses()
				.Where(classInfo => classInfo.ClassIsNavigationItem)
				.Select(classInfo => classInfo.ClassName);

			public IEnumerable<string> NavigationEnabledTypeDependencies => NavigationEnabledPageTypes
				.Select(pageType => $"nodes|{SiteContext.CurrentSiteName}|{pageType}|all");

			private NavigationItem RootDto => _basePageRepository.GetPagesInCurrentCulture(query =>
				query
					.Path(RootPath, PathTypeEnum.Single)
					.TopN(1),
				buildCacheAction: cache => cache
					.Key($"{nameof(NavigationRepository)}|{nameof(RootDto)}"))
					.Select(basePageDto => new NavigationItem
					{
						NodeId = basePageDto.NodeId,
						Name = basePageDto.Name
					})
					.FirstOrDefault();

			public NavigationRepository(
				IMemoryCache memoryCache,
				ICacheDependencyAdapter cacheDependencyAdapter,
				IPageUrlRetriever pageUrlRetriever,
				IPageRepository<BasicPage, TreeNode> basePageRepository,
				ISiteCultureRepository siteCultureRepository)
			{
				_memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
				_cacheDependencyAdapter = cacheDependencyAdapter ?? throw new ArgumentNullException(nameof(cacheDependencyAdapter));
				_pageUrlRetriever = pageUrlRetriever ?? throw new ArgumentNullException(nameof(pageUrlRetriever));
				_basePageRepository = basePageRepository ?? throw new ArgumentNullException(nameof(basePageRepository));
				_cultureRepository = siteCultureRepository ?? throw new ArgumentNullException(nameof(siteCultureRepository));
			}
		}


		/// <summary>
		/// Get a relative URL of a page for a navigation item.
		/// </summary>
		/// <param name="item">Navigation item.</param>
		/// <returns>Relative URL.</returns>
		private string? GetPageUrl(NavigationItem item)
		{
			var culture = item?.Culture?.IsoCode;

			try
			{
				var url = _pageUrlRetriever.Retrieve(item?.NodeAliasPath, culture)?.RelativePath?.ToLowerInvariant()!;

				return url;
			}
			catch
			{
				return null;
			}

		}





	}
}
