using AutoMapper;
using TourGuide.Web.Mappings;

namespace Store.Web.Mappings
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