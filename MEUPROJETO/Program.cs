using MEUPROJETO.Data;
using Microsoft.EntityFrameworkCore; // Namespace onde est� o BancoContext

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�o para o contexto do banco de dados com MySQL
//Essa parte configura a inje��o de dependencia do contexto
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<BancoContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)
    ));

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
    pattern: "{controller=Login}/{action=LoginTela}/{id?}");

app.Run();




