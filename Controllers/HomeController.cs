using SearchForm.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using System.Threading;
using PagedList;

namespace SearchForm.Controllers
{
	public class HomeController : Controller
	{

		FormDataEntities db = new FormDataEntities();

		[HttpGet]
		public ActionResult Index(string search, int? page)
		{
			var query = from username in db.UserDatas select username;
			if (!String.IsNullOrEmpty(search))
			{
				query = query.Where(x => x.username.ToLower().StartsWith(search.ToLower()));
			}
			//Thread.Sleep(6000);
			var data = query.OrderBy(s => s.username).AsNoTracking().ToPagedList(page?? 1, 5);
			return View(data);
		}

		[HttpPost]
		public ActionResult Index(int id)           // id is taken from Delete action method
		{
			var row = db.UserDatas.Where(model => model.userId == id).FirstOrDefault();
			if (row != null)
			{
				db.Entry(row).State = EntityState.Deleted;
				db.SaveChanges();
			}
			else
			{
				ViewBag.DeleteMessage = ("Could not delete the record!");
			}
			return RedirectToAction("Index");
		}


		[HttpGet]
		public ActionResult Delete(int id)
		{
			return Index(id);
		}

		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(UserData data)
		{
			try
			{
				data.ConfirmPassword = data.password;
				int count= db.UserDatas.Count(model => model.username == data.username && model.password == data.password);
				db.SaveChanges();
				if (count > 0)
				{
					return RedirectToAction("Index");
				}
				else
				{
					ModelState.AddModelError("LoginValidation", "Username or Password is incorrect.");
					return View("Login");
				}
			}

			catch (Exception ex)
			{
				return View(ex);
			}
		}

		public ActionResult Welcome()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(UserData data)
		{
			db.UserDatas.Add(data);
			int count = db.SaveChanges();

			if (count > 0)
			{
				return RedirectToAction("Login");
			}
			else
			{
				ViewBag.CreateMessage = ("<script> alert('Registeration is failed.') </script>");
			}

			return View();
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var row = db.UserDatas.Where(model => model.userId == id).FirstOrDefault();
			return View(row);
		}

		[HttpPost]
		public ActionResult Edit(UserData data)
		{
			var row = db.UserDatas.Where(model => model.userId == data.userId).FirstOrDefault();
			row.username = data.username;
			row.ConfirmPassword = data.password;
			row.password = data.password;
			db.Entry(row).State = EntityState.Modified;
			db.SaveChanges();
			return RedirectToAction("Login");
		}
	}
}