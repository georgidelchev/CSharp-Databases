using System.Linq;
using AutoMapper;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<ImportSupplierDTO, Supplier>();

            this.CreateMap<ImportPartDTO, Part>();

            this.CreateMap<ImportCustomersDTO, Customer>();

            this.CreateMap<ImportSalesDTO, Sale>();

            this.CreateMap<Supplier, ExportLocalSupplierDTO>()
                .ForMember(x => x.PartsCount, y => y.MapFrom(s => s.Parts.Count));

            this.CreateMap<Car, ExportCarsWithDistanceDTO>();

            this.CreateMap<Car, ExportGetCarsFromMakeBmwDTO>();

            this.CreateMap<Part, ExportCarPartDTO>();

            this.CreateMap<Car, ExportCarDTO>()
                .ForMember(x => x.Parts, y => y.MapFrom(s => s.PartCars.Select(pc => pc.Part)
                    .OrderByDescending(p => p.Price)));

            this.CreateMap<Customer, ExportTotalSalesByCustomer>()
                .ForMember(x => x.FullName, y => y.MapFrom(s => s.Name))
                .ForMember(x => x.BoughtCars, y => y.MapFrom(s => s.Sales.Count))
                .ForMember(x => x.SpentMoney,
                    y => y.MapFrom(s => s.Sales.Select(sl => sl.Car.PartCars.Select(pc => pc.Part).Sum(pc => pc.Price)).Sum()));
        }
    }
}
