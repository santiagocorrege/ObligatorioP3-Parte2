
using Gestor.AccesoDatos.EF;
using Gestor.LogicaAplicacion.ImplementacionCU.Articulo;
using Gestor.LogicaAplicacion.ImplementacionCU.MovimientoStock;
using Gestor.LogicaAplicacion.ImplementacionCU.TipoMovimiento;
using Gestor.LogicaAplicacion.ImplementacionCU.Usuario;
using Gestor.LogicaAplicacion.InterfacesCU.Articulo;
using Gestor.LogicaAplicacion.InterfacesCU.MovimientoStock;
using Gestor.LogicaAplicacion.InterfacesCU.TipoMovimiento;
using Gestor.LogicaAplicacion.InterfacesCU.Usuario;
using Gestor.LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Gestor.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //Indicar a Swagger que use el archivo generado durante la compilación 
            //a partir de los comentarios XML
            var rutaArchivo = Path.Combine(AppContext.BaseDirectory, "DocentesApi.xml");
            builder.Services.AddSwaggerGen();

            //builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<GestorContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionGestor")));

            //builder.Services.AddSession();

            
            builder.Services.AddScoped<IRepositorioArticulo, RepositorioArticuloEF>();
            builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuarioEF>();
            builder.Services.AddScoped<IRepositorioMovimientoStock, RepositorioMovimientoStockEF>();
            builder.Services.AddScoped<IRepositorioTipoMovimiento, RepositorioTipoMovimientoEF>();
            builder.Services.AddScoped<IRepositorioParametro, RepositorioParametroEF>();

            //Caso de uso 
            //Tipo Movimientos
            builder.Services.AddScoped<IGetTipoMovimiento, GetTipoMovimiento>();
            builder.Services.AddScoped<IAddTipoMovimiento, AddTipoMovimiento>();
            builder.Services.AddScoped<IUpdateTipoMovimiento, UpdateTipoMovimiento>();
            builder.Services.AddScoped<IDeleteTipoMovimiento, DeleteTipoMovimiento>();

            //Movimientos Stock
            builder.Services.AddScoped<IAddMovimientoStock, AddMovimientoStock>();
            builder.Services.AddScoped<IGetMovimientoStock, GetMovimientoStock>();
            builder.Services.AddScoped<IGetMovimientosStockByArticuloAndTipo, GetMovimientosStockByArticuloAndTipo>();
            builder.Services.AddScoped<IGetArticulosByMovimientosPorFecha, GetArticulosByMovimientosPorFecha>();

            //Articulos
            builder.Services.AddScoped<IGetArticulosAlfabeticamente, GetArticulosAlfabeticamente>();

            //User
            builder.Services.AddScoped<IUsuarioLogin, UsuarioLogin>();

            //inyectar los servicios necesarios para la autenticación            
            var claveEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Xyw2T+gs5Gm8Wd3U3fpGnqBkmyUbwVx8MLL0zj+zD12+4zwW32gNlElHEwqP2+KrEmWFx5H7tqvG1BdFhCqS0w==\r\n"));

            #region Registro de servicios JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    //Definir las verificaciones a realizar
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = claveEncriptada
                };

            });

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
