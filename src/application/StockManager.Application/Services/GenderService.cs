using application.StockManager.Application.Dtos.responses;
using application.StockManager.Application.Interfaces.Repositories;
using application.StockManager.Application.Interfaces.Services;
using domain.StockManager.Domain.Entities;
using Domain.StockManager.Domain.Exceptions;
using shared.StockManager.Shered.Utils;

namespace application.StockManager.Application.Service
{
    public class GenderService : IGenderService
    {

        private readonly IGenderRepository _genderRepository;

        public GenderService(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }


        public async Task<Result<IEnumerable<GenderResponseDto>>> GetAllGender()
        {
            try
            {
                var genders = await _genderRepository.GetAll();

                if (genders == null)
                {
                    return Result<IEnumerable<GenderResponseDto>>.Failure("Nenhum dado foi encontrado");
                }

                List<GenderResponseDto> genderList = new();

                foreach (var item in genders)
                {
                    var response = new GenderResponseDto
                    {
                        Id = item.Id,
                        Name = item.Name
                    };

                    genderList.Add(response);
                }

                return Result<IEnumerable<GenderResponseDto>>.Success(genderList);
            }
            catch (BusinessException ex)
            {
                return Result<IEnumerable<GenderResponseDto>>.Failure(ex.Message);
            }
        }
    }
}