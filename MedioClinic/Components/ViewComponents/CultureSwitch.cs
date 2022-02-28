#define no_suffix

using Business.Models;
using Business.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XperienceAdapter.Extensions;
using XperienceAdapter.Models;
using XperienceAdapter.Repositories;


public class CultureSwitch : ViewComponent
{
	private readonly string[] ExcludedPaths =
	{
		"landing-pages",
		"paginas-de-destino"
	};

	private readonly INavigationRepository _navigationRepository;

	private readonly ISiteCultureRepository _siteCultureRepository;

	public CultureSwitch(ISiteCultureRepository siteCultureRepository, INavigationRepository navigationRepository)
	{
		_siteCultureRepository = siteCultureRepository ?? throw new ArgumentNullException(nameof(siteCultureRepository));
		_navigationRepository = navigationRepository ?? throw new ArgumentNullException(nameof(navigationRepository));
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="cultureSwitchId"></param>
	/// <returns></returns>
	public async Task<IViewComponentResult> InvokeAsync(string cultureSwitchId)
	{
		var variants = await GetUrlCultureVariantsAsync();
		var model = (cultureSwitchId, variants?.ToDictionary(kvp1 => kvp1.Key, kvp2 => kvp2.Value));

		return View(model);
	}


	/// <summary>
	/// a method that searches for a navigation item in the hierarchy by the relative URL.
	/// </summary>
	/// <param name="searchPath"></param>
	/// <param name="startingPointItem"></param>
	/// <returns></returns>
	private NavigationItem? GetNavigationItemByRelativeUrl(string searchPath, NavigationItem startingPointItem)
	{
		if (startingPointItem != null)
		{
			var parsed = Url.Content(startingPointItem.RelativeUrl);

			if (parsed?.Equals(searchPath, StringComparison.OrdinalIgnoreCase) == true)
			{
				return startingPointItem;
			}
			else if (startingPointItem.ChildItems?.Any() == true)
			{
				var matches = new List<NavigationItem>();

				foreach (var child in startingPointItem.ChildItems)
				{
					var childMatch = GetNavigationItemByRelativeUrl(searchPath, child);
					matches.Add(childMatch!);
				}

				return matches.FirstOrDefault(match => match != null);
			}
		}

		return null;
	}


	/// <summary>
	/// 
	/// </summary>
	/// <param name="searchPath"></param>
	/// <param name="currentCulture"></param>
	/// <returns></returns>
	private async Task<IEnumerable<KeyValuePair<SiteCulture, string>>>? GetDatabaseUrlVariantsAsync(string searchPath, SiteCulture currentCulture)
	{
		var navigation = await _navigationRepository.GetWholeNavigationAsync();
		var currentPageNavigationItem = GetNavigationItemByRelativeUrl(searchPath, navigation[currentCulture]);

		if (currentPageNavigationItem != null)
		{
			var databaseVariants = new List<KeyValuePair<SiteCulture, NavigationItem>>();
			databaseVariants.Add(new KeyValuePair<SiteCulture, NavigationItem>(currentCulture, currentPageNavigationItem));

			foreach (var cultureVariant in navigation.Where(cultureVariant => !cultureVariant.Key.Equals(currentCulture)))
			{
				var otherCultureNavigationItem = _navigationRepository.GetNavigationItemByNodeId(currentPageNavigationItem.NodeId, cultureVariant.Value);

				if (otherCultureNavigationItem != null)
				{
					databaseVariants.Add(new KeyValuePair<SiteCulture, NavigationItem>(cultureVariant.Key, otherCultureNavigationItem));
				}
			}

			return databaseVariants.Select(variant => new KeyValuePair<SiteCulture, string>(variant.Key, variant.Value.RelativeUrl!));
		}

		return null;
	}


	/// <summary>
	/// This method will localize the URL of the currently displayed page, in a similar way that the GetDatabaseUrlVariantsAsync method does.
	/// However, it will be used in cases when the currently displayed page does not come from any Xperience page in the content tree.Unlike the previous method, this one will only localize the culture URL segment, leaving the rest of the URL unchanged.
	/// </summary>
	/// <param name="searchPath"></param>
	/// <returns></returns>
	private IEnumerable<KeyValuePair<SiteCulture, string>>? GetNonDatabaseUrlVariants(string searchPath)
	{
		var cultures = _siteCultureRepository.GetAll();
		var segments = searchPath.Split('/');

		if (cultures.Any(culture => culture.IsoCode?.Equals(segments?[1], StringComparison.InvariantCultureIgnoreCase) == true))
		{
			var trailingPath = string.Join('/', segments.Skip(2));

			return cultures.Select(culture => new KeyValuePair<SiteCulture, string>(culture, $"/{culture.IsoCode?.ToLower()}/{trailingPath}"));
		}

		return null;
	}


	/// <summary>
	/// With both the GetDatabaseUrlVariantsAsync and GetNonDatabaseUrlVariants methods implemented, it is time to orchestrate the two to get localized URLs, either from the database, or otherwise.
	/// </summary>
	/// <returns></returns>
	private async Task<IEnumerable<KeyValuePair<SiteCulture, string>>>? GetUrlCultureVariantsAsync()
	{
		var defaultCulture = _siteCultureRepository.DefaultSiteCulture;
		var completePath = string.IsNullOrEmpty(Request.PathBase) ? Request.Path.Value : $"{Request.PathBase}{Request.Path.Value}";
		var searchPath = Request.Path.Equals("/") && defaultCulture != null ? $"/{defaultCulture.IsoCode?.ToLowerInvariant()}/home/" : completePath;
		var currentCulture = Thread.CurrentThread.CurrentUICulture.ToSiteCulture();

		if (currentCulture != null && !ExcludedPaths.Any(path => searchPath.Contains(path)))
		{
			return await GetDatabaseUrlVariantsAsync(searchPath, currentCulture) ?? GetNonDatabaseUrlVariants(searchPath);
		}

		return null;
	}


}