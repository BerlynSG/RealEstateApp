using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class TipoVentaService : GenericService<SaveTipoVentaViewModel, TipoVentaViewModel, TipoVenta>, ITipoVentaService
    {
        private readonly ITipoVentaRepository _repository;
        private readonly IMapper _mapper;
        public TipoVentaService(ITipoVentaRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
