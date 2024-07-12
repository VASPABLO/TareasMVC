using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TareasMVC.Servicios;
using Microsoft.AspNetCore.Mvc.Razor;

namespace TareasMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Esta linea autenticamos la politica de usuario

            var politicaUsuarioAutenticados = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            // Add services to the container.
            builder.Services.AddControllersWithViews(opciones =>
            {
                opciones.Filters.Add(new AuthorizeFilter(politicaUsuarioAutenticados));

            }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization(opciones =>
            {
                opciones.DataAnnotationLocalizerProvider = (_, factoria) =>
                    factoria.Create(typeof(RecursoCompartido));  
            });

            builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
            opciones.UseSqlServer("name=DefaultConnection"));

            builder.Services.AddAuthentication();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
            {
                opciones.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, opciones =>
            {
                opciones.LoginPath = "/usuarios/login";
                opciones.AccessDeniedPath = "/usuarios/login";
            });
            // Configuracion de Servicio para usar otro idioma
            builder.Services.AddLocalization(opciones =>
            {
                opciones.ResourcesPath = "Recursos";
            });
            //Fin de la configuracion

            var app = builder.Build();

            //Definir los idiomas

            app.UseRequestLocalization(opciones =>
            {
                opciones.DefaultRequestCulture = new RequestCulture("es");
                opciones.SupportedUICultures = Constantes.CulturasUISoportadas 
                .Select(cultura => new CultureInfo(cultura.Value)).ToList();
            });
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}