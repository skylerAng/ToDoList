using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JustDoIt.Models;
using Microsoft.AspNet.Identity;

namespace JustDoIt.Controllers
{
    public class ToDoesController : Controller
    {
        // Data access layer
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ToDoes
        public ActionResult Index()
        {
            /*
            // Get the current User
            string currentUserId = User.Identity.GetUserId();

            // Db query to find user object for the current user
            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);
            
            // Looks at current user, return only lists of current user
            return View(db.ToDos.ToList().Where(x => x.User == currentUser));
            */
            return View();
        }

        private IEnumerable<ToDo> GetMyTodoes()
        {
            // Get the current User
            string currentUserId = User.Identity.GetUserId();

            // Db query to find user object for the current user
            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);

            IEnumerable <ToDo> myToDoes = db.ToDos.ToList().Where(x => x.User == currentUser);

            // Count completed tasks
            int completeCount = 0;
            foreach(ToDo toDo in myToDoes)
            {
                if (toDo.IsDone)
                {
                    completeCount++;
                }
            }

            // Everything inside sent for View to access
            // Cast to float to avoid integer excess
            ViewBag.Percent = Math.Round(100f * ((float)completeCount / (float)myToDoes.Count()));

            return myToDoes;
        }

        //
        public ActionResult BuildToDoTable()
        {
           

            // Looks at current user, return only lists of current user
            // Returns partialview with of _ToDoTable
            return PartialView("_ToDoTable", GetMyTodoes());
        }

        // GET: ToDoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            return View(toDo);
        }

        // GET: ToDoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxCreate([Bind(Include = "Id,Description,IsDone")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                // Get current user and add to database 
                string currentUserId = User.Identity.GetUserId();

                // Grab user object from db and put a reference to ToDo that is being created
                // Look for a object where object.Id is equal to current UserId, we want first user to have that Id
                ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);

                // Get current user's identity
                toDo.User = currentUser;

                // Set default state of item task
                toDo.IsDone = false;

                // If the model is toDo, add to db context
                db.ToDos.Add(toDo);

                // Save changes and then redirecting to the index
                db.SaveChanges();

                return RedirectToAction("Index");
                
            }

            return PartialView("_ToDoTable", GetMyTodoes());
        }

        // GET: ToDoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);

            if (toDo == null)
            {
                return HttpNotFound();
            }

            // Get the current User
            string currentUserId = User.Identity.GetUserId();

            // Db query to find user object for the current user
            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);

            // Check if user exists, if does not exist, return badrequest
            if (toDo.User != currentUser){
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            return View(toDo);
        }

        // POST: ToDoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,IsDone")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDo);
        }

        public ActionResult AjaxEdit(int? id, bool value)
        {
            // If it does not return an Id, it returns a bad request error
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Then it tries to find the toDo with the id
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            else
            {
                toDo.IsDone = value;

                // Modifies the entry, put this entry to toDo
                db.Entry(toDo).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_ToDoTable", GetMyTodoes());
            }


        }

        // GET: ToDoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            return View(toDo);
        }

        // POST: ToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDo toDo = db.ToDos.Find(id);
            db.ToDos.Remove(toDo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
