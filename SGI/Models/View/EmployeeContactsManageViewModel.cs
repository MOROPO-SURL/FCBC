namespace MOROPO.FCBC.SGI.Models.View
{
    public class EmployeeContactsManageViewModel
    {
        public EmployeeContactsManageViewModel()
        {
            PhoneContacts = new System.Collections.Generic.List<EmployeePhoneContactDisplayViewModel>();
            EmailContacts = new System.Collections.Generic.List<EmployeeEmailContactDisplayViewModel>();
        }

        public EmployeeDisplayViewModel Employee { get; set; }

        public System.Collections.Generic.List<EmployeePhoneContactDisplayViewModel> PhoneContacts { get; set; }

        public System.Collections.Generic.List<EmployeeEmailContactDisplayViewModel> EmailContacts { get; set; }
    }
}

