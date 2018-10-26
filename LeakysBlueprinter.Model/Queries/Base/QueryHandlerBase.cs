using LeakysBlueprinter.Model.Exceptions;

namespace LeakysBlueprinter.Model
{
    internal abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        protected IBlueprintDataContext _dataContext;

        public QueryHandlerBase(IBlueprintDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Handles the specified query.
        /// </summary>
        public TResult Handle(TQuery query)
        {
            ValidateDataContext();
            return DoHandle(query);
        }

        /// <summary>
        /// Query execution to be implemented in concrete queries.
        /// </summary>
        protected abstract TResult DoHandle(TQuery query);

        /// <summary>
        /// Check if context is still well-formatted for query execution.
        /// Mainly because context is raw XElement structure.
        /// </summary>
        protected void ValidateDataContext()
        {
            if (!_dataContext.IsValid())
                throw new AppException(ExceptionKind.Blueprint_StructureInvalid);
        }
    }
}
