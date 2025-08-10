using BogusDemoApiGateway;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
builder.Services.AddHttpClient<BogusDemoClient>();
builder.Services.AddOptions<EndpointOption>().Bind(builder.Configuration.GetSection("Endpoints"));

var constr = builder.Configuration.GetConnectionString("GatewayIdentityDb");
builder.Services.AddDbContext<GatewayIdentityDbContext>(options => options.UseSqlServer(constr));

builder.Services
    .AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<GatewayIdentityDbContext>();

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

var group = app.MapGroup("").RequireAuthorization();

group.MapGet("get-departments", async (BogusDemoClient client, CancellationToken ct,
    int pageNumber = 1, int pageSize = 10) =>
{
    var content = await client.GetDepartmentsAsync(pageNumber, pageSize, ct);
    return TypedResults.Ok(content);
});

group.MapPut("create-department", async (CreateDepartmentRequest request, BogusDemoClient client,
    CancellationToken ct) =>
{
    await client.CreateDepartmentAsync(request, ct);
    return TypedResults.Ok();
});

group.MapPost("change-department-name", async (ChangeDepartmentNameRequest request,
    BogusDemoClient client, CancellationToken ct) =>
{
    await client.ChangeDepartmentNameAsync(request, ct);
    return TypedResults.Ok();
});

group.MapPut("create-room", async (CreateRoomRequest request, BogusDemoClient client,
    CancellationToken ct) =>
{
    await client.CreateRoomAsync(request, ct);
    return TypedResults.Ok();
});

group.MapPost("change-room", async (ChangeRoomRequest request, BogusDemoClient client,
    CancellationToken ct) =>
{
    await client.ChangeRoomAsync(request, ct);
    return TypedResults.Ok();
});

group.MapDelete("delete-room", async (int departmentId, int roomId, BogusDemoClient client,
    CancellationToken ct) =>
{
    await client.DeleteRoomAsync(departmentId, roomId, ct);
    return TypedResults.Ok();
});

group.MapDelete("delete-department", async (int id, BogusDemoClient client, CancellationToken ct) =>
{
    await client.DeleteDepartmentAsync(id, ct);
    return TypedResults.Ok();
});

app.Run();