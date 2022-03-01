using Core.Configuration;
using Kentico.Content.Web.Mvc;
using MedioClinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using XperienceAdapter.Localization;

namespace MedioClinic.Controllers
{
    public abstract class BaseController : Controller
    {

        protected readonly ILogger<BaseController> _logger;

        protected readonly IOptionsMonitor<XperienceOptions> _optionsMonitor;

        protected readonly IStringLocalizer<SharedResource> _stringLocalizer;


        public BaseController(ILogger<BaseController> logger,
                          IOptionsMonitor<XperienceOptions> optionsMonitor,
                          IStringLocalizer<SharedResource> stringLocalizer)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _optionsMonitor = optionsMonitor ?? throw new ArgumentNullException(nameof(optionsMonitor));
            _stringLocalizer = stringLocalizer ?? throw new ArgumentNullException(nameof(stringLocalizer));
        }
        protected PageViewModel GetPageViewModel(
            IPageMetadata pageMetadata,
            string? message = default,
            bool displayMessage = true,
            bool displayAsRaw = default,
            MessageType messageType = MessageType.Info) =>
            PageViewModel.GetPageViewModel(
                pageMetadata,
                message,
                displayMessage,
                displayAsRaw,
                messageType);



        protected PageViewModel<TViewModel> GetPageViewModel<TViewModel>(
            IPageMetadata pageMetadata,
            TViewModel data,
            string? message = default,
            bool displayMessage = true,
            bool displayAsRaw = default,
            MessageType messageType = MessageType.Info)
            =>
            PageViewModel<TViewModel>.GetPageViewModel(
                data,
                pageMetadata,
                message,
                displayMessage,
                displayAsRaw,
                messageType);





    }
}
