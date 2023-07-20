using System.ComponentModel.DataAnnotations;

namespace Genesis.Integracoes.Integracao_DB.Model.Request.RqConsultaStatusAtendimento
{
    public class StatusAtendimento
    {
        [Required]
        public string CodigoApoiado { get; set; }
        [Required]
        public string CodigoSenhaIntegracao { get; set; }
        [Required]
        public string NumeroAtendimentoApoiado { get; set; }
        public string Procedimento { get; set; }
    }
}
