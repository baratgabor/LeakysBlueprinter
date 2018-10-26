namespace LeakysBlueprinter.Model
{
    /// <summary>
    /// Query that executes on a single grid
    /// </summary>
    public interface IGridQuery<TResult> : IQuery<TResult>
    {
        /// <summary>
        /// Entity Id of the grid to execute operation on
        /// </summary>
        string GridEntityId { get; set; }
    }
}