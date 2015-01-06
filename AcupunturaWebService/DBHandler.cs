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

        public Terapeuta getTerapeutaPorBi(int bi) {

            Terapeuta t = new Terapeuta();
            t = modelo.TerapeutaSet.Where(i => i.bi == bi).First();     
            return t;
        }
        public Paciente getPacientePorBi(int bi)
        {
            Paciente p = new Paciente();
            p = modelo.PacienteSet.Where(i => i.bi == bi).First();
            return p;
        }

        public Boolean adicionarPaciente(string nome, int bi, DateTime dataNascimento, int idUtilizador) {
            Boolean resultado;
            Paciente p = new Paciente();
            p.nome = nome;
            p.bi = bi;
            p.data_nascimento = dataNascimento;
            p.Terapeuta = getTerapeutaID(idUtilizador);
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

        public Terapeuta getTerapeutaID(int id) 
        {
            return modelo.TerapeutaSet.Where(i => i.Utilizador.Id == id).First();
        }

    }
}