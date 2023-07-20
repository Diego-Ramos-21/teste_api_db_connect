using System.Collections.Generic;

namespace Genesis.Integracoes.Integracao_DB.Model.Response.RsRecebeAtendimento_EnviaAmostra
{
    public class RecebeAtendimentoResult
    {
        public List<StatusLote> StatusLote { get; set; }
        public Confirmacao Confirmacao { get; set; }
    }
}
