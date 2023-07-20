using System.ComponentModel.DataAnnotations;

namespace Genesis.Integracoes.Integracao_DB.Model.Request.RqRecebeAtendimento
{
    public class Solicitante
    {
        [Required]
        public string CodigoConselho { get; set; }
        [Required]
        public string CodigoConselhoSolicitante { get; set; }
        [Required]
        public string CodigoUFConselhoSolicitante { get; set; }
        [Required]
        public string NomeSolicitante { get; set; }
    }
}
