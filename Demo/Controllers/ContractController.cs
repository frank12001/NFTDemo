using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using TestERC721OnlyCS.Contracts.Character;
using TestERC721OnlyCS.Contracts.Character.ContractDefinition;
using Demo.Model;
using System.Collections.Generic;

namespace Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractController : ControllerBase
    {
        private readonly ILogger<ContractController> Logger;
        public ContractController(ILogger<ContractController> logger) 
        {
            Logger = logger;
        }
        [HttpPost("Deploy")]
        public async Task<IActionResult> Deploy([FromBody] DeployDTO packet)
        {
            var url = packet.ChainRPCUrl;
            var chainId = packet.ChainId;

            var privateKey = packet.MyPrivateKey;

            var account = new Account(privateKey, chainId);
            var web3 = new Web3(account, url);

            Logger.LogInformation("Deploying...");

            var deployment = new CharacterDeployment();
            var receipt = await CharacterService.DeployContractAndWaitForReceiptAsync(web3, deployment);
            var service = new CharacterService(web3, receipt.ContractAddress);

            Logger.LogInformation($"Contract Deployment Tx Status: {receipt.Status.Value}");
            Logger.LogInformation($"Contract Address: {service.ContractHandler.ContractAddress}");
            Logger.LogInformation("Finish");

            return new JsonResult(new DeployDTO.Result() { ChainAddress = service.ContractHandler.ContractAddress });
        }

        [HttpPost("CreateNFT")]
        public async Task<IActionResult> CreateNFT([FromBody] CreateNFTDTO packet)
        {
            //創建合約連線
            var account = new Account(packet.MyPrivateKey, packet.ChainId);
            var web3 = new Web3(account, packet.ChainRPCUrl);
            var service = new CharacterService(web3, packet.ContractAddress);

            //創建 NFT (需使用 Owner 帳號)
            var currentStoredValue = await service.SafeMintRequestAsync(packet.MyAddress, packet.TokenUrl);
            Logger.LogInformation($"Contract has value stored: {currentStoredValue}");

            return new JsonResult(new CreateNFTDTO.Result() { CurrentStoredValue = currentStoredValue });
        }

        [HttpPost("AllTokenIds")]
        public async Task<IActionResult> AllTokenIds([FromBody] AllTokenIds packet)
        {
            //創建合約連線
            var account = new Account(packet.MyPrivateKey, packet.ChainId);
            var web3 = new Web3(account, packet.ChainRPCUrl);
            var service = new CharacterService(web3, packet.ContractAddress);

            //取得特定人員，所有的 TokenId
            var balance = await service.BalanceOfQueryAsync(packet.MyAddress);
            Logger.LogInformation($"My Balance: {balance}");
            List<int> myTokens = new();
            for (int i = 0; i < balance; i++)
            {
                myTokens.Add((int)await service.TokenOfOwnerByIndexQueryAsync(packet.MyAddress, i));
            }
            myTokens.Sort();
            Logger.LogInformation($"My Tokens: {System.Text.Json.JsonSerializer.Serialize(myTokens)}");

            return new JsonResult(new AllTokenIds.Result() { Ids = myTokens });
        }

        [HttpPost("ChangeBaseUrl")]
        public async Task<IActionResult> ChangeBaseUrl([FromBody] ChangeBaseUrlDTO packet)
        {
            //創建合約連線
            var account = new Account(packet.MyPrivateKey, packet.ChainId);
            var web3 = new Web3(account, packet.ChainRPCUrl);
            var service = new CharacterService(web3, packet.ContractAddress);

            var changeBaseUrl = await service.ChangeBaseUrlRequestAsync(packet.NewBaseUrl);
            Logger.LogInformation($"Change Base Url: {changeBaseUrl}");

            return new JsonResult(new ChangeBaseUrlDTO.Result() { ChainOrderId = changeBaseUrl });
        }

        [HttpPost("TokenUrl")]
        public async Task<IActionResult> TokenUrl([FromBody] TokenUrlDTO packet)
        {
            //創建合約連線
            var account = new Account(packet.MyPrivateKey, packet.ChainId);
            var web3 = new Web3(account, packet.ChainRPCUrl);
            var service = new CharacterService(web3, packet.ContractAddress);

            var tokenUrl = await service.TokenURIQueryAsync(0);
            Logger.LogInformation($"TokenUrl: {tokenUrl}");

            return new JsonResult(new TokenUrlDTO.Result() {  Url = tokenUrl });
        }

        [HttpPost("Transaction")]
        public async Task<IActionResult> Transaction([FromBody] TransactionDTO packet)
        {
            //創建合約連線
            var account = new Account(packet.SenderPrivateKey, packet.ChainId);
            var web3 = new Web3(account, packet.ChainRPCUrl);
            var service = new CharacterService(web3, packet.ContractAddress);

            var transaction = await service.SafeTransferFromRequestAsync(packet.SenderAddress, packet.ReceiveAddress, packet.TokenId);
            Logger.LogInformation($"Transaction: {transaction}");

            return new JsonResult(new TransactionDTO.Result() { ChainOrderId = transaction });
        }
    }
}
