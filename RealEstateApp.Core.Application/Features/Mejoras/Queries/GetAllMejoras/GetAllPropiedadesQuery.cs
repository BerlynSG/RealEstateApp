﻿using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.Mejora;

namespace RealEstateApp.Core.Application.Features.Mejoras.Queries.GetAllMejoras
{
    /// <summary>
    /// Parámetros para obtener todas las mejoras
    /// </summary>
    public class GetAllMejorasQuery : IRequest<IList<MejoraViewModel>>
    {

    }

    public class GetAllMejorasQueryHandler : IRequestHandler<GetAllMejorasQuery, IList<MejoraViewModel>>
    {
        private readonly IPropiedadRepository _mejoraRepository;
        private readonly IMapper _mapper;

        public GetAllMejorasQueryHandler(IPropiedadRepository mejoraRepository, IMapper mapper)
        {
            _mejoraRepository = mejoraRepository;
            _mapper = mapper;
        }

        public async Task<IList<MejoraViewModel>> Handle(GetAllMejorasQuery request, CancellationToken cancellationToken)
        {
            var Propiedades = await GetAllViewModel();
            if (Propiedades == null || Propiedades.Count == 0) throw new Exception("Mejoras not found");
            return Propiedades;
        }

        private async Task<List<MejoraViewModel>> GetAllViewModel()
        {
            var entityList = await _mejoraRepository.GetAllAsync();

            return _mapper.Map<List<MejoraViewModel>>(entityList);
        }
    }
}
