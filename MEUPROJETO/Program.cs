using MEUPROJETO.Data;
using Microsoft.EntityFrameworkCore; // Namespace onde está o BancoContext

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviço para o contexto do banco de dados com MySQL
//Essa parte configura a injeção de dependencia do contexto
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<BancoContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)
    ));

// Adicionar serviços para MVC e Razor Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configuração da rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LoginTela}/{id?}");

app.Run();




