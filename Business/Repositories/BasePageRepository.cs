using CMS.DocumentEngine;
using System;
using System.Collections.Generic;
using System.Text;
using XperienceAdapter.Models;
using XperienceAdapter.Repositories;
using XperienceAdapter.Services;

namespace Business.Repositories
{
	/// <summary>
	/// Stores pages without page type-specific data (coupled data).
	///  this class only compensates for the abstract nature of XperienceAdapter.Repositories.BasePageRepository.
	/// </summary>
	public class BasePageRepository : BasePageRepository<BasicPage, TreeNode>
	{
		public BasePageRepository(IRepositoryServices repositoryDependencies) : base(repositoryDependencies)
		{
		}
	}
}
