namespace Genesis.Integracoes.Integracao_DB.Utils
{
    public class ConfigHandler
    {
        private static string _dbUrl;
    
        public static void SetDbUrl(string dbUrl) => _dbUrl = dbUrl;
    
        public static string GetDbUrl() => _dbUrl;
    }
}
