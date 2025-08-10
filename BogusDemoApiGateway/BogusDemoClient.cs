using Microsoft.Extensions.Options;

namespace BogusDemoApiGateway;

public class BogusDemoClient
{
    private readonly HttpClient _httpClient;
    private readonly EndpointOption _endpointOption;

    public BogusDemoClient(HttpClient httpClient, IOptionsMonitor<EndpointOption> optionsMonitor)
    {
        _httpClient = httpClient;
        _endpointOption = optionsMonitor.CurrentValue;

        _httpClient.BaseAddress = new Uri(_endpointOption.BaseAddress);
    }

    public async Task<IEnumerable<DepartmentDTO>> GetDepartmentsAsync(int pageNumber, int pageSize,
        CancellationToken ct)
    {
        var url = $"{_endpointOption.GetDepartmentsEndpoint}?pageNumber={pageNumber}&pageSize={pageSize}";
        using var response = await _httpClient.GetAsync(url, ct);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadFromJsonAsync<IEnumerable<DepartmentDTO>>(ct)
            ?? throw new InvalidCastException();

        return content;
    }

    public async Task CreateDepartmentAsync(string name, CancellationToken ct)
    {
        var url = $"{_endpointOption.CreateDepartmentEndpoint}?name={name}";
        using var response = await _httpClient.PutAsync(url, default, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task ChangeDepartmentNameAsync(int id, string name, CancellationToken ct)
    {
        var url = $"{_endpointOption.ChangeDepartmentNameEndpoint}?id={id}&name={name}";
        using var response = await _httpClient.PostAsync(url, default, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task CreateRoomAsync(int id, string roomNumber, CancellationToken ct)
    {
        var url = $"{_endpointOption.CreateRoomEndpoint}?id={id}&roomNumber={roomNumber}";
        using var response = await _httpClient.PutAsync(url, default, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task ChangeRoomAsync(int departmentId, int roomId, string roomNumber,
        CancellationToken ct)
    {
        var url = $"{_endpointOption.ChangeRoomEndpoint}?departmentId={departmentId}&roomId={roomId}&roomNumber={roomNumber}";
        using var response = await _httpClient.PostAsync(url, default, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteRoomAsync(int departmentId, int roomId, CancellationToken ct)
    {
        var url = $"{_endpointOption.DeleteRoomEndpoint}?departmentId={departmentId}&roomId={roomId}";
        using var response = await _httpClient.DeleteAsync(url, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteDepartmentAsync(int departmentId, CancellationToken ct)
    {
        var url = $"{_endpointOption.DeleteDepartmentEndpoint}?departmentId={departmentId}";
        using var response = await _httpClient.DeleteAsync(url, ct);
        response.EnsureSuccessStatusCode();
    }
}

public record DepartmentDTO(int Id, string Name, IEnumerable<RoomDTO> Rooms);

public record RoomDTO(int Id, string RoomNumber);