using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model.Prototyping
{
    /*
    Basic interfaces for behavior:
    Populate<T>
    WriteOut<T>
    Command<T> - what is T for a command?
    Query<,>
    Adapter<,>
    Proxy<>
    Repository<> - instead of "plural" data objects

    ICommandHandler<ICommand<ISpecification, >>> -- uses IRepository to get IEnumerable

    INSTEAD: ICommandHandler<ICommand<IQuery, IChange>>> -- uses IRepository to get IEnumerable
    */

    // Node, Attribute, Value:
    // get child node, get attribute, set attribute to value
    // Node, --, Value:
    // get child node, set node value
    // --, Attribute, Value:
    // get attribute on main node, set attribute to value
    // 

    /*
    * 
    * 
    * 
    * */

    interface IOperation<T>
    {

    }


    //class AddNode : IOperation<XElement>
    //{
    //    public void Execute(XElement parent, XObject toadd)
    //    {
    //        parent.Add(toadd);
    //    }
    //}

    //class ChangeNodeValue : IOperation<XElement>
    //{
    //    public void Execute(XElement parent, string v)
    //    {
    //        parent.Value = v;
    //    }
    //}

    //class DeleteNode : IOperation<XElement>
    //{
    //    public void Execute(XElement element)
    //    {
    //        element.Remove();
    //    }
    //}

    public interface ICommand
    {
        
    }

    class AddNode : IOperation<ICommand>
    {
        public void Execute(ICommand command)
        {
            //command.Add(command.element);
        }
    }

    class ChangeNodeValue : IOperation<ICommand>
    {
        public void Execute(ICommand command)
        {
            //command.element.Value = v;
        }
    }

    class DeleteNode : IOperation<ICommand>
    {
        public void Execute(ICommand command)
        {
            //command.element.Remove();
        }
    }

    public interface ICommandHandler<TCommand>
    {
        void Execute(TCommand command);
    }




    interface IRepository<T>
    {
        List<T> Query(Expression<Func<T, bool>> query);
    }



    public class ChangeNodeValueCommand : ICommand
    {
        public Expression<Func<XElement, bool>> Query;
        public string Value { get; set; }
    }



    public class ChangeNodeValueCommandHandler : ICommandHandler<ChangeNodeValueCommand>
    {
	    private IRepository<XElement> _repository;
        private List<string> _undoList = new List<string>();
        private List<XElement> _affectedElements;

        ChangeNodeValueCommandHandler(IRepository<XElement> repository)
        {
            _repository = repository;
        }

        public void Execute(ChangeNodeValueCommand command)
        {
            _affectedElements = _repository.Query(command.Query);

            for (int i = 0; i < _affectedElements.Count; i++)
            {
                _undoList.Add(_affectedElements[i].Value);
                _affectedElements[i].Value = command.Value;
            }
        }

        public void Undo(ChangeNodeValueCommand command)
        {
            for (int i = 0; i < _undoList.Count; i++)
            {
                _affectedElements[i].Value = _undoList[i];
                _affectedElements = null;
                _undoList = null;
            }
        }
    }







/*
 * 
 * 
 * 
 * 
 * /




    
    public interface IChangeDescriptor
    {
        List<(string Node, string Attribute, string Value)> NewPropertyValues { get; }
    }

    public interface IQuery<TResult>
    {
        Expression<Func<TResult, bool>> Criteria { get; }
    }

    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }

    public interface ICommand<TType>
    {
        IQuery<TType> Query { get; }
        IChangeDescriptor Change { get; }
    }

    public class XMLCommand : ICommand<List<XElement>>
    {
        public IQuery<List<XElement>> Query { get; }
        public IChangeDescriptor Change { get; }

        void test ()
        {
            XNode n;
           
        }
    }

    public interface ICommandExecutor<TCommand> //where TCommand : ICommand<TType>
    {
        // uses IRepository to carry out work

        void Execute(TCommand command);
        void Undo(TCommand command);
    }

    public class XMLCommandHandler : ICommandExecutor<XMLCommand>
    {
        private List<(XElement Element, string NodeName, string NewNodeValue)> _undoList;

        public void Execute(XMLCommand command)
        {
            throw new NotImplementedException();
        }

        public void Undo(XMLCommand command)
        {
            throw new NotImplementedException();
        }
    }



    /*
            ISpecification contains:
            - query
            - properties names to change
            - new property value




            unitOfWork - contains multiple repositories - if necessary

            repositories use dbcontext, which seems to also implement similar methods, and it's an abstaction layer over the database




        */


        /*
    public interface IRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
    }
    */

    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
    }

    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        // string-based includes allow for including children of children, e.g. Basket.Items.Product
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }

    //public class Repository<T>
    //{
    //    public IEnumerable<T> List(ISpecification<T> spec)
    //    {
    //        // fetch a Queryable that includes all expression-based includes
    //        var queryableResultWithIncludes = spec.Includes
    //            .Aggregate(_dbContext.Set<T>().AsQueryable(),
    //                (current, include) => current.Include(include));

    //        // modify the IQueryable to include any string-based include statements
    //        var secondaryResult = spec.IncludeStrings
    //            .Aggregate(queryableResultWithIncludes,
    //                (current, include) => current.Include(include));

    //        // return the result of the query using the specification's criteria expression
    //        return secondaryResult
    //                        .Where(spec.Criteria)
    //                        .AsEnumerable();
    //    }
    //}


    public interface IPopulator<T>
    {
        void Populate(T collection);
    }



/*
        names for undo stuff: history... recordable...

        Example command: ReplaceAllArmorBlocksToHeavy
        - get the ID/object of light armor, get the ID/object of heavy armor
        - get all light armor blocks in blueprint data
        - iterate through light armor blocks, change data type ??? replace ???



        object structure representation links to XML data through XElement references
        to facilitate mutation of underlying XML data

        Mutator<Block> ?
        Block property setter can directly execute the change...
        Perhaps the properties themselves should reference the corresponding XElement?
        Or set up INotifyPropertyChanged link between the XML data and the Block properties? sounds like it would more loosely coupled

        How to save the changes made to support undo/rollback? (important feature, even just for practice/reference)
        Changes need to be grouped by commands; one command can execute a lot of changes all at once
        So that means List<something> - but what is "something"?
        
        The commands themselves should store...?
        We need a list of commands executed, and undo in reverse order
        Important to validate commands before execution, and only store commands which were actually executed



    Idea: Let users make "composite" commands that execute a list of commands? For example if they have their routine of preparing blueprints for submission

        interface ICommand
        {
            Undo();
        }


        Repository<

        List<ICommand>

        Keep root XElement in memory...
        Collect XElement references of child nodes (blocks) for use as a list in separate class
        
        List<XElement> Blocks

        Command objects contain the information of how to find the blocks they want to edit?
        like a query?
        and they have an List<XElement> Undo for storing the affected block(s)
        the repository could return this list to the commands and the command itself would execute the changes?? after it made deep copies of the blocks
        
        OR the repository itself would execute the change, based on a query string and the property to change






    Data:
    
    BlockDefinitions
    BlockDefinition

    ComponentDefinitions
    ComponentDefinition

    IngotDefinitions
    IngotDefinition

    OreDefititions
    OreDefintion

    ShipBlueprint file
    Cube file
    Component file
    Blueprint file


    Overall Structure:

    BlueprintFile
        ShipBlueprint
            Grid
                Block
                    Component
                        Ingot
                            Ore

*/




















        /*
    interface IRepository<T>
    {
        IEnumerable<T> ListAll();
        void Update(T t);
    }
    */

    public interface IQuery<TResult>
    {
    }

    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }

    public class User
    { }


    public class FindUsersBySearchTextQuery : IQuery<User[]>
    {
        public string SearchText { get; set; }

        public bool IncludeInactiveUsers { get; set; }
    }

    public interface IQueryProcessor
    {
        TResult Process<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }

    /*
    public class FindUsersBySearchTextQueryHandler
    : IQueryHandler<FindUsersBySearchTextQuery, User[]>
    {
        private readonly NorthwindUnitOfWork db;

        public FindUsersBySearchTextQueryHandler(NorthwindUnitOfWork db)
        {
            this.db = db;
        }

        public User[] Handle(FindUsersBySearchTextQuery query)
        {
            return (
                from user in this.db.Users
                where user.Name.Contains(query.SearchText)
                select user)
                .ToArray();
        }
    }


    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }

    public interface IValidator<TCommand>
    {
        void Validate(TCommand command);
    }

    public class ValidationCommandHandlerDecorator<T> : ICommandHandler<T>
    {
        private readonly ICommandHandler<T> _decoratee;
        private readonly IValidator<T> _validator;

        ValidationCommandHandlerDecorator(
            ICommandHandler<T> decoratee, IValidator<T> validator)
        {
            _decoratee = decoratee;
            _validator = validator;
        }

        public void Handle(T command)
        {
            var errors = _validator.Validate(command).ToArray();

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }

            _decoratee.Handle(command);
        }
    }
    */
}
