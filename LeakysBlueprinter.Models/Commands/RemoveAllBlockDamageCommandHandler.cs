using LeakysBlueprinter.Model.Exceptions;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model.Commands
{
    /// <summary>
    /// Restores the full integrity of all blocks on the grid.
    /// What complicates this is that some blocks might be not fully built.
    /// In that case it restores integrity only up to the build value.
    /// </summary>
    internal class RemoveAllBlockDamageCommandHandler : GridCommandHandlerBase<RemoveAllBlockDamageCommand>
    {
        protected const string _integrityPropertyName = "IntegrityPercent";
        protected const string _buildPropertyName = "BuildPercent";

        public RemoveAllBlockDamageCommandHandler(IBlueprintDataContext dataContext) : base(dataContext)
        { }

        /// <summary>
        /// Execute actual command
        /// </summary>
        protected override void DoHandleOnGrid(RemoveAllBlockDamageCommand command, XElement grid)
        {
            var blocksWithDamage = _dataContext.GetBlocksWithProperty(_integrityPropertyName, grid);

            foreach (var block in blocksWithDamage)
            {
                // Block property representing block damage as float from 0 to 1 (only present if <1)
                var integrityProperty = block.Element(_integrityPropertyName);
                // Block property representing build state as float from 0 to 1 (only present if <1)
                var buildProperty = block.Element(_buildPropertyName);

                // If block is not fully built ...
                if (buildProperty != null)
                {
                    // (check if both property values are valid floats)
                    if (!float.TryParse(integrityProperty.Value, out float integrityValue) ||
                        !float.TryParse(buildProperty.Value, out float buildValue))
                            throw new AppException(ExceptionKind.Blueprint_PropertyValueNotNumber);

                    // ... and integrity is lower than build percent (i.e. block has additional damage) ...
                    if (integrityValue < buildValue)
                        // ... then set integrity to build percent, removing additional damage.
                        integrityProperty.Value = buildProperty.Value;
                }
                // Otherwise ...
                else
                {
                    // ... simply remove the integrity, thereby restoring the block to full integrity.
                    integrityProperty.Remove();
                }
            }
        }
    }
}
