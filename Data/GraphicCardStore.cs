using WonderFulGraphicCards_API.Models;
using WonderFulGraphicCards_API.Models.Dto;

namespace WonderFulGraphicCards_API.Data;
public static class GraphicCardStore
{
    public static List<GraphicCardDto> GraphicsCardList = new List<GraphicCardDto>
    {
        new GraphicCardDto { Id = 1, Name = "GTX 1080", CudaCores = 2560, CoreClockSpeed = 1733},
        new GraphicCardDto { Id = 2, Name = "RX 5700 XT", CudaCores = 2560, CoreClockSpeed = 1905},
        new GraphicCardDto { Id = 3, Name = "RTX 4060", CudaCores = 3072, CoreClockSpeed = 2460},
        new GraphicCardDto { Id = 4, Name = "RTX 4090", CudaCores = 16384, CoreClockSpeed = 2520}
    };
}