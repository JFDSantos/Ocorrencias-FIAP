using AutoMapper;
using Fiap.Web.Ocorrencia.Data.Repository;
using Fiap.Web.Ocorrencia.Services;
using Fiap.Web.Ocorrencia.ViewModel;
using Fiap.Web.Ocorrencias.Data.Repository;
using Fiap.Web.Ocorrencias.Data;
using Fiap.Web.Ocorrencias.Models;
using Fiap.Web.Ocorrencias.Services;
using Fiap.Web.Ocorrencias.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region INICIALIZANDO O BANCO DE DADOS
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);
#endregion

#region ServicesAndRepositorys
builder.Services.AddScoped<IOcorrenciaRepository, OcorrenciaRepository>();
builder.Services.AddScoped<IOcorrenciaServices, OcorrenciaServices>();

builder.Services.AddScoped<IAtendimentoRepository, AtendimentoRepository>();
builder.Services.AddScoped<IAtendimentoServices, AtendimentoServices>();

builder.Services.AddScoped<ILocalizacaoServices, LocalizacaoServices>();
builder.Services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();

builder.Services.AddScoped<IGravidadeRepository, GravidadeRepository>();
builder.Services.AddScoped<IGravidadeServices, GravidadeServices>();

builder.Services.AddScoped<IServEmergenciaRepository, ServEmergenciaRepository>();
builder.Services.AddScoped<IServEmergenciaServices, ServEmergenciaServices>();

builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<IVeiculoServices, VeiculoServices>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioServices, UsuarioServices>();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthServices, AuthServices>(); // Registra a interface e a implementação correta

builder.Services.AddScoped<IUsuarioRoleRepository,UsuarioRoleRepository>();
builder.Services.AddScoped<IUsuarioRoleServices, UsuarioRoleServices>();
#endregion

#region AutoMapper
var mapperConfig = new AutoMapper.MapperConfiguration(c => {
    c.AllowNullCollections = true;
    c.AllowNullDestinationValues = true;

    c.CreateMap<GravidadeModel, GravidadeCreateViewModel>();
    c.CreateMap<GravidadeCreateViewModel, GravidadeModel>();

    c.CreateMap<UsuarioModel, UsuarioCreateViewModel>();
    c.CreateMap<UsuarioCreateViewModel, UsuarioModel>();

    c.CreateMap<UsuarioRoleModel, UsuarioRoleCreateViewModel>();
    c.CreateMap<UsuarioRoleCreateViewModel, UsuarioRoleModel>();

    c.CreateMap<AtendimentoModel, AtendimentoCreateViewModel>();
    c.CreateMap<AtendimentoCreateViewModel, AtendimentoModel>();

    c.CreateMap<LocalizacaoModel, LocalizacaoCreateViewModel>();
    c.CreateMap<LocalizacaoCreateViewModel, LocalizacaoModel>();

    c.CreateMap<OcorrenciaModel, OcorrenciaCreateViewModel>()
    .ForMember(dest => dest.Localizacao, opt => opt.Ignore())
    .ForMember(dest => dest.Gravidade, opt => opt.Ignore());

    c.CreateMap<OcorrenciaCreateViewModel, OcorrenciaModel>()
    .ForMember(dest => dest.Gravidade, opt => opt.Ignore())
    .ForMember(dest => dest.Localizacao, opt => opt.Ignore());

    c.CreateMap<ServicoEmergenciaModel, ServicoEmergenciaCreateViewModel>()
     .ForMember(dest => dest.Gravidade, opt => opt.Ignore());

    c.CreateMap<ServicoEmergenciaCreateViewModel, ServicoEmergenciaModel>()
    .ForMember(dest => dest.Gravidade, opt => opt.Ignore());

    c.CreateMap<VeiculoModel, VeiculoCreateViewModel>();
    c.CreateMap<VeiculoCreateViewModel, VeiculoModel>();

    // Mapeamento entre OcorrenciaModel e OcorrenciaViewModel
    c.CreateMap<OcorrenciaModel, OcorrenciaViewModel>()
        .ForMember(dest => dest.Localizacao, opt => opt.MapFrom(src => src.Localizacao))
        .ForMember(dest => dest.Gravidade, opt => opt.MapFrom(src => src.Gravidade));

    c.CreateMap<OcorrenciaViewModel, OcorrenciaModel>()
        .ForMember(dest => dest.Localizacao, opt => opt.MapFrom(src => src.Localizacao))
        .ForMember(dest => dest.Gravidade, opt => opt.MapFrom(src => src.Gravidade));

    // Mapeamento entre LocalizacaoModel e LocalizacaoViewModel
    c.CreateMap<LocalizacaoModel, LocalizacaoViewModel>();
    c.CreateMap<LocalizacaoViewModel, LocalizacaoModel>();

    // Mapeamento entre GravidadeModel e GravidadeViewModel
    c.CreateMap<GravidadeModel, GravidadeViewModel>();
    c.CreateMap<GravidadeViewModel, GravidadeModel>();

    c.CreateMap<UsuarioModel, UsuarioViewModel>();
    c.CreateMap<UsuarioViewModel, UsuarioModel>();

    c.CreateMap<UsuarioRoleModel, UsuarioRoleViewModel>();
    c.CreateMap<UsuarioRoleViewModel, UsuarioRoleModel>();

    c.CreateMap<AtendimentoModel, AtendimentoViewModel>();
    c.CreateMap<AtendimentoViewModel, AtendimentoModel>();

    c.CreateMap<ServicoEmergenciaModel, ServicoEmergenciaViewModel>();
    c.CreateMap<ServicoEmergenciaViewModel, ServicoEmergenciaModel>();

    c.CreateMap<VeiculoModel, VeiculoViewModel>();
    c.CreateMap<VeiculoViewModel, VeiculoModel>();
});


IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region Auth
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Adicione esta linha para garantir que o middleware de autenticação seja usado
app.UseAuthorization();

app.MapControllers();

app.Run();
