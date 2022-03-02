using System;
using System.IO;
using Microsoft.AspNetCore.Http;

using CMS.Base;

namespace XperienceAdapter.Models
{
	/// <summary>
	/// The IUploadedFile interface became a crown prince of working with uploaded files in Xperience 13. It has its default internal implementations in Xperience for both .NET Framework and .NET Standard. You will define your own application-level implementation.
	/// </summary>
	public class UploadedFile : IUploadedFile, IDisposable
	{
		private readonly IFormFile _formFile;

		private string? _contentType;

		private string? _fileName;

		private bool _disposed;

		/// <summary>
		/// Gets a value that indicates whether this implementation supports accessing the input stream
		/// via <see cref="OpenReadStream"/> method. This property always returns <see langword="true"/>.
		/// </summary>
		/// <return>
		/// <see langword="true"/> to indicate that the input stream must be accessed via <see cref="OpenReadStream"/> method.
		/// </return>
		public bool CanOpenReadStream => true;


		//The ContentType and FileName properties are not strictly needed to accomplish the goals of this course, however, they come in handy when you construct the UploadedFile object with a FormFile object that has been transformed from the original (e.g. renamed). Then, you can store your own content type and name information in the backing fields.
		//Since FormFile is an IDisposable, you should follow Microsoft's best practices to make UploadedFile also implement that interface.

		/// <summary>
		/// Gets the MIME content type of an uploaded file.
		/// </summary>
		public string ContentType
		{
			get => _formFile.ContentType ?? _contentType;
			set => _contentType = value;
		}


		/// <summary>
		/// Gets the size of an uploaded file in bytes.
		/// </summary>
		public long Length => _formFile.Length;


		/// <summary>
		/// Gets the fully qualified name of the file on the client.
		/// </summary>
		public string FileName
		{
			get => _formFile.FileName ?? _fileName;
			set => _fileName = value;
		}


		/// <summary>
		/// Gets a <see cref="Stream"/> object that points to an uploaded file.
		/// This property is not currently supported.
		/// The input stream must be accessed via <see cref="OpenReadStream"/> method. 
		/// </summary>
		/// <exception cref="NotSupportedException">
		/// Thrown always.
		/// </exception>
		public Stream InputStream =>
			throw new NotSupportedException($"Input stream must be accessed via {nameof(OpenReadStream)}() method.");


		/// <summary>
		/// Initializes a new instance of the <see cref="UploadedFile"/> class.
		/// </summary>
		/// <param name="formFile">The <see cref="IFormFile"/>.</param>
		public UploadedFile(IFormFile formFile)
		{
			_formFile = formFile ?? throw new ArgumentNullException(nameof(formFile));
		}


		/// <summary>
		/// Opens the request stream for reading the uploaded file.
		/// </summary>
		public Stream OpenReadStream() =>
			_formFile.OpenReadStream();

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}

			if (disposing)
			{
				OpenReadStream()?.Dispose();
			}

			_disposed = true;
		}
	}
}
