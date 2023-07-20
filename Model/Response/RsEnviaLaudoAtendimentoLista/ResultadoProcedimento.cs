using System;
using System.Collections.Generic;

namespace Genesis.Integracoes.Integracao_DB.Model.Response.RsEnviaLaudoAtendimentoLista
{
    public class ResultadoProcedimento
    {
        public string CodigoExameDB { get; set; }
        public string VersaoLaudo { get; set; }
        public string DescricaoMetodoLogia { get; set; }
        public string DescricaoRegiaoColeta { get; set; }
        public List<ResultadoTexto> ListaResultadoTexto { get; set; }
        public List<ResultadoImagem> ListaResultadoImagem { get; set; }
        public DateTime DataHoraLiberacaoClinica { get; set; }
        public string NomeLiberadorClinico { get; set; }
        public string Obervacao1 { get; set; }
        public string Obervacao2 { get; set; }
        public string Obervacao3 { get; set; }
        public string Obervacao4 { get; set; }
        public string Obervacao5 { get; set; }
        public string Material { get; set; }
        public string IdentificacaoExameApoiado { get; set; }
        public string MaterialApoiado { get; set; }
        public string DescricaoMaterialApoiado { get; set; }
        public string DescricaoExameApoiado { get; set; }
    }
}
