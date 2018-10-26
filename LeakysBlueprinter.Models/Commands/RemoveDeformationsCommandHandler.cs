using LeakysBlueprinter.Model.Exceptions;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model.Commands
{
    /// <summary>
    /// Removes the "Skeleton" node from the given grid, which equals removing all deformations of the grid.
    /// This doesn't affect grid block damages - i.e. blocks will still be damaged, just without being deformed.
    /// </summary>
    internal class RemoveDeformationsCommandHandler : GridCommandHandlerBase<RemoveDeformationsCommand>
    {
        protected const string _targetNodeName = "Skeleton";

        public RemoveDeformationsCommandHandler(IBlueprintDataContext dataContext) : base(dataContext)
        { }

        /// <summary>
        /// Execute actual command
        /// </summary>
        protected override void DoHandleOnGrid(RemoveDeformationsCommand command, XElement grid)
        {
            var targetNode = grid.Element(_targetNodeName);

            if (targetNode == null)
                throw new AppException(ExceptionKind.Blueprint_NoDeformationToDelete);

            targetNode.Remove();
        }
    }
}
