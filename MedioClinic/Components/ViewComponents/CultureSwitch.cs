#define no_suffix

using Business.Models;
using Business.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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







}