namespace Facade
{
    public class CarBuilderFacade
    {
        public CarBuilderFacade()
        {
            this.Car = new Car();
        }

        protected Car Car { get; set; }

        public CarInfoBuilder Info 
            => new CarInfoBuilder(this.Car);

        public CarAddressBuilder Built 
            => new CarAddressBuilder(this.Car);

        public Car Build() 
            => this.Car;
    }
}