namespace MOROPO.FCBC.SGI.Models.View
{
    public class AttachmentDisplayViewModel
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

        public AttachmentInputViewModel MergeViewModels()
        {
            AttachmentInputViewModel viewModel = new AttachmentInputViewModel();
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
                case string x when (x.Contains("excel")||(x.Contains("sheet"))):
                    return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-36px mdi-file-excel\"></span>");
                case string x when x.Contains("image"):
                    return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-36px mdi-file-image\"></span>");
                default:
                    return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-36px mdi-file\"></span>");
            }
        }
    }
}
