using System.ComponentModel.DataAnnotations;

namespace Genesis.Integracoes.Integracao_DB.Model.Request.RqEnviaBase64
{
    public class EnviaResultadoBase64REQ
    {
        [Required]
        public string CodigoApoiado { get; set; }
        [Required]
        public string CodigoSenhaIntegracao { get; set; }
        public string TipoCabecalho { get; set; }
        [Required]
        public string NumeroAtendimentoApoiado { get; set; }
        [Required]
        public string[] CodigoExameDB { get; set; }
    }
}
