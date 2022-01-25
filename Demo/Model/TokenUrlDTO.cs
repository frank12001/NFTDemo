namespace Demo.Model
{
    public class TokenUrlDTO
    {
        public string ChainRPCUrl { get; set; }
        public int ChainId { get; set; }
        public string MyPrivateKey { get; set; }
        public string ContractAddress { get; set; }
        public int TokenId { get; set; }
        public class Result
        {
            public string Url { get; set; }
        }
    }
}
