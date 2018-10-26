using LeakysBlueprinter.Model.Exceptions;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model
{
    internal abstract class GridCommandHandlerBase<T> : CommandHandlerBase<T>
        where T : IMyGridCommand
    {
        public GridCommandHandlerBase(IBlueprintDataContext dataContext) : base(dataContext)
        { }

        /// <summary>
        /// Implements abstract method instead of the concrete command to do some prep, and pushes execution of concrete commands onto another method
        /// </summary>
        protected override void DoHandle(T command)
        {
            if (command.GridEntityId == null)
                throw new AppException(ExceptionKind.GridOperationFailed_TargetGridNotSpecified);

            var targetGrid = _dataContext.GetGridByEntityId(command.GridEntityId);

            if (targetGrid == null)
                throw new AppException(ExceptionKind.Blueprint_GridNotFound);

            DoHandleOnGrid(command, targetGrid);
        }

        /// <summary>
        /// Command execution to be implemented in concrete commands, with target grid supplied.
        /// </summary>
        protected abstract void DoHandleOnGrid(T command, XElement grid);
    }
}
