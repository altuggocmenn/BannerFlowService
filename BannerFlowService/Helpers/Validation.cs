using BannerFlowService.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BannerFlowService.Helpers
{
    public class Validation
    {
        public static bool IsHtml(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            if (htmlDoc.ParseErrors.Count() > 0)
                return false;
            else
                return true;
        }

        public static bool IsValidBanner(Banner banner)
        {
            bool isValid = true;

            if (banner.Created > DateTime.Now)
                isValid = false;

            if (banner.Modified != null && banner.Modified > banner.Created)
                isValid = false;

            isValid = IsHtml(banner.Html);

            return isValid;
        }
    }
}
