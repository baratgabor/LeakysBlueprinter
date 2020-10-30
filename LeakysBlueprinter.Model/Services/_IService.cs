namespace LeakysBlueprinter.Model
{
    /// <summary>
    /// Service contract stipulating the implementation of query and command handler methods
    /// </summary>
    interface IOperationService
    {
        /// <summary>
        /// Checks if the specified query can be executed.
        /// </summary>
        /// <param name="query">The query to check</param>
        /// <returns>Returns true if query can be executed.</returns>
        bool CanExecute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;

        /// <summary>
        /// Checks if the specified command can be executed.
        /// </summary>
        /// <param name="command">The command to check</param>
        /// <returns>Returns true if command can be executed.</returns>
        bool CanExecute<TCommand>(TCommand command) where TCommand : IMyCommand;



        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query instance to execute.</param>
        /// <returns>The result of the query.</returns>
        TResult Execute<TResult>(IQuery<TResult> query);

        /// <summary>
        /// Executes the specified command
        /// </summary>
        /// <param name="command">The command instance to execute.</param>
        void Execute<TCommand>(TCommand command) where TCommand : IMyCommand;
    }
}
