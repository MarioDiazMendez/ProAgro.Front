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
    public class GeorreferenciaController : Controller
    {
        string APIProAgro = ConfigurationManager.AppSettings["APIProAgro"];
        // GET: Georreferencia
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Despliega_Georreferencia()
        {
            try
            {
                RespuestaAPI_Georreferencia model = new RespuestaAPI_Georreferencia();
                Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                string api_ProAgroGeorreferencias = APIProAgro + "Georreferencias";

                RestRequest request = new RestRequest();
                RestClient client = new RestClient();
                IRestResponse response = null;
                client = new RestClient(api_ProAgroGeorreferencias);

                request.Method = Method.GET;
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");

                //Ejecuta la llamada
                response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string GetGeorreferenciasAPI = response.Content;
                    List<Georreferencias> ListadeGeorreferencias = JsonConvert.DeserializeObject<List<Georreferencias>>(GetGeorreferenciasAPI);
                    model.ListaGeorreferencias = ListadeGeorreferencias;
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

        // GET: Georreferencia/Create
        public ActionResult Create()
        {
            ViewBag.ErrorMessage = "";
            CreateGeorrefenciaViewModels model = new CreateGeorrefenciaViewModels();

            try
            {                
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
            //string d = (form["idEstado"].ToString());
            //string a= (form["EstadoSelected"].ToString());
            //string b = form["Georreferencia.Latitud"].ToString();
            //string c = form["Georreferencia.Longitud"].ToString();

            Georreferencias georreferencia = new Georreferencias
            {
                idEstado = int.Parse(form["idEstado"].ToString()),
                Latitud = form["Georreferencia.Latitud"].ToString(),
                Longitud = form["Georreferencia.Longitud"].ToString()
            };


            if (ModelState.IsValid)
            {
                try
                {
                    RespuestaAPI_Georreferencia model = new RespuestaAPI_Georreferencia();
                    Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                    string api_ProAgroGeorreferencias = APIProAgro + "Georreferencias";                   

                    RestRequest request = new RestRequest();
                    RestClient client = new RestClient();
                    IRestResponse response = null;
                    client = new RestClient(api_ProAgroGeorreferencias);

                    request.Method = Method.POST;
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Content-Type", "application/json");

                    string obj = JsonConvert.SerializeObject(georreferencia);
                    request.AddParameter("application/json", obj, ParameterType.RequestBody);

                    //Ejecuta la llamada
                    response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        respuestaAPI.iCveMensaje = (int)((HttpStatusCode)response.StatusCode);

                        model.RespuestaSP = respuestaAPI;
                        TempData["MessageValidate"] = "Se guardo correctamente la georreferencia";
                        return RedirectToAction("Index", "Georreferencia");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = (int)((HttpStatusCode)response.StatusCode);
                        return View(georreferencia);
                    }

                }
                catch (Exception)
                {

                }
            }

            return View(georreferencia);
        }


        // GET: Georreferencia/Edit/5
        public ActionResult Edit([Bind(Include = "idGeorreferencia, idEstado, Latitud, Longitud")] Georreferencias georreferencia)
        {
            ViewBag.ErrorMessage = "";
            CreateGeorrefenciaViewModels model = new CreateGeorrefenciaViewModels();
            model.Georreferencia = new Georreferencias();
            model.Georreferencia.idEstado = georreferencia.idEstado;
            model.Georreferencia.idGeorreferencia = georreferencia.idGeorreferencia;
            model.Georreferencia.Latitud = georreferencia.Latitud;
            model.Georreferencia.Longitud = georreferencia.Longitud;

            try
            {
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
            }
            catch (Exception)
            {

            }


            return PartialView("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection form)
        {
            //string a = (form["Georreferencia.idGeorreferencia"].ToString());
            //string b = form["idEstado"].ToString();
            //string c = form["Georreferencia.Latitud"].ToString();
            //string d = form["Georreferencia.Longitud"].ToString();

            Georreferencias georreferencia = new Georreferencias
            {
                idGeorreferencia = int.Parse(form["Georreferencia.idGeorreferencia"].ToString()),
                idEstado = int.Parse(form["idEstado"].ToString()),
                Latitud = form["Georreferencia.Latitud"].ToString(),
                Longitud = form["Georreferencia.Longitud"].ToString()
            };

            //Georreferencias georreferencia = new Georreferencias
            //{
            //    idGeorreferencia = idGeorreferencia,
            //    Latitud = Latitud,
            //    Longitud = Longitud
            //};

            if (ModelState.IsValid)
            {
                try
                {
                    RespuestaAPI_Georreferencia model = new RespuestaAPI_Georreferencia();
                    Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                    string api_ProAgroGeorreferencias = APIProAgro + "Georreferencias/" + int.Parse(form["Georreferencia.idGeorreferencia"].ToString());                   

                    RestRequest request = new RestRequest();
                    RestClient client = new RestClient();
                    IRestResponse response = null;
                    client = new RestClient(api_ProAgroGeorreferencias);

                    request.Method = Method.PUT;
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Content-Type", "application/json");

                    string obj = JsonConvert.SerializeObject(georreferencia);
                    request.AddParameter("application/json", obj, ParameterType.RequestBody);

                    //Ejecuta la llamada
                    response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        respuestaAPI.iCveMensaje = (int)((HttpStatusCode)response.StatusCode);

                        model.RespuestaSP = respuestaAPI;
                        TempData["MessageValidate"] = "Se guardo correctamente la georreferencia";
                        return RedirectToAction("Index", "georreferencia");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = (int)((HttpStatusCode)response.StatusCode);
                        return View(georreferencia);
                    }
                }
                catch (Exception)
                {

                }
            }
            return View(georreferencia);
        }

        [HttpPost]
        public JsonResult Elimina_Georreferencia(int? idGeorreferencia)
        {
            try
            {
                RespuestaAPI_Georreferencia model = new RespuestaAPI_Georreferencia();
                Respuesta_BackEnd respuestaAPI = new Respuesta_BackEnd();
                string api_ProAgroGeorreferencias = APIProAgro + "Georreferencias/" + idGeorreferencia;               

                RestRequest request = new RestRequest();
                RestClient client = new RestClient();
                IRestResponse response = null;
                client = new RestClient(api_ProAgroGeorreferencias);

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