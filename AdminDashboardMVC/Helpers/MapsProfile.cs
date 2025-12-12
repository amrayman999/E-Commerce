using AdminDashboardMVC.Models;
using AutoMapper;
using E_Commerce.Domain.Entities.Products;

namespace AdminDashboardMVC.Helpers
{
    public class MapsProfile : Profile
    {
        public MapsProfile()
        {
            CreateMap<Product,ProductViewModel>().ReverseMap();
        }
    }
}
