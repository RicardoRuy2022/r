using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Diagnostico
    {
        string orgao { get; set; }
        string nome { get; set; }
        string descricao { get; set; }
        string tratamento { get; set; }
        List<Sintoma> listaSintomas { get; set; }

        public Diagnostico(string orgao, string nome, string descricao, string tratamento, List<Sintoma> listaSintomas)
        {
            this.orgao = orgao;
            this.nome = nome;
            this.descricao = descricao;
            this.tratamento = tratamento;
            this.listaSintomas = listaSintomas;
        }
    }
}
