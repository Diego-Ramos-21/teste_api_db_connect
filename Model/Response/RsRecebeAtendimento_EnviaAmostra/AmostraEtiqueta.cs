using System;

namespace Genesis.Integracoes.Integracao_DB.Model.Response.RsRecebeAtendimento_EnviaAmostra
{
    public class AmostraEtiqueta
    {
        public string NumeroAmostra { get; set; }
        public string Exames { get; set; }
        public string ContadorAmostra { get; set; }
        public string RGPacienteDB { get; set; }
        public string NomePaciente { get; set; }
        public string MeioColeta { get; set; }
        public string GrupoInterface { get; set; }
        public string Material { get; set; }
        public string RegiaoColeta { get; set; }
        public string Volume { get; set; }
        public string Prioridade { get; set; }
        public string TipoCodigoBarras { get; set; } // CODE128
        public string CodigoInstrumento { get; set; }
        public string Origem { get; set; }
        public bool FlagAmostraMae { get; set; }
        public string TextoAmostraMae { get; set; }
        public DateTime DataSistema { get; set; }
        public string EtiquetaAmostra { get; set; }
    }
}
