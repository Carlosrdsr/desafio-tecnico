using Questao5.Domain.Enumerators;
using Questao5.Domain.Exceptions;

namespace Questao5.Domain.Extensions;

public static class MovementTypeExtension
{
    private static readonly Dictionary<string, EMovementType> MovementTypeMap = new Dictionary<string, EMovementType>
    {
        { "C", EMovementType.CREDITO },
        { "D", EMovementType.DEBITO }
    };

    public static EMovementType ToMovementType(this string tipoString)
    {
        if (MovementTypeMap.TryGetValue(tipoString, out var tipo))
        {
            return tipo;
        }
        else
        {
            throw new BadRequestException($"Tipo de movimento é inválido", "INVALID_TYPE");
        }
    }
    public static string ToCode(this EMovementType tipo)
    {
        return tipo switch
        {
            EMovementType.CREDITO => "C",
            EMovementType.DEBITO => "D",
            _ => throw new ArgumentOutOfRangeException(nameof(tipo), tipo, null)
        };
    }
}
