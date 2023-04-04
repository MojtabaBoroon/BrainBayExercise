namespace BrainbayConsoleApp.Applications.Abstraction
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery command);
    }
}
