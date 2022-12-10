using MOROPO.FBCB.SGI.Persistence.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ManifestationKindInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<ManifestationKind, System.Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ManifestationKind Parent { get; set; }

        [Display(Name = "Pertenece a:")]
        public System.Guid? ParentId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ParentsManifestationKinds { get; set; }

        public ManifestationKind Export()
        {
            var entity = new ManifestationKind();

            Mergue(entity);

            return entity;
        }

        public void Import(ManifestationKind entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Parent = entity.Parent;
        }

        public void Mergue(ManifestationKind entity)
        {
            entity.Name = Name;
            entity.Parent = Parent;
        }
    }
}
