namespace Learn.C.Sharp.Services
{
    public class PropertyMapping<TSource, TDestination>(Dictionary<string, PropertyMappingValue> mappingDictionary) : IPropertyMapping
    {
        public Dictionary<string, PropertyMappingValue> _mappingDictionary { get; set; } = mappingDictionary;
    }
}
