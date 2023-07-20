using System.Collections.Generic;

namespace Genesis.Integracoes.Integracao_DB.Model.Response.RsRecebeAtendimento_EnviaAmostra
{
    public class ConfirmacaoPedido
    {
        public string NumeroAtendimentoApoiado { get; set; }
        public string Status { get; set; }
        public List<ErroIntegracao> ErroIntegracao { get; set; }
        public string NumeroAtendimentoDB { get; set; }
        public List<AmostraEtiqueta> Amostras { get; set; }
        public List<ConfirmacaoProcedimento> Procedimento { get; set; }
    }
}
