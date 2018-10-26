namespace LeakysBlueprinter.Model.Exceptions
{
    public enum ExceptionKind
    {
        Application_InitRetryFault_InitStillRunningOrAlreadyComplete,
        Blueprint_StructureInvalid,
        Blueprint_GridNotFound,
        Blueprint_GridEntityIdNotUnique,
        Blueprint_PropertyValueNotNumber,
        Blueprint_NoDeformationToDelete,
        GridOperationFailed_TargetGridNotSpecified,
        DefinitionsLoadError_DuplicateOrMissingDataTypes
    }
}