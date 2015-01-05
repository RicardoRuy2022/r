using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DomainModel;

namespace AcupunturaXML
{
    public class XmlHandler
    {
        public static void writeToXmlFile(List<DomainModel.Sintoma> listaSintomas, List<DomainModel.Diagnostico> listaDiagnosticos)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", null, null);
                doc.AppendChild(declaration);

                XmlElement root = doc.CreateElement("Acupuntura");
                doc.AppendChild(root);
                int totalSintomas = 0;

                //Adicionar Sintomas:
                XmlElement sintomasFolha1 = doc.CreateElement("Sintomas");
                foreach (Sintoma s in listaSintomas)
                {
                    XmlElement sintomaFolha1 = doc.CreateElement("Sintoma");
                    sintomaFolha1.InnerText = s.getNome;
                    sintomasFolha1.AppendChild(sintomaFolha1);
                    totalSintomas++;
                }
                sintomasFolha1.SetAttribute("Total", totalSintomas.ToString());
                root.AppendChild(sintomasFolha1);

                //Adicionar Diagnósticos:
                XmlElement diagnosticos = doc.CreateElement("Diagnosticos");
                foreach (Diagnostico d in listaDiagnosticos)
                {
                    XmlElement diagnostico = doc.CreateElement("Diagnostico");
                    diagnostico.SetAttribute("Descricao", d.getDescricao);
                    XmlElement nome = doc.CreateElement("Nome");
                    nome.InnerText = d.getNome;
                    diagnostico.AppendChild(nome);
                    XmlElement orgao = doc.CreateElement("Orgao");
                    orgao.InnerText = d.getOrgao;
                    diagnostico.AppendChild(orgao);
                    XmlElement tratamento = doc.CreateElement("Tratamento");
                    tratamento.InnerText = d.getTratamento;
                    diagnostico.AppendChild(tratamento);
                    XmlElement sintomasFolha2 = doc.CreateElement("Sintomas");
                    totalSintomas = 0;
                    foreach (Sintoma s in d.getListaSintomas)
                    {
                        XmlElement sintomaFolha2 = doc.CreateElement("Sintoma");
                        sintomaFolha2.InnerText = s.getNome;
                        sintomasFolha2.AppendChild(sintomaFolha2);
                        totalSintomas++;
                    }
                    sintomasFolha2.SetAttribute("Total", totalSintomas.ToString());
                    diagnostico.AppendChild(sintomasFolha2);
                    diagnosticos.AppendChild(diagnostico);
                }
                root.AppendChild(diagnosticos);
                //Gravar no ficheiro XML
                doc.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<DomainModel.Sintoma> getListaSintomasXml()
        {
            List<DomainModel.Sintoma> listaSintomas = new List<Sintoma>();

            XmlDocument doc = new XmlDocument();
            doc.Load(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            foreach (XmlNode node in doc.SelectNodes("Acupuntura/Sintomas/Sintoma"))
            {
                DomainModel.Sintoma sintoma = new Sintoma(node.InnerText);
                listaSintomas.Add(sintoma);
            }

            return listaSintomas;
        }

        public static List<string> getListaDiagnosticosXml(List<Sintoma> sintomasSelecionados)
        {
            List<string> listaDiagnosticos = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.Load(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            foreach (XmlNode nodeDiagnostico in doc.SelectNodes("//Diagnostico"))
            {
                int count = 0;
                foreach (XmlNode nodeSintoma in nodeDiagnostico.LastChild.ChildNodes)
                {
                    foreach (Sintoma s in sintomasSelecionados)
                    {
                        if (nodeSintoma.InnerText.Equals(s.getNome))
                        {
                            count++;
                        }
                    }
                }
                if (count > 0)
                {
                    try
                    {

                        Decimal total = Convert.ToDecimal(nodeDiagnostico.LastChild.Attributes[0].Value);
                        Decimal percentagem = Math.Round((count / total) * 100);
                        listaDiagnosticos.Add(Convert.ToString(percentagem + "|" + nodeDiagnostico.Attributes[0].Value));
                    }
                    catch
                    {
                        throw new Exception("Impossivel calcular diagnosticos!\nO ficheiro Xml selecionado pode não ser válido.\nPor favor valide o ficheiro Xml de acordo com um Schema adequado.");
                    }
                }
            }
            return listaDiagnosticos;
        }
    }
}
