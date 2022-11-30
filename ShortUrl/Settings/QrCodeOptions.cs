using System;

namespace ShortUrl.Settings
{
    public class QrCodeOptions
    {
        public const string Position = "QrCodeSettings";

        public string Path { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
    }
}