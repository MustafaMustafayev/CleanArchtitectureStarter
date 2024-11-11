using Application.Localization;

namespace Application.Responses;

public record SuccessDataResult<T> : DataResult<T>
{
    public SuccessDataResult(T data, string message)
        : base(data, true, message)
    {
    }

    public SuccessDataResult(T data)
        : base(data, true, EMessages.Success.Translate())
    {
    }

    public SuccessDataResult(string message)
        : base(default, true, message)
    {
    }

    public SuccessDataResult()
        : base(default, true, EMessages.Success.Translate())
    {
    }
}