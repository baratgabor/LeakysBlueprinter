namespace LeakysBlueprinter.Model.Queries
{
    public class GetGridMassQuery : IGridQuery<float>
    {
        public string IdentifierName { get; } = "CalculateGridMass";
        public string FriendlyName { get; } = "Calculate Grid Mass";

        public string GridEntityId { get; set; }
    }
}
