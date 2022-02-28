using MedioClinic.Middleware;
using Microsoft.AspNetCore.Builder;
using System;

namespace MedioClinic.Extensions
{
	public static class ApplicationBuilderExtensions
	{
		/// <summary>
		/// Attaches the culture recognition middleware to the request pipeline.
		/// </summary>
		/// <param name="builder">Application builder.</param>
		/// <returns>Modified application builder.</returns>
		public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
		{
			if (builder is null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			return builder.UseMiddleware<CultureMiddleware>();
		}
	}
}
