using LeakysBlueprinter.Model.Exceptions;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model.Commands
{
    /// <summary>
    /// Completes all partially incomplete blocks on the grid. If a block was additionally damaged besides not being fully complete,
    /// it preserves the damage.
    /// </summary>
    internal class CompleteAllBlocksCommandHandler : GridCommandHandlerBase<CompleteAllBlocksCommand>
    {
        protected const string _buildPropertyName = "BuildPercent";
        protected const string _integrityPropertyName = "IntegrityPercent";

        public CompleteAllBlocksCommandHandler(IBlueprintDataContext dataContext) : base(dataContext)
        { }

        protected override void DoHandleOnGrid(CompleteAllBlocksCommand command, XElement grid)
        {
            var incompleteBlocks = _dataContext.GetBlocksWithProperty(_buildPropertyName, grid);

            foreach (var block in incompleteBlocks)
            {
                // Block property representing build state as float from 0 to 1 (only present if <1)
                var buildProperty = block.Element(_buildPropertyName);
                // Block property representing block damage as float from 0 to 1 (only present if <1)
                var integrityProperty = block.Element(_integrityPropertyName);

                // If BuildPercent is present, IntegrityPercent also must be present, with IntegrityPercent being <= BuildPercent.
                // Missing integrity property is an invalid state, but we'll just ignore it here, and continue.
                if (integrityProperty == null)
                {
                    buildProperty.Remove();
                    continue;
                }

                // Convert both to float for comparisons
                if (!float.TryParse(integrityProperty.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out float integrityValue) ||
                    !float.TryParse(buildProperty.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out float buildValue))
                    throw new AppException(ExceptionKind.Blueprint_PropertyValueNotNumber);

                // Remove the BuildPercent property, making the block fully built. Keep this here, below the potential float.TryParse() exception.
                buildProperty.Remove();

                // If integrity equals to build, we have to simply remove integrity too. (Integrity > build is invalid state, but we'll just treat it normally.)
                if (integrityValue >= buildValue)
                    integrityProperty.Remove();

                // If integrity is lower than build (i.e. block is additionally damaged), set integrity to its proper level for a fully built block.
                // This ensures the preservation of block damage.
                else if (integrityValue < buildValue)
                    integrityProperty.Value = (1 - (buildValue - integrityValue)).ToString();
            }
        }
    }
}
