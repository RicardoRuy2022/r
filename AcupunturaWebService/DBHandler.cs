using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace AcupunturaWebService
{
    public class DBHandler
    {

        AcupunturaModelContainer modelo = new AcupunturaModelContainer();

        public Boolean logIn(string username, string password)
        {
            Boolean isLogged = false;
            Utilizador u = new Utilizador();
            try
            {
                u = modelo.UtilizadorSet.Where(i => i.username == username).First();
                if (u.password == password)
                    isLogged = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isLogged;
        }
                
        public List<Utilizador> getUtilizadores()
        {
            List<Utilizador> listaUtilizadores = new List<Utilizador>();
            listaUtilizadores = modelo.UtilizadorSet.ToList();

            return listaUtilizadores;
        }
        public Utilizador getUtilizadorIdTerapeura(int idTerapeuta)
        {
            return modelo.UtilizadorSet.Where(i => i.Terapeuta.Id == idTerapeuta).First();
        }
        public Terapeuta getTerapeutaPorBi(int bi, Boolean isAdmin) { 
            Terapeuta t = new Terapeuta();

            if (isAdmin)
            {
                t = modelo.TerapeutaSet.Where(i => i.bi == bi).First();
            }
            return t;
        }

        public Paciente getPacientePorBi(int bi, Boolean isAdmin, int idTerapeuta )
        {
            Paciente p = new Paciente();
           
            if (isAdmin)
            {
                p = modelo.PacienteSet.Where(i => i.bi == bi).First();

            }
            else if (p.Terapeuta == getTerapeutaID(idTerapeuta))
            {
                p = modelo.PacienteSet.Where(i => i.bi == bi).First();

            }
            return p;


        }

        public Boolean adicionarPaciente(string nome, int bi, DateTime dataNascimento, int idUtilizador, Boolean isAdmin) {
            Boolean resultado;
            Paciente p = new Paciente();

            if (isAdmin)
            {
                p.nome = nome;
                p.bi = bi;
                p.data_nascimento = dataNascimento;
                //id sem terapeuta corresponde ao id Utilizador = 2
                p.Terapeuta = getTerapeutaID(2);
            }
            else if (!isAdmin)
            {
                p.nome = nome;
                p.bi = bi;
                p.data_nascimento = dataNascimento;
                p.Terapeuta = getTerapeutaID(idUtilizador);
            }

            try { 
            modelo.PacienteSet.Add(p);
            modelo.SaveChanges();
            resultado = true;
            }
            catch 
            {
                resultado = false;
            }

            return resultado;
        }
        public Boolean removerAdministrador(string username, Boolean isAdmin)
        {
            Boolean resultado;
            try
            {
                Utilizador u = getAdministradorUsername(username, isAdmin);
                modelo.UtilizadorSet.Remove(u);
                modelo.SaveChanges();

                resultado = true;
            }
            catch
            {
                resultado = false;
            }

            return resultado;
        }
        public Boolean removerPaciente(int bi, Boolean isAdmin, int idTerapeuta)
        {
            Boolean resultado;
       
           
            try
            {
                Paciente p = getPacientePorBi(bi, isAdmin, idTerapeuta);
                modelo.PacienteSet.Remove(p); 
                modelo.SaveChanges();
                resultado = true;
            }
            catch
            {
                resultado = false;
            }

            return resultado;
        }

        public Boolean removerTerapeuta(int bi, Boolean isAdmin)
        {
            Boolean resultado;
            try
            {
                Terapeuta t = getTerapeutaPorBi(bi, isAdmin);
                updateDoTerapeutaAoPaciente(t.Id, isAdmin);
                int idUtil = t.Utilizador.Id;
                Utilizador u = new Utilizador();
                modelo.TerapeutaSet.Remove(t);
                modelo.SaveChanges();
                u = modelo.UtilizadorSet.Where(i => i.Id == idUtil).First();
                modelo.UtilizadorSet.Remove(u);
                modelo.SaveChanges();
                resultado = true;
            }
            catch
            {
                resultado = false;
            }

            return resultado;
        }

     

        public Boolean updateDoTerapeutaAoPaciente(int idTerapeuta, Boolean isAdmin)
        {
            Boolean resultado = false;
            try
            {
            if (isAdmin)
            {
                List<Paciente> listaPacientes = modelo.PacienteSet.Where(i => i.Terapeuta.Id == idTerapeuta).ToList();
                foreach (Paciente p in listaPacientes)
                {
                    p.Terapeuta = getTerapeutaID(2);
                    modelo.SaveChanges();
                }
              }
             resultado = true;
            }
            catch
            {
                resultado = false;
            }

            return resultado;
        }

        public Boolean editarPaciente(int idTerapeuta, Boolean isAdmin, string nome, int bi, DateTime dataNascimento)
        {
            Boolean resultado;
            Paciente p = getPacientePorBi(bi, isAdmin, idTerapeuta);
            if (isAdmin)
            {
                p.nome = nome;
                p.bi = bi;
                p.data_nascimento = dataNascimento;
                p.Terapeuta = getTerapeutaID(idTerapeuta);
            }
            else if (!isAdmin)
            {
                if(p.Terapeuta == getTerapeutaID(idTerapeuta)){
                    p.nome = nome;
                    p.bi = bi;
                    p.data_nascimento = dataNascimento;
                }
            }

            try
            {
                modelo.SaveChanges();
                resultado = true;
            }
            catch
            {
                resultado = false;
            }

            return resultado;
        }

        public Boolean editarTerapeuta(string nome, int bi, DateTime dataNascimento, string username, string password, Boolean isAdmin)
        {
            Terapeuta t = getTerapeutaPorBi(bi, isAdmin);
            if (isAdmin) { 
            t.nome = nome;
            t.bi = bi;
            t.data_nascimento = dataNascimento;
            t.Utilizador.username = username;
            t.Utilizador.password = password;
            }

            Boolean resultado;
            try
            {
                modelo.SaveChanges();
                resultado = true;
            }
            catch
            {
                resultado = false;
            }

            return resultado;
        }
        public Boolean editarAdministrador(string username, string password, Boolean isAdmin)
        {
            Utilizador t = getAdministradorUsername(username, isAdmin);
            if (isAdmin)
            {
                t.username = username;
                t.password = password;
            }

            Boolean resultado;
            try
            {
                modelo.SaveChanges();
                resultado = true;
            }
            catch
            {
                resultado = false;
            }

            return resultado;
        }


        public Boolean adicionarTerapeuta(string nome, int bi, DateTime dataNascimento, string username, string password, Boolean isAdmin)
        {
            Boolean resultado;
            Terapeuta t = new Terapeuta();
            Utilizador u = new Utilizador();
            if (isAdmin)
            {
                t.nome = nome;
                t.bi = bi;
                t.data_nascimento = dataNascimento;
                u.username = username;
                u.password = password;
                u.isAdmin = false;
            }
            
            try
            {
                t.Utilizador = u;
                modelo.TerapeutaSet.Add(t);
                modelo.SaveChanges();
                resultado = true;
            }
            catch
            {
                resultado = false;
            }

            return resultado;
        }

        public Boolean adicionarAdministrador(string username, string password, Boolean isAdmin)
        {
            Boolean resultado;
            Utilizador u = new Utilizador();
            if (isAdmin)
            {
                u.username = username;
                u.password = password;
                u.isAdmin = true;
            }

            try
            {
                modelo.UtilizadorSet.Add(u);
                modelo.SaveChanges();
                resultado = true;
            }
            catch
            {
                resultado = false;
            }

            return resultado;
        }


        public Terapeuta getTerapeutaID(int id) 
        {
            return modelo.TerapeutaSet.Where(i => i.Utilizador.Id == id).First();
        }
        public Utilizador getAdministradorUsername(string username, Boolean isAdmin)
        {
            Utilizador u = new Utilizador();

            if (isAdmin)
            {
                modelo.UtilizadorSet.Where(i => i.username == username).First();

            }
          
            return u;
        }

    }
}