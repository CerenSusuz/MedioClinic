using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using XperienceAdapter.Models;
using XperienceAdapter.Services;

namespace XperienceAdapter.Repositories
{
	/// <summary>
	/// Provides base functionality to retrieve pages.
	/// </summary>
	/// <typeparam name="TPageDto">Page DTO.</typeparam>
	/// <typeparam name="TPage">Xperience page.</typeparam>
	public abstract class BasePageRepository<TPageDto, TPage> : IPageRepository<TPageDto, TPage>
		where TPageDto : BasicPage, new()
		where TPage : TreeNode, new()
	{
		protected readonly IRepositoryServices _repositoryServices;

		/// <summary>
		/// Default DTO factory method.
		/// </summary>
		protected virtual Func<TPageDto> DefaultDtoFactory => () => new TPageDto();

		public BasePageRepository(IRepositoryServices repositoryDependencies)
		{
			_repositoryServices = repositoryDependencies ?? throw new ArgumentNullException(nameof(repositoryDependencies));
		}

        public IEnumerable<TPageDto> GetPagesInCurrentCulture(Action<DocumentQuery<TPage>>? filter = null, Func<TPage, TPageDto, TPageDto>? additionalMapper = null, Action<IPageCacheBuilder<TPage>>? buildCacheAction = null, bool includeAttachments = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TPageDto>> GetPagesInCurrentCultureAsync(CancellationToken? cancellationToken = null, Action<DocumentQuery<TPage>>? filter = null, Func<TPage, TPageDto, TPageDto>? additionalMapper = null, Action<IPageCacheBuilder<TPage>>? buildCacheAction = null, bool includeAttachments = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TPageDto> GetPagesByTypeAndCulture(IEnumerable<string> types, SiteCulture culture, string cacheKey, Action<MultiDocumentQuery>? filter = null, Func<TPage, TPageDto, TPageDto>? additionalMapper = null, bool includeAttachments = false, params string[] cacheDependencies)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TPageDto>> GetPagesByTypeAndCultureAsync(IEnumerable<string> types, SiteCulture culture, string cacheKey, Action<MultiDocumentQuery>? filter = null, Func<TPage, TPageDto, TPageDto>? additionalMapper = null, bool includeAttachments = false, CancellationToken? cancellationToken = null, params string[] cacheDependencies)
        {
            throw new NotImplementedException();
        }

        public void MapDtoProperties(TPage page, TPageDto dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TPageDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TPageDto>> GetAllAsync(CancellationToken? cancellationToken = null)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// a method that helps in preparing the query objects. 
        /// use the interface to configure common querying parameters for both DocumentQuery and Multi Document Query objects.
        /// However, since all DocumentQuery retrievals in the MedioClinic solution can be handled through IPageRetriever (i.e. without the need to use DocumentHelper), this method will in fact only make sure all queries are configured in the same way as IPageRetriever. 
        /// The method will only be used in the callstack of the GetPagesByTypeAndCulture and GetPagesByTypeAndCultureAsync methods.
        /// </summary>
        /// <typeparam name="TQuery"></typeparam>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="query"></param>
        /// <param name="siteCulture"></param>
        /// <returns></returns>
        protected virtual TQuery FilterFor<TQuery, TObject>(IDocumentQuery<TQuery, TObject> query, SiteCulture? siteCulture = default)
            where TQuery : IDocumentQuery<TQuery, TObject>
            where TObject : TreeNode, new()
        {
            var typedQuery = query.GetTypedQuery();

            typedQuery
                .OnSite(_repositoryServices.SiteService.CurrentSite.SiteName);

            if (siteCulture != null)
            {
                typedQuery.Culture(siteCulture.IsoCode);
            }

            if (_repositoryServices.SiteContextService.IsPreviewEnabled)
            {
                typedQuery
                    .LatestVersion()
                    .Published(false);
            }
            else
            {
                typedQuery
                    .Published()
                    .PublishedVersion();
            }

            return typedQuery;
        }
    }
}
