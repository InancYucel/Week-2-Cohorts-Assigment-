using Microsoft.AspNetCore.Mvc;
using WonderFulGraphicCards_API.Models.Dto;

namespace WonderFulGraphicCards_API.Interface;

public interface IGraphicCardShowService
{
    public GraphicCardDto? FindGraphicCardWithId(int id);

}