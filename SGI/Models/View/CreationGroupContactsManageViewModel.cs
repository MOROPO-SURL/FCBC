namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupContactsManageViewModel
    {
        public CreationGroupContactsManageViewModel()
        {
            SocialContacts = new System.Collections.Generic.List<CreationGroupSocialContactDisplayViewModel>();

            PhoneContacts = new System.Collections.Generic.List<CreationGroupPhoneContactDisplayViewModel>();

            EmailContacts = new System.Collections.Generic.List<CreationGroupEmailContactDisplayViewModel>();
        }

        public CreationGroupDisplayViewModel Group { get; set; }

        public System.Collections.Generic.List<CreationGroupSocialContactDisplayViewModel> SocialContacts { get; set; }

        public System.Collections.Generic.List<CreationGroupPhoneContactDisplayViewModel> PhoneContacts { get; set; }

        public System.Collections.Generic.List<CreationGroupEmailContactDisplayViewModel> EmailContacts { get; set; }
    }
}
