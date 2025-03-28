using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Exceptions;
using Serilog;

namespace Questao5.Controllers
{
    [Route("api/bank-accounts")]
    [OpenApiTag("Bank Accounts")]
    [ApiController]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
    public class BankAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BankAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cadastra uma transação de crédito ou débito
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("transaction")]
        public async Task<ActionResult> Post([FromBody] TransactionCommand command)
        {
            var result = await _mediator.Send(command);

            Log.Information("Transação finalizada com sucesso!");

            return Ok(result);
        }

        /// <summary>
        /// Consulta o saldo da conta corrente
        /// </summary>
        /// <param name="currentAcountId">Id da conta corrente</param>        
        /// <returns></returns>
        [HttpGet("balance")]
        public async Task<ActionResult> Get([FromQuery] string currentAcountId)
        {
            var query = new AccountAmountQuery(currentAcountId);
            var result = await _mediator.Send(query);

            Log.Information("Consulta de saldo realizado com sucesso!");
            return Ok(result);
        }

    }
}
