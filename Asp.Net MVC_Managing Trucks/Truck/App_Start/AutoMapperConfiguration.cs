using AutoMapper;
using SR.Web.Mappings;
using Truck.Mapping;

namespace Truck
{
    public class AutoMapperConfiguration
    {
       public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });

            return config.CreateMapper();
        }
    }
}