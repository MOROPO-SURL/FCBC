namespace MOROPO.FCBC.SGI.Extensions
{
    public static class ManagementDashboardSectionExtensions
    {
        public static Microsoft.AspNetCore.Html.HtmlString ToIconographyHtmlString(this SAPRESSC.Core.Web.Backend.Models.View.ManagementDashboardSectionViewModel target)
        {
            if (target.Controller == "users") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-account\"></span>");

            if (target.Controller == "roles") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-account-group\"></span>");

            if (target.Controller == "traces") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-foot-print\"></span>");

            if (target.Controller == "recognitions") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-trophy-award\"></span>");

            if (target.Controller == "distinctions") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-certificate\"></span>");

            if (target.Controller == "awards") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-trophy\"></span>");

            if (target.Controller == "agreements") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-handshake\"></span>");

            if (target.Controller == "services") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-diamond-stone\"></span>");

            if (target.Controller == "thematics") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-tag-text\"></span>");

            if (target.Controller == "techniques") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-drama-masks\"></span>");

            if (target.Controller == "contractorkinds") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-home-city\"></span>");

            if (target.Controller == "currencies") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-cash-multiple\"></span>");

            if (target.Controller == "contractors") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-domain\"></span>"); // mdi-domain

            if (target.Controller == "critics") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-tie\"></span>"); // mdi-tie

            if (target.Controller == "artists") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-account-tie\"></span>");

            if (target.Controller == "creationgroups") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-garage-variant\"></span>");

            if (target.Controller == "associations") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-bank\"></span>");

            if (target.Controller == "administrativeroles") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-graph\"></span>");

            if (target.Controller == "employees") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-worker\"></span>");

            if (target.Controller == "resolutions") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-file-certificate\"></span>");

            if (target.Controller == "information") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-information-variant \"></span>");

            if (target.Controller == "manifestationkinds") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-briefcase-account\"></span>");

            if (target.Controller == "unitmeasurements") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-ruler\"></span>");

            if (target.Controller == "materials") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-hexagon-multiple\"></span>");

            if (target.Controller == "activitykinds") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-calendar-multiple\"></span>");

            if (target.Controller == "activities") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-calendar\"></span>");

            if (target.Controller == "artisticmanifestations" ||
                target.Controller == "artisticmanifestationgroups" ||
                target.Controller == "artistictechniques" ||
                target.Controller == "artworks") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-palette\"></span>");

            if (target.Controller == "craftmanifestations" ||
                target.Controller == "craftmanifestationgroups" ||
                target.Controller == "crafttechniques" ||
                target.Controller == "craftworks") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-shoe-heel\"></span>");

            if (target.Controller == "servicelines" ||
                target.Controller == "servicelinegroups" ||
                target.Controller == "serviceworks" ||
                target.Controller == "servicetechniques") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-saw-blade\"></span>");

            if (target.Controller == "designmanifestations" ||
                target.Controller == "designmanifestationgroups" ||
                target.Controller == "designtechniques" ||
                target.Controller == "designworks") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-image-edit\"></span>");

            return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-toy-brick-outline\"></span>");
        }
    }
}
