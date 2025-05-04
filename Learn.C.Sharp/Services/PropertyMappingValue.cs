namespace Learn.C.Sharp.Services
{
    public class PropertyMappingValue(IEnumerable<string> destinationProperties)
    {
        public IEnumerable<string> DestinationProperties { get; private set; } = destinationProperties;
    }
}
