﻿using System;

namespace FlubuCore.Tasks.Versioning
{
    public class BuildVersion
    {
        public Version Version { get; set; }

        public string VersionQuality { get; set; }

        public string BuildVersionWithQuality(int versionFieldCount)
        {
            string quality = null;

            if (!string.IsNullOrEmpty(VersionQuality))
            {
                quality = VersionQuality.StartsWith("-") ? VersionQuality : $"-{VersionQuality}";
            }

            return $"{Version.ToString(versionFieldCount)}{quality}";
        }
    }
}
