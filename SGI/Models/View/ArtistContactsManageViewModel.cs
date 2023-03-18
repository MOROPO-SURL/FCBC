namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistContactsManageViewModel
    {
        public ArtistContactsManageViewModel()
        {
            PhoneContacts = new System.Collections.Generic.List<ArtistPhoneContactDisplayViewModel>();
            EmailContacts = new System.Collections.Generic.List<ArtistEmailContactDisplayViewModel>();
            SocialContacts = new System.Collections.Generic.List<ArtistSocialContactDisplayViewModel>();
        }

        public ArtistDisplayViewModel Artist { get; set; }

        public System.Collections.Generic.List<ArtistPhoneContactDisplayViewModel> PhoneContacts { get; set; }

        public System.Collections.Generic.List<ArtistEmailContactDisplayViewModel> EmailContacts { get; set; }

        public System.Collections.Generic.List<ArtistSocialContactDisplayViewModel> SocialContacts { get; set; }

    }
}
