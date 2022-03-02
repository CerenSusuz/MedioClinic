using Business.Models;
using Core.Configuration;
using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using MedioClinic.Controllers;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
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
    }
}
