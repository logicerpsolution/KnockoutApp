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

namespace Mvc_Editable_App.Controllers
{
    public class TreeNodeApiController : ApiController
    {
        private ApplicationEntities db = new ApplicationEntities();

        // GET api/TreeNodeApi
        public IEnumerable<TreeView> GetTreeViews()
        {
            return db.TreeViews.AsEnumerable();
        }

        // GET api/TreeNodeApi/5
        public TreeView GetTreeView(int id)
        {
            TreeView treeview = db.TreeViews.Find(id);
            if (treeview == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return treeview;
        }

        // PUT api/TreeNodeApi/5
        public HttpResponseMessage PutTreeView(int id, TreeView treeview)
        {
            if (ModelState.IsValid && id == treeview.Id)
            {
                db.Entry(treeview).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
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

        // POST api/TreeNodeApi
        public HttpResponseMessage PostTreeView(TreeView treeview)
        {
            if (ModelState.IsValid)
            {
                db.TreeViews.Add(treeview);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, treeview);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = treeview.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/TreeNodeApi/5
        public HttpResponseMessage DeleteTreeView(int id)
        {
            TreeView treeview = db.TreeViews.Find(id);
            if (treeview == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.TreeViews.Remove(treeview);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, treeview);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}