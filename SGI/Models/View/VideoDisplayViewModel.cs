namespace MOROPO.FCBC.SGI.Models.View
{
    public class VideoDisplayViewModel
    {
        public System.Guid Id { get; set; }

        public string Extension { get; set; }

        public bool IsPublic { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Microsoft.AspNetCore.Html.HtmlString Icon { get; set; }

        public void Import(Persistence.Entities.Contract.AbstractAttachment entity)
        {
            Id = entity.Id;
            Extension = entity.ContentType.Split('/')[0].ToLower();
            Name = entity.Name;
            IsPublic = entity.IsPublic;
            Description = entity.Description;
            Icon = GetAttachmentIcon(entity.ContentType);
        }

        public VideoInputViewModel MergeViewModels()
        {
            VideoInputViewModel viewModel = new VideoInputViewModel();
            viewModel.Id = Id;
            viewModel.Name = Name;
            viewModel.IsPublic = IsPublic;
            viewModel.Description = Description;
            return viewModel;
        }

        private Microsoft.AspNetCore.Html.HtmlString GetAttachmentIcon(string contentType)
        {
            switch (contentType)
            {
                case string x when x.Contains("pdf"):
                    return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-36px mdi-file-pdf\"></span>");
                case string x when x.Contains("word"):
                    return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-36px mdi-file-word\"></span>");
                case string x when (x.Contains("excel") || (x.Contains("sheet"))):
                    return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-36px mdi-file-excel\"></span>");
                case string x when x.Contains("image"):
                    return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-36px mdi-file-image\"></span>");
                case string x when x.Contains("video"):
                    return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-36px mdi-file-video\"></span>");
                default:
                    return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-36px mdi-file\"></span>");
            }
        }
    }
}
