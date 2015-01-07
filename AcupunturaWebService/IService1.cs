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

        //ADICIONAR TERAPEUTA:

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "adicionarTerapeuta?token={token}")]
        Boolean adicionarTerapeuta(string token, string nome, int bi, DateTime dataNascimento, string username, string password);
        
        //ADICIONAR ADMINISTRADOR:

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "adicionarAdministrador?token={token}")]
        Boolean adicionarAdministrador(string token, string username, string password);
       
        //PESQUISAR PACIENTE POR BI :

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "getPacientePorBi?token={token}&bi={bi}")]
        PacienteWEB getPacientePorBi(string token, int bi, int idTerapeuta);
     
        
        //PESQUISAR ADMINISTRADOR POR USERNAME :

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "getAdministradorUsername?token={token}&username={username}")]
        UtilizadorWEB getAdministradorUsername(string token, string username);

        //PESQUISAR TERAPEUTA POR BI :

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "getTerapeutaPorBi?token={token}&bi={bi}")]
        TerapeutaWEB getTerapeutaPorBi(string token, int bi);

        //REMOVER PACIENTE

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "removerPaciente?token={token}&bi={bi}")]
        Boolean removerPaciente(string token, int bi, int idTerapeuta);
     
        //REMOVER ADMINISTRADOR

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "removerAdministrador?token={token}&username={username}")]
        Boolean removerAdministrador(string token, string username);

        //REMOVER TERAPEUTA

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "removerTerapeuta?token={token}&bi={bi}")]
        Boolean removerTerapeuta(string token, int bi);
       
        //EDITAR PACIENTE
        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "editarPaciente?token={token}")]
        Boolean editarPaciente(string token, int idTerapeuta, string nome, int bi, DateTime dataNascimento);
        
        //EDITAR TERAPEUTA
        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "editarTerapeuta?token={token}")]
        Boolean editarTerapeuta(string token, string nome, int bi, DateTime dataNascimento, string username, string password);

        //EDITAR ADMINISTRADOR
        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "editarAdministrador?token={token}")]
        Boolean editarAdministrador(string token, string username, string password);
       
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
        
        //GET BI DO TERAPEUTA

        [OperationContract]
        [WebInvoke(Method = "GET",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "getTerapeutaID?token={token}")]
        TerapeutaWEB getTerapeutaID(string token);
           
        //GET ID UTILIZADOR DO TERAPEUTA

        [OperationContract]
        [WebInvoke(Method = "GET",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "getUtilizadorIdTerapeura?token={token}&idTerapeuta={idTerapeuta}")]
        UtilizadorWEB getUtilizadorIdTerapeura(string token, int idTerapeuta);

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
    public class TerapeutaWEB
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
