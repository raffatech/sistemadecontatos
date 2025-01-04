using MEUPROJETO.Data;
using MEUPROJETO.Repositorio;
using Microsoft.EntityFrameworkCore; // Namespace onde est� o BancoContext

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�o para o contexto do banco de dados com MySQL
//Essa parte configura a inje��o de dependencia do contexto
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<BancoContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)
    ));

//configurando a inje��o de dependencia da interface para saber quem vai resolver a classe de implementa��o
//toda vez que a interface IContatoRepositorio for chamada, vai usar a classe contatorepositario
builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();

// Adicionar servi�os para MVC e Razor Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura��o do pipeline de requisi��es HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configura��o da rota padr�o
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




