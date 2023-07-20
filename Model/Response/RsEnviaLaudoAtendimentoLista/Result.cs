using System;
using System.Collections.Generic;

namespace Genesis.Integracoes.Integracao_DB.Model.Response.RsEnviaLaudoAtendimentoLista
{
    public class Result
    {
        public string NumeroAtendimentoApoiado { get; set; }
        public string NumeroAtendimentoDB { get; set; }
        public string RGPacienteApoiado { get; set; }
        public string RGPacienteDB { get; set; }
        public string NomePaciente { get; set; }
        public string Sexo { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public string NumeroCPF { get; set; }
        public DateTime dataNascimento { get; set; }
        public List<ResultadoProcedimento> ListaResultadoProcedimento { get; set; }
    }
}
