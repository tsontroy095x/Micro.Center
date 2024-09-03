using AutoMapper;
using Micro.Center.Entities;
using Micro.Center.Web.Models.ProductType;

namespace Micro.Center.Web.AutoMapperProfile
{
    public class ProductTypeAutoMapperProfile :Profile
    {
        public ProductTypeAutoMapperProfile()
        {
            CreateMap<ProductType, ProductTypeViewModel>();
            CreateMap<ProductType, ProductTypeDetailsViewModel>();
            CreateMap<ProductType, CreateUpdateProductTypeViewModel>().ReverseMap();



        }
    }
}
