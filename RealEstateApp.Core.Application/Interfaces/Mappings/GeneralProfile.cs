using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.Mejora;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region UserProfile
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterAdminsRequest, SaveAdminsViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            #endregion
            #region Mejora

            CreateMap<Mejora, MejoraViewModel>()
                    .ReverseMap()
                    .ForMember(x => x.Propiedades, opt => opt.Ignore());



            CreateMap<Mejora, SaveMejoraViewModel>()
                    .ReverseMap()
                    .ForMember(x => x.Id, opt => opt.Ignore());
            #endregion

            #region MejoraPropiedad

            CreateMap<MejoraPropiedad, MejoraViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PropiedadId, opt => opt.Ignore())
                .ForMember(dest => dest.MejoraId, opt => opt.Ignore())
                .ForMember(dest => dest.Mejora, opt => opt.Ignore())
                .ForMember(dest => dest.Propiedad, opt => opt.Ignore());



            CreateMap<MejoraPropiedad, SaveMejoraViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PropiedadId, opt => opt.Ignore())
                .ForMember(dest => dest.MejoraId, opt => opt.Ignore())
                .ForMember(dest => dest.Mejora, opt => opt.Ignore())
                .ForMember(dest => dest.Propiedad, opt => opt.Ignore());

            #endregion


            #region Propiedad

            CreateMap<Propiedad, PropiedadViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.TipoVentaId, opt => opt.Ignore())
                .ForMember(dest => dest.AgenteId, opt => opt.Ignore())
                .ForMember(dest => dest.TipoPropiedadId, opt => opt.Ignore());


            CreateMap<Propiedad, SavePropiedadViewModel>()
                    .ForMember(dest => dest.ListaMejora, opt => opt.Ignore())
                    .ForMember(dest => dest.ListaTipoPropiedad, opt => opt.Ignore())
                    .ForMember(dest => dest.ListaTipoVenta, opt => opt.Ignore())
                    .ReverseMap()
                    .ForMember(dest => dest.TipoVentaId, opt => opt.Ignore())
                    .ForMember(dest => dest.AgenteId, opt => opt.Ignore())
                    .ForMember(dest => dest.TipoPropiedadId, opt => opt.Ignore())
                    .ForMember(dest => dest.Favoritos, opt => opt.Ignore());


            CreateMap<Propiedad, FavoritoViewModel>()
                    .ForMember(dest => dest.UserId, opt => opt.Ignore())
                    .ForMember(dest => dest.PropiedadId, opt => opt.Ignore())
                    .ForMember(dest => dest.Propiedad, opt => opt.Ignore())
                    .ReverseMap()
                    .ForMember(dest => dest.TipoVentaId, opt => opt.Ignore())
                    .ForMember(dest => dest.AgenteId, opt => opt.Ignore())
                    .ForMember(dest => dest.TipoPropiedadId, opt => opt.Ignore())
                    .ForMember(dest => dest.Favoritos, opt => opt.Ignore())
                    .ForMember(dest => dest.Baños, opt => opt.Ignore())
                    .ForMember(dest => dest.Habitaciones, opt => opt.Ignore())
                    .ForMember(dest => dest.AgenteId, opt => opt.Ignore())
                    .ForMember(dest => dest.Imagenes, opt => opt.Ignore())
                    .ForMember(dest => dest.Descripcion, opt => opt.Ignore())
                    .ForMember(dest => dest.Codigo, opt => opt.Ignore())
                    .ForMember(dest => dest.Mejoras, opt => opt.Ignore())
                    .ForMember(dest => dest.Tamaño, opt => opt.Ignore())
                    .ForMember(dest => dest.TipoPropiedad, opt => opt.Ignore())
                    .ForMember(dest => dest.TipoVenta, opt => opt.Ignore());


            #endregion
        }
    }
}
