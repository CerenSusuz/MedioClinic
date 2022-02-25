using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using CMS.DocumentEngine;
using CMS.Membership;

using XperienceAdapter.Extensions;
using XperienceAdapter.Repositories;
using XperienceAdapter.Services;
using Business.Extensions;
using Business.Models;


namespace Business.Repositories
{
	public class DoctorRepository : BasePageRepository<Doctor, CMS.DocumentEngine.Types.MedioClinic.Doctor>
	{
		private readonly IUserInfoProvider _userInfoProvider;

		public DoctorRepository(IRepositoryServices repositoryServices, IUserInfoProvider userInfoProvider) : base(repositoryServices)
		{
			_userInfoProvider = userInfoProvider ?? throw new ArgumentNullException(nameof(userInfoProvider));
		}

		private static DayOfWeek? GetShiftDayOfWeek(IEnumerable<TreeNode> dayOfWeekPage) =>
			dayOfWeekPage.ToDaysOfWeek().FirstOrDefault();
	}
}
