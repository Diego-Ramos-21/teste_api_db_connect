using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Integracoes.Integracao_DB.Model.Request.RqRecebeAtendimento
{
    public class Procedimento
    {
        [Required]
        public string CodigoExameDB { get; set; }
        public string DescricaoRegiaoColeta { get; set; }
        public string VolumeUrinario { get; set; }
        public string IdentificacaoExameApoiado { get; set; }
        public string MaterialApoiado { get; set; }
        public string DescricaoMaterialApoiado { get; set; }
        public string DescricaoExameApoiado { get; set; }
        public string CodigoMPP { get; set; }
        [Required]
        public List<AmostraColeta> Amostras { get; set; }
    }
}
