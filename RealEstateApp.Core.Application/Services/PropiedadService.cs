﻿using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class PropiedadService : GenericService<SavePropiedadViewModel, PropiedadViewModel, Propiedad>, IPropiedadService
    {
        private readonly IGenericRepository<Propiedad> _repository;
        private readonly IMapper _mapper;
        public PropiedadService(IGenericRepository<Propiedad> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
