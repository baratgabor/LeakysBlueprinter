namespace LeakysBlueprinter.Model.Commands
{
    class RemoveAllBlockDamageCommand : IMyGridCommand
    {
        public string IdentifierName { get; } = "RemoveAllBlockDamage";
        public string FriendlyName { get; } = "Remove all block damage";

        public string GridEntityId { get; set; }
    }
}
