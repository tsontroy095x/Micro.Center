using AutoMapper;
using Micro.Center.Entities;
using Micro.Center.Web.Models.Customer;

namespace Micro.Center.Web.AutoMapperProfile
{
    public class CustomerAutoMapperProfile : Profile
    {
        public CustomerAutoMapperProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Customer, CustomerDetailsViewModel>();
            CreateMap<Customer, CreateUpdateCustomerViewModel>().ReverseMap();
        }
    }
}
