using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Diagnostico
    {
        string orgao;
        string nome;
        string descricao;
        string tratamento;
        List<Sintoma> listaSintomas;

        public Diagnostico(string orgao, string nome, string descricao, string tratamento, List<Sintoma> listaSintomas)
        {
            this.orgao = orgao;
            this.nome = nome;
            this.descricao = descricao;
            this.tratamento = tratamento;
            this.listaSintomas = listaSintomas;
        }

        public String getOrgao
        {
            get;
            set;
        }
        public String getNome
        {
            get;
            set;
        }
        public String getDescricao
        {
            get;
            set;
        }
        public String getTratamento
        {
            get;
            set;
        }
        public List<Sintoma> getListaSintomas
        {
            get;
            set;
        }
    }
}
