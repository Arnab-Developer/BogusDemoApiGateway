using Microsoft.Extensions.Options;

namespace BogusDemoApiGateway;

internal class BogusDemoClient
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

    public async Task CreateDepartmentAsync(CreateDepartmentRequest request, CancellationToken ct)
    {
        var content = JsonContent.Create(request);
        using var response = await _httpClient.PutAsync(_endpointOption.CreateDepartmentEndpoint, content, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task ChangeDepartmentNameAsync(ChangeDepartmentNameRequest request, CancellationToken ct)
    {
        var content = JsonContent.Create(request);
        using var response = await _httpClient.PostAsync(_endpointOption.ChangeDepartmentNameEndpoint, content, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task CreateRoomAsync(CreateRoomRequest request, CancellationToken ct)
    {
        var content = JsonContent.Create(request);
        using var response = await _httpClient.PutAsync(_endpointOption.CreateRoomEndpoint, content, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task ChangeRoomAsync(ChangeRoomRequest request, CancellationToken ct)
    {
        var content = JsonContent.Create(request);
        using var response = await _httpClient.PostAsync(_endpointOption.ChangeRoomEndpoint, content, ct);
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

internal record DepartmentDTO(int Id, string Name, IEnumerable<RoomDTO> Rooms);

internal record RoomDTO(int Id, string RoomNumber);

internal record CreateDepartmentRequest(string Name);

internal record ChangeDepartmentNameRequest(int Id, string Name);

internal record CreateRoomRequest(int Id, string RoomNumber);

internal record ChangeRoomRequest(int DepartmentId, int RoomId, string RoomNumber);