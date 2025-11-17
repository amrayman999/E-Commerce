using AutoMapper;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Shared.Produucts;
using Microsoft.Extensions.Configuration;



namespace E_Commerce.Service.MappingProfile
{
    public class ProductProfile : Profile
    {

        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(new ProductPictureUrlResolver(configuration)));


        }
    }

    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _settings;
        public ProductPictureUrlResolver(IConfiguration settings)
        {
            _settings = settings;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrWhiteSpace(source.PictureUrl))
            {
                return string.Empty;
            }
            return $"{_settings["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
