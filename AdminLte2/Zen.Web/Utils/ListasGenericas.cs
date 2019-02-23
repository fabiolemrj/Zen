using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zen.Web.Utils
{
    public static class ListasGenericas
    {
        public static List<object> ObterSimNao = new List<object>
        {
            new {Sigla = "", Nome = "Nenhum"},
            new {Sigla = "S", Nome = "Sim"},
            new {Sigla = "N", Nome = "Não"}
        };

        public static List<object> ObterSitAprov = new List<object>
        {
            new {Sigla = "T", Nome = "Todos"},
            new {Sigla = "A", Nome = "Aprovados"},
            new {Sigla = "N", Nome = "Não Aprovados"}
        };

        public static List<object> Vazia = new List<object>
        {
            new {Sigla = "", Nome = ""}
            
        };

        public static List<object> TipoLam = new List<object>
        {
            new {Sigla = "C", Nome = "Comum"},
            new {Sigla = "E", Nome = "Especial"},
            new {Sigla = "M", Nome = "Meio Corte"}
        };

        public static List<object> ListaPeriodicidade = new List<object>
        {
          //  new {Sigla = "", Nome = "Nenhum"},
            new {Sigla = "M", Nome = "Mensal"},
            new {Sigla = "B", Nome = "Bimestral"},
            new {Sigla = "T", Nome = "Trimestral"},
            new {Sigla = "Q", Nome = "Quadrimestral"},
            new {Sigla = "S", Nome = "Semestral"},
            new {Sigla = "A", Nome = "Anual"},
        };


        public static List<object> SituacaoAtivoSusp = new List<object>
        {
            new {Sigla = "A", Nome = "Ativo"},
            new {Sigla = "S", Nome = "Suspenso"}
            
        };

        public static List<object> ObterAtivo = new List<object>
        {
            new {Sigla = "", Nome = "Nenhum"},
            new {Sigla = "A", Nome = "Sim"},
            new {Sigla = "D", Nome = "Inativo"}
        };

        public static List<object> ObterClassif = new List<object>
        {
            new {Sigla = "M", Nome = "Manual"},
            new {Sigla = "S", Nome = "Semi-automatica"}
        };

        public static List<object> ObterEstados = new List<object>
        {
                new {Sigla = "AC", Nome = "Acre" },
                new {Sigla = "AL", Nome = "Alagoas" },
                new {Sigla = "AP", Nome = "Amapa" },
                new {Sigla = "AM", Nome = "Amazonas" },
                new {Sigla = "BA", Nome = "Bahia" },
                new {Sigla = "CE", Nome = "Ceara" },
                new {Sigla = "DF", Nome = "Distrito Federal" },
                new {Sigla = "ES", Nome = "Espirito Santo" },
                new {Sigla = "GO", Nome = "Goias" },
                new {Sigla = "MA", Nome = "Maranhao" },
                new {Sigla = "MT", Nome = "Mato Grosso" },
                new {Sigla = "MS", Nome = "Mato Grosso do Sul" },
                new {Sigla = "MG", Nome = "Minas Gerais" },
                new {Sigla = "PA", Nome = "Para" },
                new {Sigla = "PB", Nome = "Paraiba" },
                new {Sigla = "PR", Nome = "Parana" },
                new {Sigla = "PE", Nome = "Pernambuco" },
                new {Sigla = "PI", Nome = "Piaui" },
                new {Sigla = "RJ", Nome = "Rio de Janeiro" },
                new {Sigla = "RN", Nome = "Rio Grande do Norte" },
                new {Sigla = "RS", Nome = "Rio Grande do Sul" },
                new {Sigla = "RO", Nome = "Rondonia" },
                new {Sigla = "RR", Nome = "Roraima" },
                new {Sigla = "SC", Nome = "Santa Catarina" },
                new {Sigla = "SP", Nome = "Sao Paulo" },
                new {Sigla = "SE", Nome = "Sergipe" },
                new {Sigla = "TO", Nome = "Tocantins" }
         };
    }
}