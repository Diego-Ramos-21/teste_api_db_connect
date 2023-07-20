using System.Collections.Generic;

namespace Genesis.Integracoes.Integracao_DB.Model.Response.RsRecebeAtendimento_EnviaAmostra
{
    public class ErroIntegracao
    {
        private static Dictionary<int, string> ErrorCodes = new Dictionary<int, string>()
        {
            {1, "Pedido já enviado"},
            {2, "Pedido inválido"},
            {3, "Paciente inválido"},
            {4, "Solicitante inválido"},
            {5, "Procedimento inválido"},
            {6, "Pedido não encontrado"},
            {7, "Pedido x Atendimento incosistente"},
            {999, "Erro inesperado"}
        };

        public string Codigo { get; set; } 
        public string Descricao { get; set; }
    }
}
