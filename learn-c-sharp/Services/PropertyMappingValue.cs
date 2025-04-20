namespace learn_c_sharp.Services
{
    public class PropertyMappingValue(IEnumerable<string> destinationProperties)
    {
        public IEnumerable<string> DestinationProperties { get; private set; } = destinationProperties;
    }
}
