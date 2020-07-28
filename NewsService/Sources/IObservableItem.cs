namespace NewsService.Sources
{
    /// <summary>
    /// Interface for Observable item
    /// </summary>
    public interface IObservableItem
    {
        void Attach(INewsSourceObserver observer);

        void Detach(INewsSourceObserver observer);

        void Notify();
    }
}