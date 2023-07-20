using System.Collections.Generic;

namespace Genesis.Integracoes.Integracao_DB.Model.Response.RsConsultaStatusAtendimento
{
    public class StatusAtendimentoResult
    {
        public string CodigoApoiado { get; set; }
        public string CodigoSenhaIntegracao { get; set; }
        public DadosStatusPedido DadosStatusPedido { get; set; }
        public List<DadosStatusProcedimento> DadosStatusProcedimento { get; set; }
    }
}
