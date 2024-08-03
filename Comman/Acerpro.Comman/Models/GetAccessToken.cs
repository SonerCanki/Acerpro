namespace Acerpro.Common.Model
{
    public class GetAccessToken
    {
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public long Expires { get; set; }
    }
}
