using LeakysBlueprinter.Model.Exceptions;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model
{
    // TODO: Get rid of this 'grid-specific' base class... this is pure bloat.
    internal abstract class GridQueryHandlerBase<TQuery, TResult> : QueryHandlerBase<TQuery, TResult>
        where TQuery : IGridQuery<TResult>
    {
        // TODO: Implement support for cross-cutting concerns, e.g. logging

        public GridQueryHandlerBase(IDefinitionsRepository definitions, IBlueprintDataContext dataContext) : base(definitions, dataContext)
        { }

        /// <summary>
        /// Implements abstract method instead of the concrete query to do some prep, and pushes execution of concrete queries onto another method
        /// </summary>
        protected override TResult DoHandle(TQuery query)
        {
            if (query.GridEntityId == null)
                throw new AppException(ExceptionKind.GridOperationFailed_TargetGridNotSpecified);

            var targetGrid = _dataContext.GetGridByEntityId(query.GridEntityId);

            if (targetGrid == null)
                throw new AppException(ExceptionKind.Blueprint_GridNotFound);

            return DoHandleOnGrid(query, targetGrid);
        }

        /// <summary>
        /// Query execution to be implemented in concrete queries, with target grid supplied.
        /// </summary>
        protected abstract TResult DoHandleOnGrid(TQuery query, XElement grid);
    }
}
