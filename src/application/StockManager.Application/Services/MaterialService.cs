using application.StockManager.Application.Dtos;
using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.Interfaces;
using application.StockManager.Application.responses;
using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Service;

public class MaterialService : IMaterialSerivice
{

    private readonly IMaterialRepository _materialRepository;
    private readonly IColorRepository _colorRepository;

    public MaterialService(IMaterialRepository materialRepository, IColorRepository colorRepository)
    {
        _materialRepository = materialRepository;
        _colorRepository = colorRepository;
    }

    public async Task<ResultResponseBase> CreateMaterial(Material material, List<int> colorIds)
    {

        if (colorIds != null && colorIds.Any())
        {
            var colors = await _colorRepository.GetAllColorsOnTheList(colorIds);

            if (colors != null)
            {
                foreach (var colorsitem in colors)
                {
                    _materialRepository.Attach(colorsitem);
                    material.Colors.Add(colorsitem);
                }
            }
        }

        await _materialRepository.CreateMaterial(material);

        OperationCompletedResponseDto operationCompleted = new()
        {
            message = "Material foi criado com sucesso."
        };

        return operationCompleted;
    }

    public Task<ResultResponseBase> DeleteMaterial(ActionUserDto actionUser)
    {
        throw new NotImplementedException();
    }
}