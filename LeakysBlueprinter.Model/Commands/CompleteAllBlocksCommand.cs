namespace LeakysBlueprinter.Model.Commands
{
    public class CompleteAllBlocksCommand : IMyGridCommand
    {
        public string IdentifierName { get; } = "CompleteAllBlocks";
        public string FriendlyName { get; } = "Complete all blocks";

        public string GridEntityId { get; set; }
    }
}
