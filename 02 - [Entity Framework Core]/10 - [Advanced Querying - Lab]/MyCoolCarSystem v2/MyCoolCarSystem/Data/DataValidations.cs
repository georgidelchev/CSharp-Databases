namespace MyCoolCarSystem.Data
{
    public static class DataValidations
    {
        public static class Make
        {
            public const int MAX_NAME_LENGTH = 20;
        }

        public static class Model
        {
            public const int MAX_NAME_LENGTH = 20;

            public const int MAX_MODIFICATION_LENGTH = 30;
        }

        public static class Car
        {
            public const int MAX_NAME_LENGTH = 20;

            public const int MAX_VIN_LENGTH = 17;

            public const int MAX_COLOR_LENGTH = 15;
        }

        public static class Customer
        {
            public const int MAX_FIRST_NAME_LENGTH = 30;

            public const int MAX_LAST_NAME_LENGTH = 30;
        }

        public static class Address
        {
            public const int MAX_TEXT_LENGTH = 200;

            public const int MAX_TOWN_LENGTH = 30;
        }
    }
}