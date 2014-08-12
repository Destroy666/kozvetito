using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kozvetito.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString MailTo(this HtmlHelper helper, string emailAddress, string displayText = null)
        {
            if (string.IsNullOrEmpty(displayText)) displayText = emailAddress;

            var builder = new TagBuilder("a");
            builder.MergeAttribute("href", "mailto:" + emailAddress);
            builder.SetInnerText(displayText);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }
    }
}