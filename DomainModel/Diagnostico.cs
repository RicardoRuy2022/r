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
            get { return orgao; }
        }
        public String getNome
        {
            get { return nome; }
        }
        public String getDescricao
        {
            get { return descricao; }
        }
        public String getTratamento
        {
            get { return tratamento; }
        }
        public List<Sintoma> getListaSintomas
        {
            get { return listaSintomas; }
        }
    }
}
