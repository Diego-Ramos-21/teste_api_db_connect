using System;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Integracoes.Integracao_DB.Model.Request.RqRecebeAtendimento
{
    public class Paciente
    {
        [Required]
        public DateTime? DataHoraPaciente { get; set; }
        [Required]
        public string NomePaciente { get; set; }
        public string NumeroCartaoNacionalSaude { get; set; }
        public string NumeroCPF { get; set; }
        public string RGPacienteApoiado { get; set; }
        [Required]
        public string SexoPaciente { get; set; } // M ou F
    }
}
