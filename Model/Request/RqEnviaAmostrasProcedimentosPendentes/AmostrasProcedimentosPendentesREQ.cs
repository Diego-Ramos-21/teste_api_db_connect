using Genesis.Integracoes.Integracao_DB.Model.Response.RsListaProcedimentosPendentes;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Integracoes.Integracao_DB.Model.Request.RqEnviaAmostrasProcedimentosPendentes
{
    public class AmostrasProcedimentosPendentesREQ
    {
        [Required]
        public string CodigoApoiado { get; set; }
        [Required]
        public string CodigoSenhaIntegracao { get; set; }
        [Required]
        public PedidoMPP ListaPedidoMPP { get; set; }
    }
}
