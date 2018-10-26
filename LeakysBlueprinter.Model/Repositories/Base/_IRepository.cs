namespace LeakysBlueprinter.Model
{
    internal interface IRepository<TEntity, TId>
    {
        TEntity GetById(TId id);
    }
}
