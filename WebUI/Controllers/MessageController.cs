using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;

namespace WebUI.Controllers
{
    public class MessageController : Controller
    {
        public MessageController()
        {
            WebAPIHost = "http://localhost:49567/";
            LoadMessages();
        }
        public void LoadMessages()
        {
            string URL = WebAPIHost + "api/message";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                Messages = JsonConvert.DeserializeObject<List<Message>>(reader.ReadToEnd());
            }
        }

        public string WebAPIHost { get; set; }
        public List<Message>  Messages { get; set; }
        // GET: MessageController
        public ActionResult Index()
        {
            return View(Messages);
        }

        // GET: MessageController/Details/5
        public ActionResult Details(int id)
        {
            Message message = Messages.Where(x => x.MessageId == id).FirstOrDefault();
            return View(message);
        }

        // GET: MessageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MessageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            Message message = new Message(collection["Text"], Convert.ToDateTime(collection["Date"]));
            message.MessageId = 0;
            try
            {
                string URL = WebAPIHost + "api/message";
                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json";
                request.Method = "POST";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(message);
                    streamWriter.Write(json);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(message);
                    }
                }
            }
            catch
            {
                return View(message);
            }
        }

        // GET: MessageController/Edit/5
        public ActionResult Edit(int id)
        {
            Message message = Messages.Where(x => x.MessageId == id).FirstOrDefault();
            return View(message);
        }

        // POST: MessageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Message message = new Message(collection["Text"], Convert.ToDateTime(collection["Date"]));
                message.MessageId = id;

                string URL = WebAPIHost + "api/message/" + id.ToString();
                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json";
                request.Method = "PUT";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(message);
                    streamWriter.Write(json);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(message);
                    }
                }
            }
            catch
            {
                return View(Messages.Where(x => x.MessageId == id).FirstOrDefault());
            }
        }

        // GET: MessageController/Delete/5
        public ActionResult Delete(int id)
        {
            Message message = Messages.Where(x => x.MessageId == id).FirstOrDefault();
            return View(message);
        }

        // POST: MessageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                string URL = WebAPIHost + "api/message/" + id.ToString();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "DELETE";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    string state = reader.ReadToEnd();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(Messages.Where(x => x.MessageId == id).FirstOrDefault());
                    }
                }
            }
            catch
            {
                return View(Messages.Where(x => x.MessageId == id).FirstOrDefault());
            }
        }
    }
}
