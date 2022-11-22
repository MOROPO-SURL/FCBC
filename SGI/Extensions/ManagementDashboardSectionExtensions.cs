namespace MOROPO.FCBC.SGI.Extensions
{
    public static class ManagementDashboardSectionExtensions
    {
        public static Microsoft.AspNetCore.Html.HtmlString ToIconographyHtmlString(this SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Models.View.ManagementDashboardSectionViewModel target)
        {
            if (target.Controller == "users") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-account\"></span>");

            if (target.Controller == "groups") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-account-group\"></span>");

            if (target.Controller == "traces") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-foot-print\"></span>");

            if (target.Controller == "artisticmanifestations") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-palette\"></span>");

            if (target.Controller == "awards") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-trophy-award\"></span>");

            if (target.Controller == "distinctions") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-certificate\"></span>");

            if (target.Controller == "recognitions") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-handshake\"></span>");

            if (target.Controller == "services") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-face-agent\"></span>");

            if (target.Controller == "thematics") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-folder-table\"></span>");

            if (target.Controller == "techniques") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-drama-masks\"></span>");

            if (target.Controller == "contractorkinds") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-tag-text\"></span>");

            if (target.Controller == "currencies") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-currency-usd \"></span>");

            if (target.Controller == "critics") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-book-search \"></span>");

            return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-toy-brick-outline\"></span>");
        }
    }
}
