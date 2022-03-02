﻿using Business.Models;
using CMS.DocumentEngine;
using Core.Configuration;
using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using MedioClinic.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using XperienceAdapter.Localization;
using XperienceAdapter.Repositories;

[assembly: RegisterPageRoute(CMS.DocumentEngine.Types.MedioClinic.SiteSection.CLASS_NAME, typeof(DoctorsController), ActionName = nameof(DoctorsController.Index), Path = "/Doctors")]
[assembly: RegisterPageRoute(CMS.DocumentEngine.Types.MedioClinic.Doctor.CLASS_NAME, typeof(DoctorsController), ActionName = nameof(DoctorsController.Detail))]
namespace MedioClinic.Controllers
{
    public class DoctorsController : BaseController
    {
        private readonly IPageDataContextRetriever _pageDataContextRetriever;

        private readonly IPageRepository<Doctor, CMS.DocumentEngine.Types.MedioClinic.Doctor> _doctorRepository;

        public DoctorsController(
            ILogger<DoctorsController> logger,
            IOptionsMonitor<XperienceOptions> optionsMonitor,
            IStringLocalizer<SharedResource> stringLocalizer,
            IPageDataContextRetriever pageDataContextRetriever,
            IPageRepository<Doctor, CMS.DocumentEngine.Types.MedioClinic.Doctor> doctorRepository)
            : base(logger, optionsMonitor, stringLocalizer)
        {
            _pageDataContextRetriever = pageDataContextRetriever ?? throw new ArgumentNullException(nameof(pageDataContextRetriever));
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
        }


		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			if (_pageDataContextRetriever.TryRetrieve<CMS.DocumentEngine.Types.MedioClinic.SiteSection>(out var pageDataContext)
				&& pageDataContext.Page != null)
			{
				var doctorsPath = pageDataContext.Page.NodeAliasPath;

				var doctorPages = await _doctorRepository.GetPagesInCurrentCultureAsync(
					cancellationToken,
					filter => filter
						.FilterDuplicates()
						.Path(doctorsPath, PathTypeEnum.Children),
					buildCacheAction: cache => cache
						.Key($"{nameof(DoctorsController)}|Doctors")
						.Dependencies((_, builder) => builder
							.PageType(CMS.DocumentEngine.Types.MedioClinic.Doctor.CLASS_NAME)
							.PagePath(doctorsPath, PathTypeEnum.Children)
							.PageOrder()));

				if (doctorPages?.Any() == true)
				{
					var data = (pageDataContext.Page.DocumentName, doctorPages);
					var viewModel = GetPageViewModel(pageDataContext.Metadata, data);

					return View(viewModel);
				}
			}

			return NotFound();
		}






	}
}
