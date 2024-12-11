namespace AIR_Wheelly_BLL.Services;

public class FuelTypeService
{
    private readonly List<string> _fuelTypes =
    [
        "Petrol",
        "Diesel",
        "Electric",
        "Hybrid"
    ];

    public IEnumerable<string> GetFuelTypes()
    {
        return _fuelTypes;
    }
}