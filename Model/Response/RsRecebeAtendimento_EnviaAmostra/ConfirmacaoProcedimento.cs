using System.Collections.Generic;

namespace Genesis.Integracoes.Integracao_DB.Model.Response.RsRecebeAtendimento_EnviaAmostra
{
    public class ConfirmacaoProcedimento
    {
        public string CodigoExameDB { get; set; }
        public string Status { get; set; }
        public List<ErroIntegracao> ErroPedido { get; set; }
    }
}
