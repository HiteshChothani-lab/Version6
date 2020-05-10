namespace UserManagement.Manager.Mappers
{
    public interface IServiceEntityMapper
    {
        TDestination Map<TSource, TDestination>(TSource value);
        TDestination Map<TDestination>(object value);
    }
}
