namespace RecipeBook.SharedKernel.Contracts
{
    public interface ILogWriter
    {
        void LogCritical(string message);
        void LogError(string message);
        void LogWarning(string message);
        void LogInformation(string message);
        void LogDebug(string message);
    }
}
