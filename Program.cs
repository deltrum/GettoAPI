using AutoMapper;
using GettoAPI.Data;
using GettoAPI.Dtos;
using GettoAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var sqlConBuilder = new SqlConnectionStringBuilder();

sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("SQLDbConnection");
sqlConBuilder.UserID = builder.Configuration["UserID"];
sqlConBuilder.Password = builder.Configuration["Password"];

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));
builder.Services.AddScoped<IMemberRepo, MemberRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/v1/members", async (IMemberRepo repo, IMapper mapper) => {
    var members = await repo.GetAllMemebers();

    return Results.Ok(mapper.Map<IEnumerable<MemberRDto>>(members));
});

app.MapGet("/api/v1/members/{id}", async (IMemberRepo repo, IMapper mapper, int id) => {
    var member = await repo.GetMemberById(id);

    if(member != null) 
    {
        return Results.Ok(mapper.Map<MemberRDto>(member));
    }

    return Results.NotFound();

});

app.MapPost("/api/v1/members", async (IMemberRepo repo, IMapper mapper, MemberCDto memberCDto) => {
    var memberModel = mapper.Map<Member>(memberCDto);

    await repo.CreateMember(memberModel);
    await repo.SaveChanges();

    var memberRDto = mapper.Map<MemberRDto>(memberModel);

    return Results.Created($"api/v1/commands/{memberRDto.Id}", memberRDto);
});

app.MapPut("api/v1/members/{id}", async (IMemberRepo repo, IMapper mapper, int id, MemberUDto memberUDto) => {
    var memberModel = await repo.GetMemberById(id);

    if (memberModel == null)
    {
        return Results.NotFound();
    }

    mapper.Map(memberUDto, memberModel);

    await repo.SaveChanges();

    return Results.NoContent();
});

app.MapDelete("api/v1/members/{id}", async (IMemberRepo repo, IMapper mapper, int id) => {
    var memberModel = await repo.GetMemberById(id);

    if (memberModel == null)
    {
        return Results.NotFound();
    }

    repo.DeleteCommand(memberModel);

    await repo.SaveChanges();

    return Results.NoContent();
});

app.Run();
