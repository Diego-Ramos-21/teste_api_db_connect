using System;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Integracoes.Integracao_DB.Model.Request.RqListaProcedimentosPendentes
{
    public class ListaProcedimentosPendentesREQ
    {
        [Required]
        public string CodigoApoiado { get; set; }
        [Required]
        public string CodigoSenhaIntegracao { get; set; }
        [Required]
        public DateTime DtInicial { get; set; }
        [Required]
        public DateTime DtFinal { get; set; }
    }
}
