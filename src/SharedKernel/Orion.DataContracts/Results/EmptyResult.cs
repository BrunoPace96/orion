namespace Orion.DataContracts.Results
{
    public record EmptyResult
    {
        private EmptyResult() {}
        public static EmptyResult Create() => new();
    }
}