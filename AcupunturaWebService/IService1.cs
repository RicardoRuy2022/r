﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AcupunturaWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        //Autenticacao
        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "login?username={username}&password={password}")]
        string logIn(string username, string password);

        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "logout")]
        void logOut(string token);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "isAdmin?token={token}")]
        bool isAdmin(string token);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "isLoggedIn?token={token}")]
        bool isLoggedIn(string token);
        //-----------------------------------------
        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "getAllUtilizadores?token={token}")]
        List<UtilizadorWEB> getAllUtilizadores(string token);

        //XML:

        [OperationContract]
        void writeToXmlFile(LinkedList<Sintomma> listaSintomas, LinkedList<Diagnostico> listaDiagnosticos);
    }

    [DataContract]
    public class UtilizadorWEB
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public Boolean isAdmin { get; set; }
    }

    [DataContract]
    public class Sintomma
    {
        string _nome;

        [DataMember]
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
    }
    [DataContract]
    public class Diagnostico
    {

        string orgao;
        string nome;
        string descricao;
        string tratamento;
        List<Sintoma> listaSintomas;

        [DataMember]
        public String getOrgao
        {
            get { return orgao; }
        }
        [DataMember]
        public String getNome
        {
            get { return nome; }
        }
        [DataMember]
        public String getDescricao
        {
            get { return descricao; }
        }
        [DataMember]
        public String getTratamento
        {
            get { return tratamento; }
        }
        [DataMember]
        public List<Sintoma> getListaSintomas
        {
            get { return listaSintomas; }
        }
    }
}
