using MOROPO.FCBC.SGI.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ManifestationKindInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<ArtisticManifestationGroup, System.Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Pertenece a:")]
        public System.Guid? ParentId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ParentsManifestationKinds { get; set; }

        public string SerializedTree { get; set; }

        public ArtisticManifestationGroup Export()
        {
            var entity = new ArtisticManifestationGroup();

            Mergue(entity);

            return entity;
        }

        public void Import(ArtisticManifestationGroup entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ParentId = entity.Parent?.Id;
        }

        public void Mergue(ArtisticManifestationGroup entity)
        {
            entity.Name = Name;
            entity.ParentId = ParentId;
        }
    }
}
