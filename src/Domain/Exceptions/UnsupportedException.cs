namespace Domain.Exceptions;

public class UnsupportedException : Exception
{
    public UnsupportedException(string code)
        : base($"Colour \"{code}\" is unsupported.")
    {
    }
}
