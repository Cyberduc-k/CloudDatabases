namespace Service.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string what) : base($"The {what} could not be found")
    {
    }
}
