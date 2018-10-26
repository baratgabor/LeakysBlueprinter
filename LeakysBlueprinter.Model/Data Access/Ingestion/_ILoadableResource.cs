namespace LeakysBlueprinter.Model
{
    internal interface ILoadableResource<T>
    {
        T Load();
    }
}
