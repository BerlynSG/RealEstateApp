using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Mejora;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class MejoraService : GenericService<SaveMejoraViewModel, MejoraViewModel, Mejora>, IMejoraService
    {
        private readonly IMejoraRepository _repository;
        private readonly IMapper _mapper;
        public MejoraService(IMejoraRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
