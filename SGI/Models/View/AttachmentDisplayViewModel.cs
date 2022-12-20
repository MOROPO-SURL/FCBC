namespace MOROPO.FCBC.SGI.Models.View
{
    public class AttachmentDisplayViewModel
    {
        public System.Guid Id { get; set; }

        public string Extension { get; set; }

        public bool IsPublic { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public void Import(Persistence.Entities.Contract.AbstractAttachment entity)
        {
            Id = entity.Id;
            Extension = entity.ContentType.Split('/')[0].ToLower();
            Name = entity.Name;
            IsPublic = entity.IsPublic;
            Description = entity.Description;
        }
    }
}
