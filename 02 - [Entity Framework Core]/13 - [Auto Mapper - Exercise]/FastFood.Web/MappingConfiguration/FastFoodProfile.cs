using System;
using System.Globalization;
using AutoMapper;
using FastFood.Models;
using FastFood.Models.Enums;
using FastFood.Web.ViewModels.Positions;
using FastFood.Web.ViewModels.Categories;
using FastFood.Web.ViewModels.Employees;
using FastFood.Web.ViewModels.Items;
using FastFood.Web.ViewModels.Orders;

namespace FastFood.Web.MappingConfiguration
{
    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Position, PositionsAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            //Categories
            this.CreateMap<CreateCategoryInputModel, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.CategoryName));

            this.CreateMap<Category, CategoryAllViewModel>();

            //Employees
            this.CreateMap<Position, RegisterEmployeeViewModel>()
                .ForMember(x => x.PositionId, y => y.MapFrom(s => s.Id))
                .ForMember(x => x.PositionName, y => y.MapFrom(s => s.Name));

            this.CreateMap<RegisterEmployeeInputModel, Employee>();

            this.CreateMap<Employee, EmployeesAllViewModel>()
                .ForMember(x => x.Position, y => y.MapFrom(s => s.Position.Name));

            //Items
            this.CreateMap<Category, CreateItemViewModel>()
                .ForMember(x => x.CategoryId, y => y.MapFrom(s => s.Id))
                .ForMember(x => x.CategoryName, y => y.MapFrom(s => s.Name));

            this.CreateMap<CreateItemInputModel, Item>();

            this.CreateMap<Item, ItemsAllViewModels>()
                .ForMember(x => x.Category, y => y.MapFrom(s => s.Category.Name));

            //Orders
            this.CreateMap<Item, CreateOrderItemViewModel>()
                .ForMember(x => x.ItemId, y => y.MapFrom(s => s.Id))
                .ForMember(x => x.ItemName, y => y.MapFrom(s => s.Name));

            this.CreateMap<Employee, CreateOrderEmployeeViewModel>().ForMember(x => x.EmployeeId, y => y.MapFrom(s => s.Id))
                .ForMember(x => x.EmployeeName, y => y.MapFrom(s => s.Name));

            this.CreateMap<CreateOrderInputModel, Order>()
                .ForMember(x => x.DateTime, y => y.MapFrom(s => DateTime.UtcNow))
                .ForMember(x => x.Type, y => y.MapFrom(s => OrderType.ToGo));

            this.CreateMap<CreateOrderInputModel, OrderItem>()
                .ForMember(x => x.ItemId, y => y.MapFrom(s => s.ItemId))
                .ForMember(x => x.Quantity, y => y.MapFrom(s => s.Quantity));

            this.CreateMap<Order, OrderAllViewModel>()
                .ForMember(x => x.Employee, y => y.MapFrom(s => s.Employee.Name))
                .ForMember(x => x.DateTime,
                    y => y.MapFrom(s => s.DateTime.ToString("D", CultureInfo.InvariantCulture)))
                .ForMember(x => x.OrderId, y => y.MapFrom(s => s.Id));
        }
    }
}
