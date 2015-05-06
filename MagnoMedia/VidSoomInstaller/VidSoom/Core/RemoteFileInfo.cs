using System;
using System.Collections.Generic;
using System.Text;

namespace MyDownloader.Core
{
    [Serializable]
    public class RemoteFileInfo
    {
        private bool acceptRanges;
        private long fileSize;
        private DateTime lastModified = DateTime.MinValue;

        private string mimeType;
        private string fileName;

        public string MimeType
        {
            get { return mimeType; }
            set { mimeType = value; }
        }

        public bool AcceptRanges
        {
            get { return acceptRanges; }
            set { acceptRanges = value; }
        }

        public long FileSize
        {
            get { return fileSize; }
            set { fileSize = value; }
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public DateTime LastModified
        {
            get { return lastModified; }
            set { lastModified = value; }
        }

    }
}
