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
        UriTemplate = "logout?token={token}")]
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
        BodyStyle=WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "writeToXmlFile?token={token}")]
        void writeToXml(string token, List<SintomaWEB> listaSintomas, List<DiagnosticoWEB> listaDiagnosticos);
      
        //ADICIONAR PACIENTE:

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "adicionarPaciente?token={token}")]
        Boolean adicionarPaciente(string token, string nome, int bi, DateTime dataNascimento);
       
        //PESQUISAR PACIENTE POR BI :

        [OperationContract]
        [WebInvoke(Method = "GET",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "getPacientePorBi?token={token}&bi={bi}")]
        PacienteWEB getPacientePorBi(string token, int bi);

        //GET LISTA SINTOMAS

        [OperationContract]
        [WebInvoke(Method = "GET",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "getListaSintomasXml?token={token}")]
        List<SintomaWEB> getListaSintomasXml(string token);

        //GET LISTA DE TODOS OS DIAGNOSTICOS

        [OperationContract]
        [WebInvoke(Method = "GET",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "getAllDiagnosticosXml?token={token}")]
        List<string> getAllDiagnosticosXml(string token);

        //GET LISTA DIAGNOSTICOS DEPENDENDO DE UMA LISTA DE SINTOMAS

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "getListaDiagnosticosXml?token={token}")]
        List<string> getListaDiagnosticosXml(string token, List<SintomaWEB> listaSintomasWeb);

        //VALIDAR COM O SCHEMA

        [OperationContract]
        [WebInvoke(Method = "GET",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "validaXml?token={token}")]
        string validaXml(string token);
    }

    [DataContract]
    public class PacienteWEB
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string nome { get; set; }
        [DataMember]
        public int bi { get; set; }
        [DataMember]
        public DateTime dataNascimento { get; set; }
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
    public class SintomaWEB
    {
        [DataMember]
        public string nome { get; set; }
    }

    [DataContract]
    public class DiagnosticoWEB
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
        public List<SintomaWEB> listaSintomas { get; set; }
    }
}
