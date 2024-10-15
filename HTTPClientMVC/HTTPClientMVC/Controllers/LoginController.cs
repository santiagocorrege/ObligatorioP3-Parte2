using HTTPClientMVC.Models.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace HTTPClientMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7223/api/";

        //Configuracion para deserializar el json y evitar errores por mayusculas y minusculas
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_url);
        }

        // GET: LoginController
        public ActionResult Autorizar()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autorizar(LoginModel loginModel)
        {
            try
            {
                var json = JsonSerializer.Serialize(loginModel);
                var body = new StringContent(json, Encoding.UTF8, "application/json");
                var respuesta = _httpClient.PostAsync("Usuario/Login", body).Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    var content = respuesta.Content.ReadAsStringAsync().Result;

                    var token = JsonSerializer.Deserialize<LoginTokenModel>(content, _jsonOptions);
                    if (token == null)
                    {
                        ViewBag.Error = "No se ha podido deserializar el token";
                        return View(loginModel);
                    }
                    HttpContext.Session.SetString("Token", token.Token);
                    HttpContext.Session.SetString("Rol", token.Rol);
                    //HttpContext.Session.SetString("Email", token.Email);
                    //Agregar el token a las cabeceras de las peticiones, para que el servidor lo pueda validar
                    //No olvidar que el token debe ser enviado en todas las peticiones
                    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
                    return RedirectToAction("Index", "MovimientoStock");
                }
                else
                {
                    SetError(respuesta);
                    return View(loginModel);
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(loginModel);
            }
        }

        public ActionResult Logout()
        {
            if (HttpContext.Session.GetString("Token") == null)
            {
                return RedirectToAction("Login");
            }

            HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove("Rol");
            HttpContext.Session.Remove("Email");
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            return RedirectToAction("Autorizar");
        }
        private void SetError(HttpResponseMessage respuesta)
        {
            var contenidoError = respuesta.Content.ReadAsStringAsync().Result;
            dynamic mensajeJson = JObject.Parse(@"{'Message':'" + contenidoError + "'}");
            ViewBag.Error = $"Hubo un error. {respuesta.ReasonPhrase} " + mensajeJson.Message;
        }


    }
}
