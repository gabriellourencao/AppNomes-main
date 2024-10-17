using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppNomesBr.Domain.DataTransferObject.ExternalIntegrations.IBGE.Censos
{

        public class Mesorregiao
        {
            public int id { get; set; }
            public string nome { get; set; }
            public UF UF { get; set; }
        }

        public class Microrregiao
        {
            public int id { get; set; }
            public string nome { get; set; }
            public Mesorregiao mesorregiao { get; set; }
        }

        public class Regiao
        {
            public int id { get; set; }
            public string sigla { get; set; }
            public string nome { get; set; }
        }

        public class RegiaoImediata
        {
            public int id { get; set; }
            public string nome { get; set; }

            [JsonPropertyName("regiao-intermediaria")]
            public RegiaoIntermediaria regiaointermediaria { get; set; }
        }

        public class RegiaoIntermediaria
        {
            public int id { get; set; }
            public string nome { get; set; }
            public UF UF { get; set; }
        }

        public class LocalidadeRoot
        {
            public int id { get; set; }
            public string nome { get; set; }
            public Microrregiao microrregiao { get; set; }

            [JsonPropertyName("regiao-imediata")]
            public RegiaoImediata regiaoimediata { get; set; }
        }

        public class UF
        {
            public int id { get; set; }
            public string sigla { get; set; }
            public string nome { get; set; }
            public Regiao regiao { get; set; }
        }
    }

