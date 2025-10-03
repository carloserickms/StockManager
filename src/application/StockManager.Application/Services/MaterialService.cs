using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.Interfaces.Repositories;
using application.StockManager.Application.Interfaces.Services;
using domain.StockManager.Domain.Entities;
using domain.StockManager.Domain.Entities.ValueObjects;
using shared.StockManager.Shered.Utils;

namespace application.StockManager.Application.Service;

public class MaterialService : IMaterialService
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
        var colors = await _colorRepository.GetAllColorsOnTheList(colorIds);

        MaterialInfo materialInfo = new(material.Name, material.Amount, material.Value);

        var newMaterial = Material.Create(materialInfo, colors);

        await _materialRepository.Save(newMaterial);

        return Result<Material>.Created("Material criado com sucesso.");
    }

    public Task<Result<Material>> DeleteMaterial(ActionUserDto actionUser)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<Material>>> GetAllMaterial()
    {
        var materiais = await _materialRepository.GetAll();
        return Result<IEnumerable<Material>>.Success(materiais);
    }
}