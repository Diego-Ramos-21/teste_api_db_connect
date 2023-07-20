using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Integracoes.Integracao_DB.Model.Request.RqRecebeAtendimento
{
    public class Pedido
    {
        public double Altura { get; set; }
        public string CodigoPrioridade { get; set; }
        public DateTime DataHoraDum { get; set; }
        public string DescricaoDadosClinicos { get; set; }
        public string DescricaoMedicamentos { get; set; }
        [Required]
        public Paciente ListaPacienteApoiado { get; set; }
        [Required]
        public List<Procedimento> ListaProcedimento { get; set; }
        public List<Solicitante> ListaSolicitante { get; set; }
        [Required]
        public string NumeroAtendimentoApoiado { get; set; }
        public string NumeroAtendimentoDBReserva { get; set; }
        public double Peso { get; set; }
        public string PostoColeta { get; set; }
        public string UsoApoiado { get; set; }
        public string CodigoMPP { get; set; }
        public List<Questionario> ListaQuestionarios { get; set; }
    }
}
