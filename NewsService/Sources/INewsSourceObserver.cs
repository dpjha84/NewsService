namespace NewsService.Sources
{
    /// <summary>
    /// Interface for every observer once notified by observable item
    /// </summary>
    public interface INewsSourceObserver
    {
        /// <summary>
        /// Refresh as notification is received
        /// </summary>
        void Refresh();
    }
}