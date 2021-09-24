using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    public class Login : Statement
    {
        public string username = null;
        public string password = null;
        
        public Login()
        {
            statement = State.LOGIN;
        }
    }
}
