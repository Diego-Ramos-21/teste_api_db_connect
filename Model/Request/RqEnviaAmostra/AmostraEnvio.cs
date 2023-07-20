using System.ComponentModel.DataAnnotations;

namespace Genesis.Integracoes.Integracao_DB.Model.Request.RqEnviaAmostra
{
    public class AmostraEnvio
    {
        [Required]
        public string codigoApoiado { get; set; }
        [Required]
        public string codigoSenhaIntegracao { get; set; }
        [Required]
        public string numeroAtendimentoApoiado { get; set; }
    }
}
