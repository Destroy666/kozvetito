using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc.Html;

namespace kozvetito.Helpers
{
    public static class CultureHelper
    {
        private static readonly List<string> _validCultures = new List<string> { "hu", "hu-HU", "de", "de-DE", "de-AT" };

        private static readonly List<string> _cultures = new List<string>
        {
            "hu",
            "de"
        };

        public static bool IsRightToLeft()
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft;
        }

        public static string GetImplementedCulture(string name)
        {
            if (string.IsNullOrEmpty(name)) return GetDefaultCulture();

            if (_validCultures.Where(c => c.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Count() == 0)
                return GetDefaultCulture();

            if (_cultures.Where(c => c.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Count() > 0)
                return name;

            var n = GetNeutralCulture(name);

            foreach (var c in _cultures)
            {
                if (c.StartsWith(n)) 
                    return c;
            }

            return GetDefaultCulture();
        }

        public static string GetDefaultCulture()
        {
            return _cultures[0];
        }

        public static string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

        public static string GetCurrentNeutralCulture()
        {
            return GetNeutralCulture(Thread.CurrentThread.CurrentCulture.Name);
        }

        public static string GetNeutralCulture(string name)
        {
            if (name.Length < 2) return name;
            return name.Substring(0, 2);
        }
    }
}