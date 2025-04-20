namespace learn_c_sharp.Services
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
        public bool IsMappingExists<TSource, TDestination>(string fields);
    }
}