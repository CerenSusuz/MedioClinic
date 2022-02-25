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
    }
}
