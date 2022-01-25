using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Model
{
    public class TransactionDTO
    {
        public string ChainRPCUrl { get; set; }
        public int ChainId { get; set; }
        public string ContractAddress { get; set; }
        public string SenderPrivateKey { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiveAddress { get; set; }
        public int TokenId { get; set; }

        public class Result
        {
            public string ChainOrderId { get; set; }
        }
    }
}
