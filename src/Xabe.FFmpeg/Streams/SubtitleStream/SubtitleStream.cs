﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Xabe.FFmpeg
{
    /// <inheritdoc />
    public class SubtitleStream : ISubtitleStream
    {
        private string _configuredLanguage;
        private string _split;

        /// <inheritdoc />
        public string Codec { get; internal set; }

        /// <inheritdoc />
        public string Path { get; internal set; }

        /// <inheritdoc />
        public string Build()
        {
            var builder = new StringBuilder();
            builder.Append(_split);
            builder.Append(BuildLanguage());
            return builder.ToString();
        }

        /// <inheritdoc />
        public string BuildInputArguments()
        {
            return null;
        }

        /// <inheritdoc />
        public int Index { get; internal set; }

        /// <inheritdoc />
        public string Language { get; internal set; }

        /// <inheritdoc />
        public int? Default { get; internal set; }

        /// <inheritdoc />
        public int? Forced { get; internal set; }

        /// <inheritdoc />
        public string Title { get; internal set; }

        public StreamType StreamType => StreamType.Subtitle;

        /// <inheritdoc />
        public ISubtitleStream SetLanguage(string lang)
        {
            if (!string.IsNullOrEmpty(lang))
            {
                _configuredLanguage = lang;
            }
            return this;
        }

        /// <inheritdoc />
        public IEnumerable<string> GetSource()
        {
            return new[] { Path };
        }

        private string BuildLanguage()
        {
            string language = string.Empty;
            language = !string.IsNullOrEmpty(_configuredLanguage) ? _configuredLanguage : Language;
            if (!string.IsNullOrEmpty(language))
            {
                language = $"-metadata:s:s:{Index} language={language} ";
            }
            return language;
        }

        private void Split(TimeSpan startTime, TimeSpan duration)
        {
            _split = $"-ss {startTime.ToFFmpeg()} -t {duration.ToFFmpeg()} ";
        }
    }
}