using HTTPClientMVC.Models.Articulo;
using HTTPClientMVC.Models.MovimientoStock;
using HTTPClientMVC.Models.TipoMovimiento;
using HTTPClientMVC.Models.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace HTTPClientMVC.Controllers
{
    public class MovimientoStockController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _uri = "https://localhost:7223/api/";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, WriteIndented = true };

        public MovimientoStockController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_uri);
            //_httpClient.DefaultRequestHeaders.Accept.Clear();
            //_httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }
        // GET: MovimientoStockController
        public ActionResult Index()
        {
            var token = HttpContext.Session.GetString("Token");
            if (token == null)
            {
                return RedirectToAction("Autorizar", "Usuario");
            }
            HttpResponseMessage respuesta = _httpClient.GetAsync("MovimientoStock").Result;            
            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string json = respuesta.Content.ReadAsStringAsync().Result;
                var lista = JsonSerializer.Deserialize<List<MovimientoStockModel>>(json, _jsonOptions);
                return View(lista);
            }
            else
            {
                SetError(respuesta);
                return RedirectToAction("Create", "MovimientoStock");
            }            
        }

        //// GET: MovimientoStockController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: MovimientoStockController/Create
        public ActionResult Create()
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");
                if ((token == null))
                {
                    return RedirectToAction("Autorizar", "Usuario");
                }
                var articulos = GetArticulos();
                if (articulos == null)
                {
                    throw new Exception("No existen articulos registrados o no tiene acceso a los mismos");
                }                
                var tipoMovimientos = GetTiposDeMovimientos();
                if(tipoMovimientos == null)
                {
                    throw new Exception("No existen articulos registrados o no tiene acceso a los mismos");
                }
                var selectListArticulos = new SelectList(articulos, "Id", "Nombre");
                var selectListTipoMovimiento = new SelectList(tipoMovimientos, "Id", "Nombre");
                var viewModel = new MovimientoStockCreateModel
                {
                    ArticulosSelectList = selectListArticulos,
                    TipoMovimientoSelectList = selectListTipoMovimiento,
                };
                return View(viewModel);
            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "MovimientoStock");
            }            
        }

        public List<ArticuloModel> GetArticulos()
        {
            var token = HttpContext.Session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", token);
            var response = _httpClient.GetAsync("Articulo").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                var lista = JsonSerializer.Deserialize<List<ArticuloModel>>(json, _jsonOptions);                
                return lista;
            }
            return null;
        }

        public List<TipoMovimientoModel> GetTiposDeMovimientos()
        {
            var token = HttpContext.Session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", token);
            var response = _httpClient.GetAsync("TipoMovimiento").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                var lista = JsonSerializer.Deserialize<List<TipoMovimientoModel>>(json, _jsonOptions);
                return lista;
            }
            return null;
        }

        // POST: MovimientoStockController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovimientoStockCreateModel model)
        {
            var token = HttpContext.Session.GetString("Token");
            if ((token == null))
            {
                return RedirectToAction("Autorizar", "Usuario");
            }            
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", token);
            if (model == null)
            {
                ViewBag.Error = "No se puede agregar un movimiento de stock con datos nulos";
                return View();
            }
            try
            {
                //Enviar el docente a la api
                var movimientoStockAdd = new MovimientoStockAddModel()
                {
                    ArticuloId = model.SelectedArticuloId,
                    TipoMovimientoId = model.SelectedTipoMovimientoId,
                    Cantidad = model.Cantidad,
                };
                var json = JsonSerializer.Serialize(movimientoStockAdd);
                //Configurar el contenido del request
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                //Enviar el request a la api para crear el docente en la base de datos
                var respuesta = _httpClient.PostAsync("MovimientoStock", content).Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    SetError(respuesta);
                    return RedirectToAction("Create", "MovimientoStock");
                }

            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

 
        public ActionResult GetFiltradosPorArticuloYTipo()
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");
                if ((token == null))
                {
                    return RedirectToAction("Autorizar", "Usuario");
                }
                var articulos = GetArticulos();
                if (articulos == null)
                {
                    throw new Exception("No existen articulos registrados o no tiene acceso a los mismos");
                }
                var tipoMovimientos = GetTiposDeMovimientos();
                if (tipoMovimientos == null)
                {
                    throw new Exception("No existen articulos registrados o no tiene acceso a los mismos");
                }
                var selectListArticulos = new SelectList(articulos, "Id", "Nombre");
                var selectListTipoMovimiento = new SelectList(tipoMovimientos, "Id", "Nombre");
                var viewModel = new MovimientoStockCreateModel
                {
                    ArticulosSelectList = selectListArticulos,
                    TipoMovimientoSelectList = selectListTipoMovimiento,
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("GetFiltradosPorArticuloYTipo", "MovimientoStock");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetFiltradosPorArticuloYTipo(MovimientoStockCreateModel model)
        {
            var token = HttpContext.Session.GetString("Token");
            if ((token == null))
            {
                return RedirectToAction("Autorizar", "Usuario");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", token);
            if (model == null)
            {
                ViewBag.Error = "No se puede agregar un movimiento de stock con datos nulos";
                return View();
            }
            try
            {
                if (model.Pagina == null)
                    model.Pagina = 1;

                //Enviar el docente a la api
                var movimientoStockAdd = new MovimientoStockAddModel()
                {
                    ArticuloId = model.SelectedArticuloId,
                    TipoMovimientoId = model.SelectedTipoMovimientoId,                    
                };
                var json = JsonSerializer.Serialize(movimientoStockAdd);
                //Configurar el contenido del request
                var body = new StringContent(json, Encoding.UTF8, "application/json");
                //Enviar el request a la api para crear el docente en la base de datos
                var respuesta = _httpClient.GetAsync($"MovimientoStock/MovimientosFiltradosXArticuloTipo/{model.SelectedArticuloId}/{model.SelectedTipoMovimientoId}/{model.Pagina}").Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    var content = respuesta.Content.ReadAsStringAsync().Result;
                    if (String.IsNullOrEmpty(content))
                    {
                        ViewBag.Pagina = -1;
                        return View();
                    }
                    ViewBag.Pagina = model.Pagina++;
                    var respuestaDeserealizada = JsonSerializer.Deserialize<IEnumerable<MovimientoStockConArticuloTipoMovimientoModel>>(content, _jsonOptions);
                    var articulos = GetArticulos();
                    var tipoMovimientos = GetTiposDeMovimientos();
                    var selectListArticulos = new SelectList(articulos, "Id", "Nombre");
                    var selectListTipoMovimiento = new SelectList(tipoMovimientos, "Id", "Nombre");
                    var viewModel = new MovimientoStockCreateModel
                    {
                        ArticulosSelectList = selectListArticulos,
                        TipoMovimientoSelectList = selectListTipoMovimiento,
                        ListaResultadosMovimientos = respuestaDeserealizada
                    };                    
                    return View(viewModel);
                }
                else
                {
                    SetError(respuesta);
                    return RedirectToAction("GetFiltradosPorArticuloYTipo", "MovimientoStock");
                }

            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("GetFiltradosPorArticuloYTipo", "MovimientoStock");
            }
        }


        //

        [HttpGet]
        public ActionResult GetArticulosFiltradosPorMovimientosRangoFechas()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetArticulosFiltradosPorMovimientosRangoFechas(ArticuloFiltradoModel model)
        {
            try
            {
                if (model.Pagina == null)
                    model.Pagina = 1;

                if (model.FechaInicio == DateTime.MinValue || model.FechaFin == DateTime.MinValue)
                {
                    throw new Exception("No se puede filtrar los movimientos, fechas no falidas");
                }

                var token = HttpContext.Session.GetString("Token");
                if ((token == null))
                {
                    return RedirectToAction("Autorizar", "Usuario");
                }
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", token);

                var fechas = new
                {
                    FechaInicio = model.FechaInicio,
                    FechaFin = model.FechaFin
                };
                var json = JsonSerializer.Serialize(fechas);                
                var body = new StringContent(json, Encoding.UTF8, "application/json");                
                var respuesta = _httpClient.PostAsync("MovimientoStock/ArticulosConMovimientosRangoFechas", body).Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var content = respuesta.Content.ReadAsStringAsync().Result;
                    if (String.IsNullOrEmpty(content))
                    {
                        ViewBag.Pagina = -1;
                        return View();
                    }
                    ViewBag.Pagina = model.Pagina++;
                    var lista = JsonSerializer.Deserialize<IEnumerable<ArticuloCompletoModel>>(content, _jsonOptions);
                    
                    
                    if (lista == null)
                    {
                        ViewBag.Error = "No se ha podido deserializar el token";
                        return View();
                    }
                    model.ListaArticulos = lista;
                                        
                    //HttpContext.Session.SetString("Email", token.Email);
                    return View(model);
                }
                else
                {
                    SetError(respuesta);
                    return View();
                }

            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetMovimientoSummary()
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");
                if ((token == null))
                {
                    return RedirectToAction("Autorizar", "Usuario");
                }
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", token);
                HttpResponseMessage respuesta = _httpClient.GetAsync("MovimientoStock/MovimientosSummary").Result;
                if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string json = respuesta.Content.ReadAsStringAsync().Result;
                    var lista = JsonSerializer.Deserialize<List<MovimientoStockSummary>>(json, _jsonOptions);
                    return View(lista);
                }
                else
                {
                    SetError(respuesta);
                    return RedirectToAction("GetMovimientoSummary", "MovimientoStock");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "MovimientoStock");
            }
        }
        private void SetError(HttpResponseMessage respuesta)
        {
            var contenidoError = respuesta.Content.ReadAsStringAsync().Result;
            dynamic mensajeJson = JObject.Parse(@"{'Message':'" + contenidoError + "'}");
            TempData["Error"] = $"Hubo un error. {respuesta.ReasonPhrase} " + mensajeJson.Message;
        }
    }
}
