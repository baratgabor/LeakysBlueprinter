using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model
{
    public sealed class BlueprintService : IService
    {
        // TODO: Ascertain if keeping async init is beneficial
        public Task Initialization { get; private set; }

        public XDocument Blueprint => _blueprint;
        private XDocument _blueprint;
        private IDefinitionsRepository _definitionsRepository;
        private IBlueprintDataContext _blueprintDataContext;

        internal BlueprintService(IDefinitionsRepository definitionsRepository, ILoadableResource<XDocument> blueprintResource)
        {
            _definitionsRepository = definitionsRepository;
            //Initialization = InitializeAsync(blueprintResource);
            _blueprint = blueprintResource.Load();

            _blueprintDataContext = new BlueprintDataContext(XElement.Parse(_blueprint.ToString()));
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

        public TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            // TODO: Replace temporary implementation with something robust
            Type type = typeof(IQueryHandler<TQuery, TResult>);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            var instance = (IQueryHandler<TQuery, TResult>) Activator.CreateInstance(types.First(), _definitionsRepository, _blueprintDataContext );

            return instance.Handle(query);
        }

        // TODO: Implement blueprint saving functionality

        public void Execute<TCommand>(TCommand command) where TCommand : IMyCommand
        {
            // TODO: Implement command execution
            throw new NotImplementedException();
        }
    }
}
