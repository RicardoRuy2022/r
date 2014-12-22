using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcupunturaWebService
{
    public class DBHandler
    {

        AcupunturaModelContainer modelo = new AcupunturaModelContainer();

        /*
         * Autenticacao
         * */

        public String logIn(string username, string password)
        {
            String texto = "";
            Utilizador c = new Utilizador();
            try
            {
                c = modelo.UtilizadorSet.Where(i => i.username == username).First();
                if (c.password == password)
                    texto = "S";
                else
                    texto = "N";
            }
            catch (Exception e)
            {
                texto = "User not available";
                Console.WriteLine(e.ToString());
            }

            return texto;
        }

        /**/

        public List<Utilizador> getContas()
        {
            List<Utilizador> listaContas = new List<Utilizador>();
            listaContas = modelo.UtilizadorSet.ToList();

            return listaContas;
        }
    }
}