namespace Questao5.Domain.Exceptions
{
    public class ApiException
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? TipoError { get; set; }
    }
}
