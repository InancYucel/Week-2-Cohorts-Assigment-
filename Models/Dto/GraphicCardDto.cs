using System.ComponentModel.DataAnnotations;

namespace WonderFulGraphicCards_API.Models.Dto;

public class GraphicCardDto
{
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    [Required]
    public int CudaCores { get; set; }
    [Required]
    public int CoreClockSpeed { get; set; }
}