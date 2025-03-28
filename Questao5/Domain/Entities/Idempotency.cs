namespace Questao5.Domain.Entities;

public class Idempotency
{
    public Guid IdempotencyKey { get; private set; }
    public string Request { get; private set; }
    public string Result { get; private set; }

    public Idempotency(Guid idempotencyKey, string request, string result)
    {
        Request = request;
        Result = result;
        IdempotencyKey = idempotencyKey;
    }
    public Idempotency() { }
}
