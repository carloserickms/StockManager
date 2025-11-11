using application.StockManager.Application.Dtos;
using application.StockManager.Application.Dtos.responses;
using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.Interfaces.Repositories;
using application.StockManager.Application.Interfaces.Services;
using domain.StockManager.Domain.Entities;
using domain.StockManager.Domain.Entities.ValueObjects;
using Domain.StockManager.Domain.Exceptions;
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

        try
        {
            var newMaterial = Material.Create(materialInfo, colors);

            await _materialRepository.Save(newMaterial);
        }
        catch (BusinessException ex)
        {
            return Result<Material>.Failure(ex.Message);
        }

        return Result<Material>.Created("Material criado com sucesso.");
    }

    public Task<Result<Material>> DeleteMaterial(ActionUserDto actionUser)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<MaterialResponseDto>>> GetAllMaterial(int page)
    {
        var materiais = await _materialRepository.GetColorMaterial(page);
        IEnumerable<MaterialResponseDto> materialList = new List<MaterialResponseDto>();

        if (materiais == null || !materiais.Any())
        {
            return Result<IEnumerable<MaterialResponseDto>>.Failure("Materiais não encontrados.");
        }

        List<MaterialResponseDto> lista = new();

        foreach (var item in materiais)
        {
            var responseDto = new MaterialResponseDto
            {
                id = item.Id,
                name = item.Name,
                amount = item.Amount,
                value = item.Value,
                colors = item.Colors?.Select(c => new ColorDto
                {
                    id = c.Id,
                    name = c.Name
                }).ToList()
            };

            lista.Add(responseDto);
        }

        return Result<IEnumerable<MaterialResponseDto>>.Success(lista);
    }

    public async Task<Result<IEnumerable<MaterialResponseDto>>> GetMaterialByName(string name)
    {
        var materiaisList = await _materialRepository.GetMaterialByName(name);

        List<MaterialResponseDto> listMaterial = new();

        foreach (var item in materiaisList)
        {
            var responseDto = new MaterialResponseDto
            {
                id = item.Id,
                name = item.Name,
                amount = item.Amount,
                value = item.Value,
                colors = item.Colors?.Select(c => new ColorDto
                {
                    id = c.Id,
                    name = c.Name
                }).ToList()
            };

            listMaterial.Add(responseDto);
        }

        return Result<IEnumerable<MaterialResponseDto>>.Success(listMaterial);
    }

    public async Task<Result<MaterialResponseDto>> GetMaterialById(int id)
    {
        var material = await _materialRepository.GetById(id);

        if (material == null)
        {
            return Result<MaterialResponseDto>.Failure("Nenhum material encontrado.");
        }

        var responseDto = new MaterialResponseDto
        {
            id = material.Id,
            amount = material.Amount,
            name = material.Name,
            value = material.Value,
            colors = material.Colors?.Select(c => new ColorDto
            {
                id = c.Id,
                name = c.Name
            }).ToList()
        };

        return Result<MaterialResponseDto>.Success(responseDto);
    }
}