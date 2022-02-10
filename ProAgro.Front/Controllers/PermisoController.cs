using Newtonsoft.Json;
using ProAgro.Front.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProAgro.Front.Controllers
{
    public class PermisoController : Controller
    {
        string APIProAgro = ConfigurationManager.AppSettings["APIProAgro"];
        // GET: Permiso
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Despliega_Permisos()
        {
            try
            {
                RespuestaAPI_Permiso model = new RespuestaAPI_Permiso();
                Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                string api_ProAgroPermisos = APIProAgro + "Permisos";

                model.EstadoUsuarioById = new List<EstadoUsuariosModel>();

                List<Permiso> ListadePermisos = new List<Permiso>();

                RestRequest request = new RestRequest();
                RestClient client = new RestClient();
                IRestResponse response = null;
                client = new RestClient(api_ProAgroPermisos);

                request.Method = Method.GET;
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");

                //Ejecuta la llamada
                response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string GetPermisosAPI = response.Content;
                    ListadePermisos = JsonConvert.DeserializeObject<List<Permiso>>(GetPermisosAPI);  
                    
                    model.ListaPermisos = ListadePermisos;
                }

                foreach (Permiso permiso in ListadePermisos)
                {
                    string api_ProAgroEstado = APIProAgro + "Estados/" + permiso.idEstado;

                    RestRequest requestEstado = new RestRequest();
                    RestClient clientEstado = new RestClient();
                    IRestResponse responseEstado = null;
                    clientEstado = new RestClient(api_ProAgroEstado);

                    requestEstado.Method = Method.GET;
                    requestEstado.AddHeader("Accept", "application/json");
                    requestEstado.AddHeader("Content-Type", "application/json");

                    //Ejecuta la llamada
                    responseEstado = clientEstado.Execute(requestEstado);

                    string GetEstadosAPI_id = responseEstado.Content;
                    Estado EstadoId = JsonConvert.DeserializeObject<Estado>(GetEstadosAPI_id);

                    EstadoUsuariosModel edousuario = new EstadoUsuariosModel();
                    edousuario.idEstado = permiso.idEstado;
                    edousuario.NombreEstado = EstadoId.NombreEstado;

                  
                    //------------------------------------------
                    string api_ProAgroUsuario = APIProAgro + "Usuarios/" + permiso.idUsuario;

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
                   
                    edousuario.idUsuario = permiso.idUsuario;
                    edousuario.NombreUsuario = UsuarioId.Nombre;

                    model.EstadoUsuarioById.Add(edousuario);

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


        // GET: PErmisos/Create
        public ActionResult Create()
        {
            ViewBag.ErrorMessage = "";
            CreatePermisoViewModels model = new CreatePermisoViewModels();

            try
            {
                Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                string api_ProAgroUsuarios = APIProAgro + "Usuarios";
                string api_ProAgroEstados = APIProAgro + "Estados";

                RestRequest request = new RestRequest();
                RestClient client = new RestClient();
                IRestResponse response = null;
                client = new RestClient(api_ProAgroEstados);

                request.Method = Method.GET;
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");

                //Ejecuta la llamada
                response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string GetEstadosAPI = response.Content;
                    List<Estado> ListadeEstados = JsonConvert.DeserializeObject<List<Estado>>(GetEstadosAPI);
                    model.ListaEstados = ListadeEstados;
                }

                RestRequest requestUsuario = new RestRequest();
                RestClient clientUsuario = new RestClient();
                IRestResponse responseUsuario = null;
                clientUsuario = new RestClient(api_ProAgroUsuarios);

                requestUsuario.Method = Method.GET;
                requestUsuario.AddHeader("Accept", "application/json");
                requestUsuario.AddHeader("Content-Type", "application/json");

                //Ejecuta la llamada
                responseUsuario = clientUsuario.Execute(requestUsuario);
                if (responseUsuario.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string GetUsuariosAPI = responseUsuario.Content;
                    List<Usuario> ListadeUsuarios = JsonConvert.DeserializeObject<List<Usuario>>(GetUsuariosAPI);
                    model.ListaUsuarios = ListadeUsuarios;
                }
            }
            catch (Exception)
            {

            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            Permiso permisomodel = new Permiso
            {
                idEstado = int.Parse(form["idEstado"].ToString()),
                idUsuario = int.Parse(form["idUsuario"].ToString())
            };

            if (ModelState.IsValid)
            {
                try
                {
                    RespuestaAPI_Permiso model = new RespuestaAPI_Permiso();
                    Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                    string api_ProAgroPermiso = APIProAgro + "Permisos";

                    RestRequest request = new RestRequest();
                    RestClient client = new RestClient();
                    IRestResponse response = null;
                    client = new RestClient(api_ProAgroPermiso);

                    request.Method = Method.POST;
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Content-Type", "application/json");

                    string obj = JsonConvert.SerializeObject(permisomodel);
                    request.AddParameter("application/json", obj, ParameterType.RequestBody);

                    //Ejecuta la llamada
                    response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        respuestaAPI.iCveMensaje = (int)((HttpStatusCode)response.StatusCode);

                        model.RespuestaSP = respuestaAPI;
                        TempData["MessageValidate"] = "Se guardo correctamente el permiso";
                        return RedirectToAction("Index", "Permiso");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = (int)((HttpStatusCode)response.StatusCode);
                        return View(permisomodel);
                    }
                }
                catch (Exception)
                {

                }
            }
            return View(permisomodel);
        }


        [HttpPost]
        public JsonResult Elimina_Permiso(int idEstado, int idUsuario)
        {
            try
            {
                Permiso permisomodel = new Permiso
                {
                    idEstado = idEstado,
                    idUsuario = idUsuario
                };

                RespuestaAPI_Permiso model = new RespuestaAPI_Permiso();
                Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                string api_ProAgroPermiso = APIProAgro + "Permisos";

                RestRequest request = new RestRequest();
                RestClient client = new RestClient();
                IRestResponse response = null;
                client = new RestClient(api_ProAgroPermiso);

                request.Method = Method.DELETE;
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");

                string obj = JsonConvert.SerializeObject(permisomodel);
                request.AddParameter("application/json", obj, ParameterType.RequestBody);

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

    }
}