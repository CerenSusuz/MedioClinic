using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;

using Core.Configuration;
using XperienceAdapter.Repositories;
using Business.Models;
using MedioClinic.Controllers;
using XperienceAdapter.Localization;
using Microsoft.Extensions.Localization;

[assembly: RegisterPageRoute(CMS.DocumentEngine.Types.MedioClinic.HomePage.CLASS_NAME, typeof(HomeController))]
namespace MedioClinic.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IPageDataContextRetriever _pageDataContextRetriever;

        private readonly IPageRepository<HomePage, CMS.DocumentEngine.Types.MedioClinic.HomePage> _homePageRepository;

        private readonly IPageRepository<CompanyService, CMS.DocumentEngine.Types.MedioClinic.CompanyService> _companyServiceRepository;

        public HomeController(
            ILogger<HomeController> logger,
            IOptionsMonitor<XperienceOptions> optionsMonitor,
            IStringLocalizer<SharedResource> stringLocalizer,
            IPageDataContextRetriever pageDataContextRetriever,
            IPageRepository<HomePage, CMS.DocumentEngine.Types.MedioClinic.HomePage> homePageRepository,
            IPageRepository<CompanyService, CMS.DocumentEngine.Types.MedioClinic.CompanyService> companyServiceRepository)
            : base(logger, optionsMonitor, stringLocalizer)
        {
            _pageDataContextRetriever = pageDataContextRetriever ?? throw new ArgumentNullException(nameof(pageDataContextRetriever));
            _homePageRepository = homePageRepository ?? throw new ArgumentNullException(nameof(homePageRepository));
            _companyServiceRepository = companyServiceRepository ?? throw new ArgumentNullException(nameof(companyServiceRepository));
        }s
    }
}