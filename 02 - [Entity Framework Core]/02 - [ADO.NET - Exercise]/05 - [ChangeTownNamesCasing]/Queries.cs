
namespace _5._Change_Town_Names_Casing
{
  public static  class Queries
    {
        public const string ChangeCityNames = "UPDATE Towns SET Name = UPPER(Name)WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";

        public const string FindAllCityNames = "SELECT t.Name FROM Towns as t JOIN Countries AS c ON c.Id = t.CountryCode WHERE c.Name = @countryName";
    }
}
