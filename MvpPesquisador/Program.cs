using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using MvpPesquisador.Controllers;
using MvpPesquisador.Negocio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<PessoaController>();
builder.Services.AddSingleton<ProjetoController>();
builder.Services.AddSingleton<ResultadoController>();
builder.Services.AddSingleton<EstabelecimentoController>();
builder.Services.AddSingleton<MvpPesquisador.Modelo.Estabelecimento>();
builder.Services.AddSingleton<MvpPesquisador.Modelo.Resultado>();
builder.Services.AddSingleton<MvpPesquisador.Modelo.Pesquisador>();
builder.Services.AddSingleton<MvpPesquisador.Modelo.Aluno>();
builder.Services.AddSingleton<MvpPesquisador.Modelo.Projeto>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
