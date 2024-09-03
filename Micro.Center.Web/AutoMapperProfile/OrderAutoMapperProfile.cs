using AutoMapper;
using Micro.Center.Entities;
using Micro.Center.Web.Models.Order;

namespace Micro.Center.Web.AutoMapperProfile
{
    public class OrderAutoMapperProfile :Profile
    {
        public OrderAutoMapperProfile()
        {
            CreateMap<Order, OrderViewModel>();
            CreateMap<Order, OrderDetailsViewModel>();
            CreateMap<Order, CreateUpdateOrderViewModel>().ReverseMap();



        }
    }
}
