using System;
using System.Collections.Generic;
using System.Text;

using XperienceAdapter.Models;

namespace Business.Models
{
    /// <summary>
    /// To make work with uploaded files more convenient, you'll want to wrap both the uploaded file and the processing result state into one single object. Chances are, you'll pass the processed file onto multiple other places in your code base. Therefore, it is better to have a wrapper instead of a two-piece tuple
    /// </summary>
    public class ProcessedFile : IDisposable
    {
        private bool _disposed;

        public UploadedFile? UploadedFile { get; }

        public FormFileResultState ResultState { get; }

        public ProcessedFile(FormFileResultState formFileResultState, UploadedFile? uploadedFile = default)
        {
            UploadedFile = uploadedFile ?? throw new ArgumentNullException(nameof(uploadedFile));
            ResultState = formFileResultState;
        }

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
                UploadedFile?.Dispose(); 
            }

            _disposed = true;
        }
    }
}
