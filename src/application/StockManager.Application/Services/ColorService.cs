using application.StockManager.Application.Dtos.responses;
using application.StockManager.Application.Interfaces.Repositories;
using application.StockManager.Application.Service;
using shared.StockManager.Shered.Utils;

public class ColorService : IColorService
{

    private readonly IColorRepository _colorRepository;

    public ColorService(IColorRepository colorRepository)
    {
        _colorRepository = colorRepository;
    }

    public async Task<Result<IEnumerable<ColorResponseDto>>> GetAllColors()
    {
        var colors = await _colorRepository.GetAll();

        var colorResponse = colors
            .Select(c => new ColorResponseDto
            {
                id = c.Id,
                name = c.Name
            }).ToList();

        return Result<IEnumerable<ColorResponseDto>>.Success(colorResponse);
    }
}