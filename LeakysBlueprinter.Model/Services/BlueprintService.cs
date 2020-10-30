using LeakysBlueprinter.Model.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model
{
    public sealed class BlueprintService : IOperationService
    { 
        public XDocument Blueprint { get; }

        public string BlueprintName { get; private set; }
        public string CreatorName { get; private set; }
        public int GridCount { get; private set; }
        public int BlockCount { get; private set; }
        public float Mass { get; private set; }
        public int DamagedBlockCount { get; private set; }
        public int IncompleteBlockCount { get; private set; }

        private IDefinitionsRepository _definitionsRepository;
        private IBlueprintDataContext _blueprintDataContext;

        internal BlueprintService(IDefinitionsRepository definitionsRepository, ILoadableResource<XDocument> blueprintResource)
        {
            _definitionsRepository = definitionsRepository;

            try
            {
                Blueprint = blueprintResource.Load();
                _blueprintDataContext = new BlueprintDataContext(XElement.Parse(Blueprint.ToString()));
            }
            catch (Exception e)
            {
                throw new Exception("Blueprint cannot be loaded. This is probably because the game changed, and this app became incompatible with the blueprint files.", e);
            }

            SetBlueprintInfo();
        }

        // TODO: Implement execution validity checking
        public bool CanExecute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            throw new NotImplementedException();
        }

        public bool CanExecute<TCommand>(TCommand command) where TCommand : IMyCommand
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            // TODO: Replace temporary implementation with something robust. Why not use MediatR instead?
            var queryType = query.GetType();

            var queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResult));

            var actualType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => queryHandlerType.IsAssignableFrom(p)).First();

            dynamic instance = Activator.CreateInstance(actualType, _definitionsRepository, _blueprintDataContext);

            return instance.Handle((dynamic)query);
        }

        // TODO: Implement blueprint saving functionality

        public void Execute<TCommand>(TCommand command) where TCommand : IMyCommand
        {
            // TODO: Implement command execution
            throw new NotImplementedException();
        }

        private void SetBlueprintInfo()
        {
            BlueprintName = _blueprintDataContext.GetBlueprintName();
            CreatorName = _blueprintDataContext.GetCreatorName();
            GridCount = _blueprintDataContext.GetGridCount();
            BlockCount = _blueprintDataContext.GetBlockCount();
            DamagedBlockCount = _blueprintDataContext.GetDamagedBlockCount();
            IncompleteBlockCount = _blueprintDataContext.GetIncompleteBlockCount();
            Mass = Execute(new GetTotalMassQuery());
        }

    }
}
