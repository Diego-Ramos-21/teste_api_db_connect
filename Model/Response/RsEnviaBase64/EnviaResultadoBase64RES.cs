namespace Genesis.Integracoes.Integracao_DB.Model.Response.RsEnviaBase64
{
    public class EnviaResultadoBase64RES
    {
        public byte[] LaudoPDF { get; set; }
        public string LinkLaudo { get; set; }
        public string Mensagem { get; set; }
        public int Status { get; set; }
    }
}
