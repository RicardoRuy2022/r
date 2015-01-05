using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AcupunturaXML;
using DomainModel;

namespace AcupunturaWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        DBHandler dbHandler = new DBHandler();
        private Dictionary<string, UtilizadorWEB> utilizadores;
        private Dictionary<string, Token> tokens;

        public Service1()
        {
            this.utilizadores = new Dictionary<string, UtilizadorWEB>();
            this.tokens = new Dictionary<string, Token>();
        }

        //Token:

        private class Token
        {
            private string value;
            private DateTime dataLogin;
            private DateTime dataExpirar;
            private int tempoSessao;
            private UtilizadorWEB utilizador;

            public Token(UtilizadorWEB utilizador) : this(utilizador, DateTime.Now) { }

            public Token(UtilizadorWEB utilizador, DateTime dataLogin)
            {
                tempoSessao = 4;
                this.value = Guid.NewGuid().ToString();
                this.dataLogin = dataLogin;
                this.dataExpirar = dataLogin.AddHours(tempoSessao);
                this.utilizador = utilizador;
            }

            public string Value { get { return value; } }
            public DateTime DataExpirar { get { return dataExpirar; } }
            public UtilizadorWEB Utilizador { get { return utilizador; } }
            public string Username { get { return utilizador.username; } }
            public Boolean isTimeOutExpired() { return dataExpirar < DateTime.Now; }

        }

        //Autenticacao:

        public string logIn(String username, String password)
        {
            cleanUpTokens();
            lerUtilizadoresBD();

            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password) && password.Equals(utilizadores[username].password))
            {
                Token tokenObject = new Token(utilizadores[username]);
                tokens.Add(tokenObject.Value, tokenObject);
                return tokenObject.Value;
            }
            else
            {
                throw new ArgumentException("Invalid username or password.");
            }
        }

        private void lerUtilizadoresBD()
        {
            List<UtilizadorWEB> listaUtilizadoresWeb = new List<UtilizadorWEB>();
            List<Utilizador> listaUtilizador = dbHandler.getUtilizadores();

            foreach (Utilizador u in listaUtilizador)
            {
                UtilizadorWEB util = new UtilizadorWEB();
                util.username = u.username;
                util.password = u.password;
                util.isAdmin = u.isAdmin;
                util.id = u.Id;
                if (!verificaUtilizador(util))
                    utilizadores.Add(util.username, util);
            }



        }

        private bool verificaUtilizador(UtilizadorWEB util)
        {
            foreach (KeyValuePair<String, UtilizadorWEB> u in utilizadores)
            {
                if (u.Value.username.Equals(util.username))
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
            return tokens[token].Utilizador.isAdmin;
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
                throw new ArgumentException("Invalid token.");
            }
            try
            {
                tokenObject = tokens[token];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("User not logged in or session expired.");
            }
            if (tokenObject.isTimeOutExpired())
            {
                tokens.Remove(tokenObject.Username);
                throw new Exception("Session expired.");
            }
            if (mustBeAdmin && !tokens[token].Utilizador.isAdmin)
            {
                throw new ArgumentException("Only administrators can manage that operation.");
            }
            return tokenObject;

        }


        public List<UtilizadorWEB> getAllUtilizadores(string token)
        {
            checkAuthentication(token, false);
            List<Utilizador> listaUtilizadores = new List<Utilizador>();
            List<UtilizadorWEB> listaFinal = new List<UtilizadorWEB>();

            listaUtilizadores = dbHandler.getUtilizadores();

            foreach (Utilizador u in listaUtilizadores)
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

        //Tratamento do XML:

//Devia estar assim:
//Mas para dar tem de estar como em baixo.

        public void writeToXml(string token, List<Sintomma> listaSintomas, List<Diagnostico> listaDiagnosticos)
        {
            checkAuthentication(token, false);
            List<DomainModel.Sintomma> listaS = new List<DomainModel.Sintomma>();

            foreach (Sintomma s in listaSintomas)
            {
                DomainModel.Sintomma sin = new DomainModel.Sintomma(s.nome);
                listaS.Add(sin);
            }

            List<DomainModel.Diagnostico> listaD = new List<DomainModel.Diagnostico>();

            XmlHandler.writeToXmlFile(listaS, listaD);
        }

        //public void writeToXml(string token, string listaSintomas, string listaDiagnosticos)
        //{
        //    checkAuthentication(token, false);
        //    List<DomainModel.Sintomma> listaS = new List<DomainModel.Sintomma>();
        //    String[] list = listaSintomas.Split('\n');
        //    foreach (String s in list)
        //    {
        //        DomainModel.Sintomma sin = new DomainModel.Sintomma(s);
        //        listaS.Add(sin);
        //    }

        //    List<DomainModel.Diagnostico> listaD = new List<DomainModel.Diagnostico>();

        //    XmlHandler.writeToXmlFile(listaS, listaD);
        //}

    }
}
