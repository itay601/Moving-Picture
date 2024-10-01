using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingPictureWindowsForms
{
    public class Authentication
    {
        public static bool CheckUserCredentials(string username, string password)
        {
            //TODO
            //future - implement check with Database
            if(username == "kali" && password == "kali")
            {
                return true;
            }
            return false;
        }
    }
}
