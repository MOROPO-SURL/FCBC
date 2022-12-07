namespace MOROPO.FCBC.SGI.Models.View
{
    public class ContractorContactsManageViewModel
    {
        public ContractorContactsManageViewModel()
        {
            PhoneContacts = new System.Collections.Generic.List<ContractorPhoneContactDisplayViewModel>();
            EmailContacts = new System.Collections.Generic.List<ContractorEmailContactDisplayViewModel>();
        }

        public ContractorDisplayViewModel Contractor { get; set; }

        public System.Collections.Generic.List<ContractorPhoneContactDisplayViewModel> PhoneContacts { get; set; }

        public System.Collections.Generic.List<ContractorEmailContactDisplayViewModel> EmailContacts { get; set; }
    }
}
