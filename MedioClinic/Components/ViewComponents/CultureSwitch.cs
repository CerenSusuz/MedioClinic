#define no_suffix

using Business.Models;
using Business.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using XperienceAdapter.Repositories;

...

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












}