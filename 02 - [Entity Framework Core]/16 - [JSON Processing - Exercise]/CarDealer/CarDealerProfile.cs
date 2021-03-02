using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;
using CarDealer.DTO;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<Car, CarsDTO>();

            this.CreateMap<Supplier, SuppliersDTO>()
                .ForMember(x => x.PartsCount, y => y.MapFrom(s => s.Parts.Count));

            this.CreateMap<Customer, CustomerTotalSalesDTO>()
                .ForMember(x => x.FullName, y => y.MapFrom(s => s.Name))
                .ForMember(x => x.BoughtCars, y => y.MapFrom(s => s.Sales.Count))
                .ForMember(x => x.SpentMoney, y => y.MapFrom(s => s.Sales
                                                                   .Select(s => s.Car
                                                                                 .PartCars
                                                                                 .Select(pc => pc.Part)
                                                                                 .Sum(pc => pc.Price))
                                                                   .Sum()));
        }
    }
}
