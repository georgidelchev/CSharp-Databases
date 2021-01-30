namespace Facade
{
    public class CarInfoBuilder : CarBuilderFacade
    {
        public CarInfoBuilder(Car car)
        {
            this.Car = car;
        }

        public CarInfoBuilder WithType(string type)
        {
            this.Car.Type = type;

            return this;
        }

        public CarInfoBuilder WithColor(string color)
        {
            this.Car.Color = color;

            return this;
        }

        public CarInfoBuilder WithNumberOfDoors(int number)
        {
            this.Car.NumberOfDoors = number;

            return this;
        }
    }
}