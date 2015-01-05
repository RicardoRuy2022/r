using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Utilizador
    {
        private string username;
        private string password;
        private Boolean isAdmin;

        public Utilizador(string username, string password, Boolean isAdmin) {
            this.username = username;
            this.password = password;
            this.isAdmin = isAdmin;
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        
        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        public Boolean IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }
        
        
    }
}
