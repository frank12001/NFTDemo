using System;

namespace Demo.Model
{
    public class DeployDTO
    {
        public string ChainRPCUrl { get; set; }
        public int ChainId { get; set; }
        public string MyPrivateKey { get; set; }

        public class Result
        {
            public string ChainAddress { get; set; }
        }
    }
}
