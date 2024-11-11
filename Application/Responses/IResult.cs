namespace Application.Responses;

public interface IResult
{
    bool Success { get; }

    string? Message { get; }
}