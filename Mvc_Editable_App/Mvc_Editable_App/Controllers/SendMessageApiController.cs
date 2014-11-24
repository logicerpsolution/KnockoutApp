using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Mvc_Editable_App.Models;

namespace Mvc_Editable_App.Controllers
{
    public class SendMessageApiController : ApiController
    {
        private ApplicationEntities1 sendMsg = new ApplicationEntities1();

        // GET api/SendMessageApi
        public IEnumerable<sendMessage> GetsendMessages()
        {
            return sendMsg.sendMessages.AsEnumerable();
        }

        // GET api/SendMessageApi/5
        public sendMessage GetsendMessage(int id)
        {
            sendMessage sendmessage = sendMsg.sendMessages.Find(id);
            if (sendmessage == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return sendmessage;
        }

        // PUT api/SendMessageApi/5
        public HttpResponseMessage PutsendMessage(int id, sendMessage sendmessage)
        {
            if (ModelState.IsValid && id == sendmessage.Id)
            {
                sendMsg.Entry(sendmessage).State = EntityState.Modified;

                try
                {
                    sendMsg.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/SendMessageApi
        public HttpResponseMessage PostsendMessage(sendMessage sendmessage)
        {
            if (ModelState.IsValid)
            {
                sendMsg.sendMessages.Add(sendmessage);
                sendMsg.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, sendmessage);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = sendmessage.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/SendMessageApi/5
        public HttpResponseMessage DeletesendMessage(int id)
        {
            sendMessage sendmessage = sendMsg.sendMessages.Find(id);
            if (sendmessage == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            sendMsg.sendMessages.Remove(sendmessage);

            try
            {
                sendMsg.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, sendmessage);
        }

        protected override void Dispose(bool disposing)
        {
            sendMsg.Dispose();
            base.Dispose(disposing);
        }
    }
}