namespace Questao5.Domain.Exceptions;

public class BadRequestException : Exception
{
    public string Mensagem { get; private set; }
    public string Tipo { get; private set; }
    public BadRequestException(string error) : base(error)
    { }

    public BadRequestException(string mensagem, string tipo)
    {
        Mensagem = mensagem;
        Tipo = tipo;
    }
}
