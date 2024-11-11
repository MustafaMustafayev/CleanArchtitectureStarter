namespace Application.Responses;

public interface IDataResult<out T> : IResult
{
    T? Data { get; }
}