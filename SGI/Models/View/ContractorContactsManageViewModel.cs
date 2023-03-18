namespace MOROPO.FCBC.SGI.Models.View
{
    public class ContractorContactsManageViewModel
    {
        public ContractorContactsManageViewModel()
        {
            PhoneContacts = new System.Collections.Generic.List<ContractorPhoneContactDisplayViewModel>();
            EmailContacts = new System.Collections.Generic.List<ContractorEmailContactDisplayViewModel>();
            SocialContacts = new System.Collections.Generic.List<ContractorSocialContactDisplayViewModel>();
        }

        public ContractorDisplayViewModel Contractor { get; set; }

        public System.Collections.Generic.List<ContractorPhoneContactDisplayViewModel> PhoneContacts { get; set; }

        public System.Collections.Generic.List<ContractorEmailContactDisplayViewModel> EmailContacts { get; set; }

        public System.Collections.Generic.List<ContractorSocialContactDisplayViewModel> SocialContacts { get; set; }
    }
}
