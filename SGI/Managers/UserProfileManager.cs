using MOROPO.SAPRESSC.Core.Web.Security.Models.View.Contrat;

namespace MOROPO.FCBC.SGI.Managers
{
    public class UserProfileManager : SAPRESSC.Core.Web.Security.Managers.Facade.IUserProfileManagerFacade
    {
        public IUserProfileViewModel GetUserProfile(string username)
        {
            throw new System.NotImplementedException();
        }

        public void PreValidateUserProfile(string username, IUserProfileViewModel profileModel, Microsoft.AspNetCore.Mvc.Controller controller)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUserProfile(string username, IUserProfileViewModel profileModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
