using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Sintomma
    {
        string nome;

        public Sintomma(string nome)
        {
            this.nome = nome;
        }

        public String getNome
        {
            get { return nome; }
            set { nome = value; }
        }
    }
}
