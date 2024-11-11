using Application.Localization;

namespace Application.Responses;

public record ErrorDataResult<T> : DataResult<T>
{
    public ErrorDataResult(T data, string message)
        : base(data, false, message)
    {
    }

    public ErrorDataResult(T data)
        : base(data, false, EMessages.GeneralError.Translate())
    {
    }

    public ErrorDataResult(string message)
        : base(default, false, message)
    {
    }

    public ErrorDataResult()
        : base(default, false, EMessages.GeneralError.Translate())
    {
    }
}