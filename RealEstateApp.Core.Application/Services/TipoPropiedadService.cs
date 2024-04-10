using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class TipoPropiedadService : GenericService<SaveTipoPropiedadViewModel, TipoPropiedadViewModel, TipoPropiedad>, ITipoPropiedadService
    {
        private readonly IGenericRepository<TipoPropiedad> _repository;
        private readonly IMapper _mapper;
        public TipoPropiedadService(IGenericRepository<TipoPropiedad> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
