using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Manager.Mappers
{
    public interface IServiceEntityMapper
    {
        TDestination Map<TSource, TDestination>(TSource value);
        TDestination Map<TDestination>(object value);
    }
}
