namespace Questao5.Application.Dto;

public class TransactionDto
{
    public string TransactionID { get; private set; }

    public TransactionDto(string id) =>
        TransactionID = id;
}
