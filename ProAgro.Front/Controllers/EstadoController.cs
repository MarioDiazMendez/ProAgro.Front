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
    public class EstadoController : Controller
    {
        string APIProAgro = ConfigurationManager.AppSettings["APIProAgro"];
        // GET: Estado
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Despliega_Estados()
        {
            try
            {
                RespuestaAPI_Estado model = new RespuestaAPI_Estado();
                Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
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
        public ActionResult Create([Bind(Include = "NombreEstado, Abreviatura")] Estado estado)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RespuestaAPI_Estado model = new RespuestaAPI_Estado();
                    Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                    string api_ProAgroEstados = APIProAgro + "Estados";

                    RestRequest request = new RestRequest();
                    RestClient client = new RestClient();
                    IRestResponse response = null;
                    client = new RestClient(api_ProAgroEstados);

                    request.Method = Method.POST;
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Content-Type", "application/json");

                    string obj = JsonConvert.SerializeObject(estado);
                    request.AddParameter("application/json", obj, ParameterType.RequestBody);

                    //Ejecuta la llamada
                    response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        respuestaAPI.iCveMensaje = (int)((HttpStatusCode)response.StatusCode);

                        model.RespuestaSP = respuestaAPI;
                        TempData["MessageValidate"] = "Se guardo correctamente el estado";
                        return RedirectToAction("Index", "estado");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = (int)((HttpStatusCode)response.StatusCode);
                        return View(estado);
                    }

                }
                catch (Exception)
                {

                }
            }

            return View(estado);
        }


        // GET: Estado/Edit/5
        public ActionResult Edit([Bind(Include = "idEstado, NombreEstado, Abreviatura")] Estado estado)
        {
            ViewBag.ErrorMessage = "";
            return PartialView("Edit", estado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int idEstado, string NombreEstado, string Abreviatura)
        {
            Estado estado = new Estado
            {
                idEstado = idEstado,
                NombreEstado = NombreEstado,
                Abreviatura = Abreviatura
            };

            if (ModelState.IsValid)
            {
                try
                {
                    RespuestaAPI_Estado model = new RespuestaAPI_Estado();
                    Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                    string api_ProAgroEstados = APIProAgro + "Estados/" + idEstado;                    

                    RestRequest request = new RestRequest();
                    RestClient client = new RestClient();
                    IRestResponse response = null;
                    client = new RestClient(api_ProAgroEstados);

                    request.Method = Method.PUT;
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Content-Type", "application/json");

                    string obj = JsonConvert.SerializeObject(estado);
                    request.AddParameter("application/json", obj, ParameterType.RequestBody);

                    //Ejecuta la llamada
                    response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        respuestaAPI.iCveMensaje = (int)((HttpStatusCode)response.StatusCode);

                        model.RespuestaSP = respuestaAPI;
                        TempData["MessageValidate"] = "Se guardo correctamente el estado";
                        return RedirectToAction("Index", "estado");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = (int)((HttpStatusCode)response.StatusCode);
                        return View(estado);
                    }
                }
                catch (Exception)
                {

                }
            }
            return View(estado);
        }


        [HttpPost]
        public JsonResult Elimina_Estado(int? idEstado)
        {
            try
            {
                RespuestaAPI_Estado model = new RespuestaAPI_Estado();
                Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                string api_ProAgroEstados = APIProAgro + "Estados/" + idEstado;              

                RestRequest request = new RestRequest();
                RestClient client = new RestClient();
                IRestResponse response = null;
                client = new RestClient(api_ProAgroEstados);

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
    }
}