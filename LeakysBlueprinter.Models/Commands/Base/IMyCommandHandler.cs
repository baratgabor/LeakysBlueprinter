namespace LeakysBlueprinter.Model
{
    internal interface IMyCommandHandler<TCommand> where TCommand : IMyCommand
    {
        void Handle(TCommand command);
    }
}
