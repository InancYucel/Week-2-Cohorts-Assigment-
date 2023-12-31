using WonderFulGraphicCards_API.Interface;
using WonderFulGraphicCards_API.Service;

namespace WonderFulGraphicCards_API.Extension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IGraphicCardShowService, GraphicCardShowService>(); //Interface and Implementation
        return services;
    }
}