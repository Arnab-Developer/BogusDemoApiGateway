namespace BogusDemoApiGateway;

public class EndpointOption
{
    public string BaseAddress { get; set; }

    public string GetDepartmentsEndpoint { get; set; }

    public string CreateDepartmentEndpoint { get; set; }

    public string ChangeDepartmentNameEndpoint { get; set; }

    public string CreateRoomEndpoint { get; set; }

    public string ChangeRoomEndpoint { get; set; }

    public string DeleteRoomEndpoint { get; set; }

    public string DeleteDepartmentEndpoint { get; set; }

    public EndpointOption()
    {
        BaseAddress = string.Empty;
        GetDepartmentsEndpoint = string.Empty;
        CreateDepartmentEndpoint = string.Empty;
        ChangeDepartmentNameEndpoint = string.Empty;
        CreateRoomEndpoint = string.Empty;
        ChangeRoomEndpoint = string.Empty;
        DeleteRoomEndpoint = string.Empty;
        DeleteDepartmentEndpoint = string.Empty;
    }
}