using System;
using System.Collections.Generic;

namespace Genesis.Integracoes.Integracao_DB.Model.Response.RsListaProcedimentosPendentes
{
    public class PedidoMPP
    {
        public string NomePaciente { get; set; }
        public string NumeroAtendimentoApoiado { get; set; }
        public string NumeroAtendimentoDB { get; set; }
        public DateTime DataHoraPedido { get; set; }
        public List<ProcedimentoMPP> ListaProcedimentoMPP { get; set; }
    }
}
