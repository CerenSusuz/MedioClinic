using Business.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class FileService : IFileService
	{
		// If you require a check on specific characters in the IsValidFileExtensionAndSignature
		// method, supply the characters in the _allowedChars field.
		private static readonly byte[] _allowedChars = { };

		// For more file signatures, see the File Signatures Database (https://www.filesignatures.net/)
		// and the official specifications for the file types you wish to add.
		private static readonly Dictionary<string, List<byte[]>> _fileSignature = new Dictionary<string, List<byte[]>>
	{
		{ ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
		{ ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
		{ ".jpeg", new List<byte[]>
			{
				new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
				new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
				new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
			}
		},
		{ ".jpg", new List<byte[]>
			{
				new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
				new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
				new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
			}
		},
		{ ".zip", new List<byte[]>
			{
				new byte[] { 0x50, 0x4B, 0x03, 0x04 },
				new byte[] { 0x50, 0x4B, 0x4C, 0x49, 0x54, 0x45 },
				new byte[] { 0x50, 0x4B, 0x53, 0x70, 0x58 },
				new byte[] { 0x50, 0x4B, 0x05, 0x06 },
				new byte[] { 0x50, 0x4B, 0x07, 0x08 },
				new byte[] { 0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70 },
			}
		},
	};

        public (string Name, string Extension) GetSafeFileName(string completeFileName)
        {
            throw new NotImplementedException();
        }

        public Task<ProcessedFile> ProcessFormFileAsync(IFormFile formFile, string[] permittedExtensions, long sizeLimit)
        {
            throw new NotImplementedException();
        }
    }
}
