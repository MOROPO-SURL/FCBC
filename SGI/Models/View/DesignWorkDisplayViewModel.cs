using MOROPO.FCBC.SGI.Persistence.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class DesignWorkDisplayViewModel: SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.DesignWork, System.Guid>
    {
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        public DesignManifestationDisplayViewModel Manifestation { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image FeatureImage { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image[] Images { get; set; }

        public DesignTechniqueDisplayViewModel[] Techniques { get; set; }

        public System.Collections.Generic.ICollection<DesignWorkAward> Awards { get; set; }

        public void Import(DesignWork entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Manifestation = new DesignManifestationDisplayViewModel();
            FeatureImage = entity.FeatureImage;
            Images = entity.ImageBridges.Select(bridge => bridge.Image).ToArray();
            Techniques = entity.TechniqueBridges.Select(bridge =>
            {
                var model = new DesignTechniqueDisplayViewModel();

                model.Import(bridge.Technique);

                return model;
            }).ToArray();
            Awards = entity.Awards;

            Manifestation.Import(entity.Manifestation);
        }
    }
}
