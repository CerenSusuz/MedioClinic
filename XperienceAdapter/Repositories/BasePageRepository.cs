using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CMS.DocumentEngine;
using CMS.Helpers;
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



        /// <summary>
        /// The method has exactly the same set of parameters, however, it returns IEnumerable<Page>.
        /// Since the GetPagesByTypeAndCulture* methods will be used for the sole purpose of retrieving pages of types inheriting fields from a common page type, the current method can return TPage instead of TreeNode.
        /// </summary>
        /// <param name="types"></param>
        /// <param name="culture"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected IEnumerable<TPage> GetPagesOfMultipleTypes(
    IEnumerable<string> types,
    SiteCulture culture,
    Action<MultiDocumentQuery>? filter = default)
        {
            MultiDocumentQuery query = GetQueryForMultipleTypes(types, culture, filter);

            return query
                .GetEnumerableTypedResult()
                .Select(page => page as TPage)
                .Where(page => page != null)!;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        /// <param name="culture"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected async Task<IEnumerable<TPage>> GetPagesOfMultipleTypesAsync(
            IEnumerable<string> types,
            SiteCulture culture,
            Action<MultiDocumentQuery>? filter = default)
        {
            MultiDocumentQuery query = GetQueryForMultipleTypes(types, culture, filter);

            return (await query
                .GetEnumerableTypedResultAsync())
                .Select(page => page as TPage)
                .Where(page => page != null)!;
        }

        /// <summary>
        ///  lets you specify page types that will be retrieved, culture, and a custom MultiDocumentQuery filter.
        /// </summary>
        /// <param name="types"></param>
        /// <param name="culture"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected MultiDocumentQuery GetQueryForMultipleTypes(
            IEnumerable<string> types,
            SiteCulture? culture,
            Action<MultiDocumentQuery>? filter)
        {
            var query = new MultiDocumentQuery();

            query = FilterFor(query, culture)
                .Types(types.ToArray())
                .WithCoupledColumns();

            filter?.Invoke(query);

            return query;
        }


        /// <summary>
        /// Maps basic Xperience page properties onto DTO ones.
        /// </summary>
        /// <param name="page">Xperience page.</param>
        /// <param name="includeAttachments">Indicates if attachment information shall be included.</param>
        /// <returns>Page DTO.</returns>
        protected virtual TPageDto MapBasicDtoProperties(TPage page, bool includeAttachments)
        {
            var dto = DefaultDtoFactory();
            dto.Guid = page.DocumentGUID;
            dto.NodeId = page.NodeID;
            dto.Name = page.DocumentName;
            dto.NodeAliasPath = page.NodeAliasPath;
            dto.ParentId = page.NodeParentID;
            dto.Culture = _repositoryServices.SiteCultureRepository.GetByExactIsoCode(page.DocumentCulture);

            if (includeAttachments)
            {
                foreach (var attachment in page.Attachments)
                {
                    dto.Attachments.Add(new PageAttachment
                    {
                        AttachmentUrl = _repositoryServices.PageAttachmentUrlRetriever.Retrieve(attachment),
                        Extension = attachment.AttachmentExtension,
                        FileName = attachment.AttachmentName,
                        Guid = attachment.AttachmentGUID,
                        Id = attachment.ID,
                        MimeType = attachment.AttachmentMimeType,
                        Title = attachment.AttachmentTitle
                    });
                }
            }

            return dto;
        }


        /// <summary>
        /// Default DTO mapping method.
        /// </summary>
        /// <param name="page">Xperience page.</param>
        /// <param name="dto">Page DTO.</param>
        public virtual void MapDtoProperties(TPage page, TPageDto dto) { }

        /// <summary>
        /// Maps query results onto DTOs.
        /// </summary>
        /// <param name="pages">Xperience pages.</param>
        /// <param name="additionalMapper">Ad-hoc mapper supplied as a parameter.</param>
        /// <param name="includeAttachments">Indicates if attachment information shall be included.</param>
        /// <returns>Page DTOs.</returns>
        protected IEnumerable<TPageDto> MapPages(
            IEnumerable<TPage?>? pages = default,
            Func<TPage, TPageDto, TPageDto>? additionalMapper = default,
            bool includeAttachments = default)
        {
            if (pages != null && pages.Any())
            {
                if (additionalMapper != null)
                {
                    foreach (var page in pages)
                    {
                        var dto = ApplyMappers(page!, includeAttachments);

                        if (dto != null)
                        {
                            yield return additionalMapper(page!, dto);
                        }
                    }
                }
                else
                {
                    foreach (var page in pages)
                    {
                        var dto = ApplyMappers(page!, includeAttachments);

                        if (dto != null)
                        {
                            yield return dto;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Applies the basic mapper as well as the type-specific one.
        /// </summary>
        /// <param name="page">Xperience page.</param>
        /// <param name="includeAttachments">Indicates if attachment information shall be included.</param>
        /// <returns>Page DTO.</returns>
        protected TPageDto ApplyMappers(TPage page, bool includeAttachments)
        {
            var dto = MapBasicDtoProperties(page, includeAttachments);
            MapDtoProperties(page, dto);

            return dto;
        }


        /// <summary>
        /// create a method that produces the CacheSettings objects. 
        /// The method will only be called from within the two GetPagesByTypeAndCulture* interface methods, since the other two use IPageRetriever internally (with no need to work with a standalone CacheSettings object).
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="cacheDependencies"></param>
        /// <returns></returns>
        protected static CacheSettings GetCacheSettings(string cacheKey, params string[] cacheDependencies)
        {
            var settings = new CacheSettings(TimeSpan.FromMinutes(10).TotalMinutes, cacheKey);
            settings.CacheDependency = CacheHelper.GetCacheDependency(cacheDependencies);

            return settings;
        }



    }
}
