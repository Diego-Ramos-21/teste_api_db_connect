using System.ComponentModel.DataAnnotations;

namespace Genesis.Integracoes.Integracao_DB.Model.Request.RqRecebeAtendimento
{
    public class AtendimentoDB
    {
        [Required]
        public string CodigoApoiado { get; set; }
        [Required]
        public string CodigoSenhaIntegracao { get; set; }
        [Required]
        public Pedido Pedido { get; set; }
    }
}
