using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Test.Helper;

namespace Questao5.Test.Tests
{
    public class IntegrationTest1
    {
        [Fact]
        public async Task InserirTransacaoComSucesso()
        {
            //Arrange
            Guid requestId = Guid.NewGuid();
            Guid currentAccountId = Guid.NewGuid();
            decimal value = 5;
            string type = "D";

        //Act
        var tarefa = new Application.Commands.Requests.TransactionCommand() { 
            RequestId = requestId, 
            CurrentAccountId = currentAccountId, 
            Value = value, 
            Type = type };

            //Assert
            Assert.Equal(tarefa.RequestId, requestId);
            Assert.Equal(tarefa.CurrentAccountId, currentAccountId);
            Assert.Equal(tarefa.Value, value);
            Assert.Equal(tarefa.Type, type);
        }

        [Fact]
        public async Task ConsultarContaComSucesso()
        {
            //Arrange            
            var currentAccountId = Guid.NewGuid().ToString();

            //Act
            var tarefa = new Application.Queries.Requests.AccountAmountQuery(currentAccountId);

            //Assert
            Assert.Equal(tarefa.CurrentAccountId, currentAccountId);
        }

        [Fact]
        public async Task ConsultarContaComFalha()
        {
            // Arrange           
            string id = "99-382D323D-7067-ED11";
            var repository = new CurrentAccountRepositoryTest();

            // Act            
            var tarefa = repository.GetAccountByIdAsync(id);

            //Assert
            Assert.Null(tarefa.Result);
        }

        [Fact]
        public async Task ConsultarContaComSucesso2()
        {
            // Arrange           
            string id = Guid.NewGuid().ToString();
            var repository = new CurrentAccountRepositoryTest();

            // Act            
            var tarefa = repository.GetAccountByIdAsync(id);

            //Assert
            Assert.NotNull(tarefa.Result);
        }

        [Fact]
        public async Task InserirTransacaoComFalha()
        {
            // Arrange           
            var model = new Movement();
            var repository = new MovementRepositoryTest();

            // Act            
            var tarefa = repository.AddAsync(model);

            //Assert
            Assert.Null(tarefa);
        }

        [Fact]
        public async Task ConsultaValorDebito()
        {
            // Arrange           
            var repository = new MovementRepositoryTest();

            // Act            
            var tarefa = repository.GetSumByTypeAsync(Guid.NewGuid().ToString(), EMovementType.DEBITO);

            //Assert
            Assert.NotNull(tarefa);
        }

        [Fact]
        public async Task ConsultaValorCredito()
        {
            // Arrange           
            var repository = new MovementRepositoryTest();

            // Act            
            var tarefa = repository.GetSumByTypeAsync(Guid.NewGuid().ToString(), EMovementType.CREDITO);

            //Assert
            Assert.NotNull(tarefa);
        }

    }
}
