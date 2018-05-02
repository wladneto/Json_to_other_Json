using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;



namespace Json_to_other_Json
{
    class Program
    {
        //Classe com estrutura original dos Itens em Json
        public class JsonOriginal
        {
            public string gentilico { get; set; } 
            public string nome_pais { get; set; }
            public string nome_pais_int { get; set; }
            public string sigla { get; set; }
        }
        //Classe com estrutura final dos itens (onde quero chegar)
        public class JsonFinal
        {
            public string canonicalForm { get; set; }
            public List<string> list { get; set; }
        }

        static void Main(string[] args)
        {
            var jsonText = File.ReadAllText("C:\\paises-gentilicos-google-maps.json"); //Caminho do Json Origial
            var paises = JsonConvert.DeserializeObject<IList<JsonOriginal>>(jsonText);

            var JsonFinalmente = new List<JsonFinal>();
            foreach (var item in paises) // Nesse laço leio cada item do Json Original e vou criando itens no Json Final como eu desejo (regra de negocio)
            {
                var JsonFinalItem = new JsonFinal(); 
                JsonFinalItem.canonicalForm = item.nome_pais;
                var lista = new List<string>();

                lista.Add(item.nome_pais);
                lista.Add(item.nome_pais_int);
                JsonFinalItem.list = lista;

                JsonFinalmente.Add(JsonFinalItem); 

            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
           
            Console.Write(serializer.Serialize(JsonFinalmente)); //Imprimir o Json Gerado em tela
            Console.ReadKey(); // Aguardar em tela

            FileStream fs = new FileStream(@"C:\JsonFinal.json", FileMode.Append); //Gerar Arquivo do Json em Tela
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(serializer.Serialize(JsonFinalmente));
            sw.Flush();
            sw.Close();
            fs.Close();

        }
    }

}
