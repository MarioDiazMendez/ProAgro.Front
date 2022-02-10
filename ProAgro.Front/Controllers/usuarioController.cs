using Newtonsoft.Json;
using ProAgro.Front.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProAgro.Front.Controllers
{
    public class usuarioController : Controller
    {
        string APIProAgro = ConfigurationManager.AppSettings["APIProAgro"];
        

        // GET: usuario
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Despliega_Usuarios()
        {
            try
            {
                RespuestaAPI_Usuario model = new RespuestaAPI_Usuario();
                Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                string api_ProAgroUsuarios = APIProAgro + "Usuarios";

                RestRequest request = new RestRequest();
                RestClient client = new RestClient();
                IRestResponse response = null;
                client = new RestClient(api_ProAgroUsuarios);         

                request.Method = Method.GET;
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");

                //Ejecuta la llamada
                response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string GetUsuariosAPI = response.Content;
                    List<Usuario> ListadeUsuarios = JsonConvert.DeserializeObject<List<Usuario>>(GetUsuariosAPI);
                    model.ListaUsuarios = ListadeUsuarios;
                }

                respuestaAPI.iCveMensaje = (int)((HttpStatusCode)response.StatusCode);

                model.RespuestaSP = respuestaAPI;

                return Json(model, JsonRequestBehavior.DenyGet);               
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.DenyGet);
            }

        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.ErrorMessage = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombre, Contrasena, RFC")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RespuestaAPI_Usuario model = new RespuestaAPI_Usuario();
                    Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                    string api_ProAgroUsuarios = APIProAgro + "Usuarios";

                    RestRequest request = new RestRequest();
                    RestClient client = new RestClient();
                    IRestResponse response = null;
                    client = new RestClient(api_ProAgroUsuarios);

                    request.Method = Method.POST;
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Content-Type", "application/json");
                  
                    string obj = JsonConvert.SerializeObject(usuario);
                    request.AddParameter("application/json", obj, ParameterType.RequestBody);

                    //Ejecuta la llamada
                    response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {                        
                        respuestaAPI.iCveMensaje = (int)((HttpStatusCode)response.StatusCode);

                        model.RespuestaSP = respuestaAPI;
                        TempData["MessageValidate"] = "Se guardo correctamente el usuario";
                        return RedirectToAction("Index", "usuario");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = (int)((HttpStatusCode)response.StatusCode);
                        return View(usuario);
                    }

                }
                catch (Exception)
                {
                  
                }
            }

            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit([Bind(Include = "idUsuario, Nombre, RFC")] Usuario usuario)
        {           
            ViewBag.ErrorMessage = "";
            return PartialView("Edit", usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int idUsuario, string Nombre, string RFC)
        {       
            Usuario usuario = new Usuario
            {
                idUsuario = idUsuario,
                Nombre = Nombre,
                RFC = RFC
            };

            if (ModelState.IsValid)
            {
                try
                {
                    RespuestaAPI_Usuario model = new RespuestaAPI_Usuario();
                    Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                    string api_ProAgroUsuarios = APIProAgro + "Usuarios/" + idUsuario;

                    RestRequest request = new RestRequest();
                    RestClient client = new RestClient();
                    IRestResponse response = null;
                    client = new RestClient(api_ProAgroUsuarios);

                    request.Method = Method.PUT;
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Content-Type", "application/json");

                    string obj = JsonConvert.SerializeObject(usuario);
                    request.AddParameter("application/json", obj, ParameterType.RequestBody);

                    //Ejecuta la llamada
                    response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        respuestaAPI.iCveMensaje = (int)((HttpStatusCode)response.StatusCode);

                        model.RespuestaSP = respuestaAPI;
                        TempData["MessageValidate"] = "Se guardo correctamente el usuario";
                        return RedirectToAction("Index", "usuario");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = (int)((HttpStatusCode)response.StatusCode);
                        return View(usuario);
                    }
                }
                catch (Exception)
                {

                }
            }
            return View(usuario);
        }


        [HttpPost]
        public JsonResult Elimina_Usuario(int? idUsuario)
        {
            try
            {
                RespuestaAPI_Usuario model = new RespuestaAPI_Usuario();
                Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                string api_ProAgroUsuarios = APIProAgro + "Usuarios/" + idUsuario;

                RestRequest request = new RestRequest();
                RestClient client = new RestClient();
                IRestResponse response = null;
                client = new RestClient(api_ProAgroUsuarios);

                request.Method = Method.DELETE;
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");

                //Ejecuta la llamada
                response = client.Execute(request);
               

                respuestaAPI.iCveMensaje = (int)((HttpStatusCode)response.StatusCode);

                model.RespuestaSP = respuestaAPI;

                return Json(model, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.DenyGet);
            }
        }


        // GET: Usuario/Edit/5
        public ActionResult UsuarioEstadoGeorreferencia(int idUsuario)
        {
            ViewBag.ErrorMessage = "";
            RespuestaAPI_GeorreferenciaMAPS model = new RespuestaAPI_GeorreferenciaMAPS();
            try
            {
                //RespuestaAPI_Permiso model = new RespuestaAPI_Permiso();
                //model.ListaPermisos = new List<Permiso>();

                Permiso  PermisoxUsuario = new Permiso();             

                Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                string api_ProAgroPermisos = APIProAgro + "Permisos/" + idUsuario;

                RestRequest request = new RestRequest();
                RestClient client = new RestClient();
                IRestResponse response = null;
                client = new RestClient(api_ProAgroPermisos);

                request.Method = Method.GET;
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");

                //Ejecuta la llamada
                response = client.Execute(request);              
              
                model.ListaGeorreferencias = new List<GeorreferenciasMaps>();

                string GetPermisosAPI = response.Content;
                PermisoxUsuario = JsonConvert.DeserializeObject<Permiso>(GetPermisosAPI);

                //--------------------------------------------

                string api_ProAgroGeorreferencias = APIProAgro + "GeorreferenciasxEstado/" + PermisoxUsuario.idEstado;

                RestRequest requestGeo = new RestRequest();
                RestClient clientGeo = new RestClient();
                IRestResponse responseGeo = null;
                clientGeo = new RestClient(api_ProAgroGeorreferencias);

                requestGeo.Method = Method.GET;
                requestGeo.AddHeader("Accept", "application/json");
                requestGeo.AddHeader("Content-Type", "application/json");


                //Ejecuta la llamada
                responseGeo = clientGeo.Execute(requestGeo);

                string GetGeoAPI = responseGeo.Content;
                List<GeorreferenciasMaps> ListadeGeorreferencias = JsonConvert.DeserializeObject<List<GeorreferenciasMaps>>(GetGeoAPI);

                model.ListaGeorreferencias = ListadeGeorreferencias;

                

                //------------------------------------------------------

                string api_ProAgroUsuario = APIProAgro + "Usuarios/" + idUsuario;

                RestRequest requestUsuario = new RestRequest();
                RestClient clientUsuario = new RestClient();
                IRestResponse responseUsuario = null;
                clientUsuario = new RestClient(api_ProAgroUsuario);

                requestUsuario.Method = Method.GET;
                requestUsuario.AddHeader("Accept", "application/json");
                requestUsuario.AddHeader("Content-Type", "application/json");

                //Ejecuta la llamada
                responseUsuario = clientUsuario.Execute(requestUsuario);

                string GetUsuariosAPI_id = responseUsuario.Content;
                Usuario UsuarioId = JsonConvert.DeserializeObject<Usuario>(GetUsuariosAPI_id);

                model.NombredelUsuario = UsuarioId.Nombre;
                //----------------------------------------------


                string api_ProAgroEstados = APIProAgro + "Estados/" + PermisoxUsuario.idEstado;

                RestRequest requestEdo = new RestRequest();
                RestClient clientEdo = new RestClient();
                IRestResponse responseEdo = null;
                clientEdo = new RestClient(api_ProAgroEstados);

                requestEdo.Method = Method.GET;
                requestEdo.AddHeader("Accept", "application/json");
                requestEdo.AddHeader("Content-Type", "application/json");

                //Ejecuta la llamada
                responseEdo = clientEdo.Execute(requestEdo);

                string GetEstadosAPI_id = responseEdo.Content;
                Estado EstadoId = JsonConvert.DeserializeObject<Estado>(GetEstadosAPI_id);

                model.NombredelEstado = EstadoId.NombreEstado;
              
               

                return PartialView("UsuarioEstadoGeorreferencia", model);
              
            }
            catch (Exception ex)
            {
                return View("UsuarioEstadoGeorreferencia", model);
                
            }
          
        }

    }
}