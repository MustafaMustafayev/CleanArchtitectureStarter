using Application.Localization;

namespace Application.Responses;

public record SuccessResult : Result
{
    public SuccessResult(string message)
        : base(true, message)
    {
    }

    public SuccessResult()
        : base(true, EMessages.Success.Translate())
    {
    }
}