using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcupunturaXML
{
    public class Pessoa
    {
        private string nome;
        private int bi;
        private DateTime dataNascimento;

        public Pessoa(string nome, int bi, DateTime dataNascimento) {
            this.nome = nome;
            this.bi = bi;
            this.dataNascimento = dataNascimento;
        }

        public string Nome
        {
            get { return nome; }  
            set { nome = value; }
        }

        public int Bi
        {
            get { return bi; }
            set { bi = value; }
        }
        
        public DateTime DataNascimento
        {
            get { return dataNascimento; }
            set { dataNascimento = value; }
        }
        

        
        

    }
}
