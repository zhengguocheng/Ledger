using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace DMS
{
    public class AuthenticationService
    {
        public static User LoginUser { get; set; }
        public static bool IsAuthenticated { get; set; }

        public static bool Authenticate(string userName, string password)
        {
            LoginUser = new UserController().Authenticate(userName, password);
            IsAuthenticated = (LoginUser != null);
            return IsAuthenticated;
        }

        public static void ReloadLoginedUser()
        {
            LoginUser = new UserController().Find(LoginUser.ID);
        }

        public static void Logout()
        {
            LoginUser = null;
            IsAuthenticated = false;
        }

        public static bool IsAdminLogin()
        {
            if (string.Compare(LoginUser.Role.Trim(), AppConstants.Role_Admin.Trim(), true) == 0)
                return true;
            else
                return false;
            
        }
    }
}
