#define no_suffix

using Business.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
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
}