using System;

namespace Server_Manager
{
    public class ServerConfig
    {
        public string Name { get; set; } = "";
        public string Host { get; set; } = "";
        public int Port { get; set; } = 22;
        public string User { get; set; } = "";
        public string Mode { get; set; } = "SSH";
        public string? Url { get; set; } = "";
        public bool? IsOnline { get; set; } = null;

        public bool IsMode(string mode) => string.Equals(Mode, mode, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        ///  The bare host name to probe or connect to: for WEB entries the URL is reduced
        ///  to its authority, so "https://example.com/admin" resolves to "example.com".
        /// </summary>
        public string ResolveHostName()
        {
            string raw = (IsMode("WEB") && !string.IsNullOrWhiteSpace(Url) ? Url! : Host).Trim();
            if (raw.Length == 0) return "";

            // Uri only parses an authority when a scheme is present; supply a placeholder.
            string candidate = raw.Contains("://") ? raw : "rcc://" + raw;
            if (Uri.TryCreate(candidate, UriKind.Absolute, out var uri) && !string.IsNullOrEmpty(uri.Host))
                return uri.Host;

            int slash = raw.IndexOf('/');
            return slash >= 0 ? raw.Substring(0, slash) : raw;
        }

        /// <summary>
        ///  Host names are passed to external processes, so reject anything that is not a
        ///  plausible host: no whitespace and no shell metacharacters.
        /// </summary>
        public static bool IsValidHostName(string? host)
        {
            if (string.IsNullOrWhiteSpace(host)) return false;

            foreach (char c in host)
            {
                if (char.IsLetterOrDigit(c)) continue;
                if (c is '.' or '-' or '_' or ':') continue;
                return false;
            }
            return true;
        }

        public static bool IsValidUserName(string? user)
        {
            if (string.IsNullOrWhiteSpace(user)) return false;

            foreach (char c in user)
            {
                if (char.IsLetterOrDigit(c)) continue;
                if (c is '.' or '-' or '_' or '\\' or '@' or '$') continue;
                return false;
            }
            return true;
        }
    }
}
