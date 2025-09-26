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

    public async Task<Result<Material>> CreateMaterial(Material material, List<int> colorIds)
    {

        if (colorIds == null || !colorIds.Any())
        {
            return Result<Material>.Failure("Lista de cores está vazia");
        }

        if (!material.CheckAmoutInputAmout(material.Amount))
        {
            return Result<Material>.Failure("A Quantidade do material não pode ser menor ou igual a zero.");
        }

        var colors = await _colorRepository.GetAllColorsOnTheList(colorIds);

        if (colors != null)
        {
            foreach (var colorsitem in colors)
            {
                _materialRepository.Attach(colorsitem);
                material.AddInColorList(colorsitem);
            }
        }

        await _materialRepository.CreateMaterial(material);

        return Result<Material>.Created("Material criado com sucesso.");
    }

    public Task<Result<Material>> DeleteMaterial(ActionUserDto actionUser)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<Material>>> GetAllMaterial()
    {
        var materiais = await _materialRepository.GetMaterials();
        return Result<IEnumerable<Material>>.Success(materiais);
    }
}