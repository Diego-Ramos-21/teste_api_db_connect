using System.Collections.Generic;

namespace Genesis.Integracoes.Integracao_DB.Model.Response.RsRecebeAtendimento_EnviaAmostra
{
    public class StatusLotePedido
    {
        public string NomePaciente { get; set; }
        public string NumeroAtendimentoDB { get; set; } 
        public string NumeroAtendimentoApoiado { get; set; }
        public string PostoColeta { get; set; }
        public List<StatusLoteProcedimento> Procedimentos { get; set; }
        public List<ErroIntegracao> ErroPedido { get; set; }
        public List<ConfirmacaoProcedimento> ErroProcedimento { get; set; }
    }
}