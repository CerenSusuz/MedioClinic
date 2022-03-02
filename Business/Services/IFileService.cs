using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using Core;
using XperienceAdapter.Models;
using Business.Models;


namespace Business.Services
{
	/// <summary>
	/// As the file service is universal enough to cater to all kinds of file uploads, create it in the Business project.
	/// </summary>
	public interface IFileService : IService
	{
		/// <summary>
		/// Validates a form file and converts it into <see cref="UploadedFile"/>.
		/// The method accepts the input IFormFile object, along with an array of permitted file extensions, and a file size limit.
		/// </summary>
		/// <param name="formFile">Input file.</param>
		/// <param name="permittedExtensions">Permitted file name extensions.</param>
		/// <param name="sizeLimit">File size limit.</param>
		/// <returns>Uploaded file.</returns>
		Task<ProcessedFile> ProcessFormFileAsync(IFormFile formFile, string[] permittedExtensions, long sizeLimit);

		/// <summary>
		/// Sanitizes a file name and extension.
		/// The method accepts a string with a complete file name.
		/// The method returns a tuple that consists of two strings—a file name and a file extension.
		/// </summary>
		/// <param name="completeFileName">Input file name.</param>
		/// <returns>Name and extension.</returns>
		(string Name, string Extension) GetSafeFileName(string completeFileName);
	}
}
