using LeakysBlueprinter.Model.Exceptions;

namespace LeakysBlueprinter.Model
{
    internal abstract class CommandHandlerBase<T> : IMyCommandHandler<T>
        where T : IMyCommand
    {
        protected IBlueprintDataContext _dataContext;

        // TODO: Implement support for cross-cutting concerns, e.g. logging

        public CommandHandlerBase(IBlueprintDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        public void Handle(T command)
        {
            ValidateDataContext();
            DoHandle(command);
        }

        /// <summary>
        /// Command execution to be implemented in concrete commands.
        /// </summary>
        protected abstract void DoHandle(T command);

        // TODO: Consider pushing validity check to context class itself; it could subscribe to change events on root XElement, re-check structure, and throw if structure is invalid
        /// <summary>
        /// Check if context is still well-formatted for command execution.
        /// Mainly because context is raw XElement structure.
        /// </summary>
        protected void ValidateDataContext()
        {
            if (!_dataContext.IsValid())
                throw new AppException(ExceptionKind.Blueprint_StructureInvalid);
        }
    }
}
