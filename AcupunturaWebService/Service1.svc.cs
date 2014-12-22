using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AcupunturaWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        DBHandler dbHandler = new DBHandler();
        private Dictionary<string, UtilizadorWEB> contas;
        private Dictionary<string, Token> tokens;

        public Service1()
        {

            this.contas = new Dictionary<string, UtilizadorWEB>();
            this.tokens = new Dictionary<string, Token>();
        }
        private class Token
        {
            private string value;
            private DateTime dataLogin;
            private DateTime dataExpirar;
            private int HORAS;
            private UtilizadorWEB conta;

            public Token(UtilizadorWEB conta) : this(conta, DateTime.Now) { }

            public Token(UtilizadorWEB conta, DateTime dataLogin)
            {
                HORAS = 10;
                this.value = Guid.NewGuid().ToString();
                this.dataLogin = dataLogin;
                this.dataExpirar = dataLogin.AddHours(HORAS);
                this.conta = conta;
            }

            public string Value { get { return value; } }
            public DateTime DataExpirar { get { return dataExpirar; } }
            public UtilizadorWEB Conta { get { return conta; } }
            public string Username { get { return conta.username; } }
            //  public void UpdateTimeout() { UpdateTimeout(240000); }
            // public void UpdateTimeout(long timeout) { this.timeout = Environment.TickCount + timeout; }
            public Boolean isTimeOutExpired() { return dataExpirar < DateTime.Now; }

        }

        //authentication

        public string logIn(String username, String password)
        {
            cleanUpTokens();
            lercontasBD();

            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password) && password.Equals(contas[username].password))
            {
                Token tokenObject = new Token(contas[username]);
                tokens.Add(tokenObject.Value, tokenObject);
                return tokenObject.Value;
            }
            else
            {
                throw new ArgumentException("ERROR: invalid username/password combination");
            }
            // return handler.logIn(username, password);
        }

        private void lercontasBD()
        {
            List<UtilizadorWEB> listaContasWeb = new List<UtilizadorWEB>();
            List<Utilizador> listaConta = dbHandler.getContas();

            foreach (Utilizador c in listaConta)
            {
                UtilizadorWEB con = new UtilizadorWEB();
                con.username = c.username;
                con.password = c.password;
                con.isAdmin = c.isAdmin;
                con.id = c.Id;
                if (!verificaConta(con))
                    contas.Add(con.username, con);
            }



        }

        private bool verificaConta(UtilizadorWEB con)
        {
            foreach (KeyValuePair<String, UtilizadorWEB> c in contas)
            {
                if (c.Value.username.Equals(con.username))
                    return true;
            }
            return false;
        }

        public void logOut(string token)
        {
            tokens.Remove(token);
            cleanUpTokens();

        }

        public bool isAdmin(string token)
        {
            return tokens[token].Conta.isAdmin;
        }

        public bool isLoggedIn(string token)
        {
            bool res = true;
            try
            {
                checkAuthentication(token, false);
            }
            catch (ArgumentException)
            {
                res = false;
            }
            return res;
        }

        private void cleanUpTokens()
        {
            foreach (Token tokenObject in tokens.Values)
            {

                tokens.Remove(tokenObject.Username);

            }
        }

        private Token checkAuthentication(string token, bool mustBeAdmin)
        {
            Token tokenObject;
            if (String.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Error: invalid token value.");
            }
            try
            {
                tokenObject = tokens[token];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("Error: user is not logged in /expired session?).");
            }
            if (tokenObject.isTimeOutExpired())
            {
                tokens.Remove(tokenObject.Username);
                throw new Exception("Error: the session has expired. Please Loged in again.");
            }
            if (mustBeAdmin && !tokens[token].Conta.isAdmin)
            {
                throw new ArgumentException("Error: only admins are allowed to perform this operation.");
            }
            return tokenObject;

        }


      public  List<UtilizadorWEB> getAllUtilizadores(string token) 
        {
            checkAuthentication(token, false);
            List<Utilizador> listaUtilizadores = new List<Utilizador>();
            List<UtilizadorWEB> listaFinal = new List<UtilizadorWEB>();

            listaUtilizadores = dbHandler.getContas();

            foreach( Utilizador u in listaUtilizadores)
            {
                UtilizadorWEB util = new UtilizadorWEB();
                util.id = u.Id;
                util.isAdmin = u.isAdmin;
                util.username = u.username;
                util.password = u.password;
                listaFinal.Add(util);
            }

            return listaFinal;
        }


        //fim authentication
    }
}
