namespace LeakysBlueprinter.Model
{
    /// <summary>
    /// Command that executes operation on a single grid
    /// </summary>
    public interface IMyGridCommand : IMyCommand
    {
        /// <summary>
        /// Entity Id of the grid to execute operation on
        /// </summary>
        string GridEntityId { get; set; }
    }
}
