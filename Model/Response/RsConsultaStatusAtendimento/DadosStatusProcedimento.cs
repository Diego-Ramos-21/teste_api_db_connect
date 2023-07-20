using System;

namespace Genesis.Integracoes.Integracao_DB.Model.Response.RsConsultaStatusAtendimento
{
    public class DadosStatusProcedimento
    {
        public string CodigoExameDB { get; set; }
        public string IdentificacaoExameApoiado { get; set; }
        public Nullable<DateTime> DataHoraRecepcaoOrigem { get; set; }
        public Nullable<DateTime> DataHoraCheckout { get; set; }
        public Nullable<DateTime> DataHoraRecepcaoUPF { get; set; }
        public Nullable<DateTime> DataHoraLiberacaoTecnica { get; set; }
        public Nullable<DateTime> DataHoraLiberacaoClinica { get; set; }
        public Nullable<DateTime> DataHoraDivulgacao { get; set; }
        public Nullable<DateTime> DataHoraImpressao { get; set; }
        public string StatusProducao { get; set; }
        public string TipoMPP { get; set; }
    }
}
