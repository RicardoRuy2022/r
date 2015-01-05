using System;
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
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "writeToXmlFile?token={token}&listaSintomas={listaSintomas}&listaDiagnosticos={listaDiagnosticos}")]
        //Devia estar assim:
        void writeToXml(string token, List<Sintomma> listaSintomas, List<Diagnostico> listaDiagnosticos);
        //void writeToXml(string token, string listaSintomas, string listaDiagnosticos);
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
        [DataMember]
        public string nome { get; set; }
    }

    [DataContract]
    public class Diagnostico
    {
        [DataMember]
        public string orgao { get; set; }
        [DataMember]
        public string nome { get; set; }
        [DataMember]
        public string descricao { get; set; }
        [DataMember]
        public string tratamento { get; set; }
        [DataMember]
        public List<Sintomma> listaSintomas { get; set; }
    }
}
