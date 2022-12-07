namespace MOROPO.FCBC.SGI.Models.View
{
    public class AssociationContactsManageViewModel
    {
        public AssociationContactsManageViewModel()
        {
            PhoneContacts = new System.Collections.Generic.List<AssociationPhoneContactDisplayViewModel>();
            EmailContacts = new System.Collections.Generic.List<AssociationEmailContactDisplayViewModel>();
        }

        public AssociationDisplayViewModel Association { get; set; }

        public System.Collections.Generic.List<AssociationPhoneContactDisplayViewModel> PhoneContacts { get; set; }

        public System.Collections.Generic.List<AssociationEmailContactDisplayViewModel> EmailContacts { get; set; }
    }
}
