namespace SentimentAnalyser.Data.Interfaces
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork CreateUnitOfWork();
    }
}