using Genesis.Integracoes.Integracao_DB.Model.Response.RsRecebeAtendimento_EnviaAmostra;
using System.Collections.Generic;

namespace Genesis.Integracoes.Integracao_DB.Model.Response.RsEnviaAmostrasProcedimentosPendentes
{
    public class ListaAmostraEtiquetasPedido
    {
        public string NumeroAtendimentoApoiado { get; set; }
        public string NumeroAtendimentoDB { get; set; }
        public List<AmostraEtiqueta> AmostraEtiquetas { get; set; }
    }
}