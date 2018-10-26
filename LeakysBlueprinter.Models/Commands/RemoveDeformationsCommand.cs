namespace LeakysBlueprinter.Model.Commands
{
    public class RemoveDeformationsCommand : IMyGridCommand
    {
        public string IdentifierName { get; } = "RemoveDeformations";
        public string FriendlyName { get; } = "Remove deformations";

        public string GridEntityId { get; set; }
    }
}
