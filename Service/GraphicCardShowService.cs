using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WonderFulGraphicCards_API.Data;
using WonderFulGraphicCards_API.Interface;
using WonderFulGraphicCards_API.Models.Dto;

namespace WonderFulGraphicCards_API.Service;

public class GraphicCardShowService : IGraphicCardShowService
{
    public GraphicCardDto? FindGraphicCardWithId(int id)
    {
        return GraphicCardStore.GraphicsCardList.FirstOrDefault(x => x.Id == id);
    }
}