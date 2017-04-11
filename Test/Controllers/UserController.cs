using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class UserController : Controller
    {
        public ActionResult List()
        {
            var db = new UserModel();

            var users = db.UserProfiles.ToList();

            if (!users.Any())
            {
                db.UserProfiles.Add(new UserProfile { Name = "Саша", City = "Київ", Age = 18 });
                db.UserProfiles.Add(new UserProfile { Name = "Аня", City = "Львів", Age = 25 });
                db.UserProfiles.Add(new UserProfile { Name = "Віка", City = "Харьків", Age = 22 });
                db.UserProfiles.Add(new UserProfile { Name = "Ігор", City = "Київ", Age = 40 });
                db.UserProfiles.Add(new UserProfile { Name = "Ваня", City = "Львів", Age = 16 });

                db.SaveChanges();
            }

            return View(users);
        }

        [HttpPost]
        public ActionResult SortByAge(int selection, List<UserProfile> model)
        {
            if (selection == 1)
                return PartialView("List", model.OrderByDescending(profile => profile.Age).ToList());
            else if (selection == 2)
                return PartialView("List", model.OrderBy(profile => profile.Age).ToList());
            else
            {
                var db = new UserModel();
                var users = db.UserProfiles.ToList();
                return PartialView("List", users);
            }
        }

        [HttpPost]
        public ActionResult FilterByCity(string city, List<UserProfile> model)
        {
            if (string.IsNullOrEmpty(city))
            {
                var db = new UserModel();
                var users = db.UserProfiles.ToList();
                return PartialView("List", users);
            }
            else
                return PartialView("List", model.Where(profile => profile.City == city).ToList());
        }
    }
}
