using System;
using System.Collections.Generic;

namespace Genesis.Integracoes.Integracao_DB.Model.Response.RsRecebeAtendimento_EnviaAmostra
{
    public class StatusLote
    {
        public string NumeroLote { get; set; }
        public string ArquivoSolicitacaoPedidos { get; set; }
        public List<StatusLotePedido> Pedidos { get; set; }
        public DateTime DataHoraGravacao { get; set; }
    }
}
