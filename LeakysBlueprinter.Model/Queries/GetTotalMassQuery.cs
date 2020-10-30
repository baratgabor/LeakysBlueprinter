namespace LeakysBlueprinter.Model.Queries
{
    public class GetTotalMassQuery : IQuery<float>
    {
        public string IdentifierName { get; } = "CalculateTotalMass";
        public string FriendlyName { get; } = "Calculate Total Mass";
    }
}
