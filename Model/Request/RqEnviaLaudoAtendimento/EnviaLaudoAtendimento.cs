using System.Collections.Generic;

namespace Genesis.Integracoes.Integracao_DB.Model.Request.RqEnviaLaudoAtendimento
{
    public class EnviaLaudoAtendimento
    {
        public string CodigoApoiado { get; set; }
        public string CodigoSenhaIntegracao { get; set; }
        public List<string> NumeroAtendimentoApoiado { get; set; }
    }
}
