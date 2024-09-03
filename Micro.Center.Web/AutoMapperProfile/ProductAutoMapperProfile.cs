using AutoMapper;
using Micro.Center.Entities;
using Micro.Center.Web.Models.Product;

namespace Micro.Center.Web.AutoMapperProfile
{
    public class ProductAutoMapperProfile :Profile
    {
        public ProductAutoMapperProfile()
        {
            CreateMap<Product, ProductViewModel>();
                

            CreateMap<Product, ProductDetailsViewModel>();
            CreateMap<Product, CreateUpdateProductViewModel>().ReverseMap();



        }
    }
}
