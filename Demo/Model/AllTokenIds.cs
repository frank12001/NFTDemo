using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Model
{
    public class AllTokenIds
    {
        public string ChainRPCUrl { get; set; }
        public int ChainId { get; set; }
        public string MyPrivateKey { get; set; }
        public string MyAddress { get; set; }
        public string ContractAddress { get; set; }
        public class Result
        {
            public List<int> Ids { get; set; }
        }
    }
}
