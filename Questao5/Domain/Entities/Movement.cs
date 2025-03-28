using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities;

public class Movement : Entity
{
    public override string Id
    {
        get => MovementId;
        set => MovementId = value;
    }
    public string MovementId { get; private set; }
    public string CurrentAccountId { get; private set; }
    public DateTime MovementDate { get; private set; }
    public EMovementType MovementType { get; private set; }
    public decimal Value { get; private set; }

    public Movement(string currentAccountId, EMovementType movementType, decimal value)
    {
        CurrentAccountId = currentAccountId;
        MovementDate = DateTime.Now;
        MovementType = movementType;
        Value = value;
    }

    public Movement() { }
}
