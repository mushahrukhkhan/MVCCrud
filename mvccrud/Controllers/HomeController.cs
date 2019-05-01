using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCrud.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SubmitStudentInfo(Models.StudentInfoModel studentInfoModel)
        {

            try
            {

                DAL.MVCCRUDEntities3 entity = new DAL.MVCCRUDEntities3();
                DAL.StudentInfo inf = new DAL.StudentInfo();

                inf.Name = studentInfoModel.Name;
                inf.ClassId = studentInfoModel.ClassId;
                inf.Contact = studentInfoModel.Contact;
                inf.CourseId = studentInfoModel.CourseId;
                inf.Code = studentInfoModel.Code;
                inf.IsActive = true;
                inf.CreatedDate = DateTime.Now;
                inf.Id = studentInfoModel.Id;
                entity.Entry(inf).State = studentInfoModel.Id == 0 ? EntityState.Added : EntityState.Modified;

                entity.SaveChanges();

               
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("StudentInfo");

        }

        public ActionResult StudentInfo()
        {
            DAL.MVCCRUDEntities3 entity = new DAL.MVCCRUDEntities3();
            
            ViewBag.Course = entity.Courses.ToList() ;
            ViewBag.Class = entity.Classes.ToList();
            return View(new Models.StudentInfoModel());
        }
        public JsonResult StudentData() {

            DAL.MVCCRUDEntities3 entity = new DAL.MVCCRUDEntities3();

            var result = (from ep in entity.StudentInfoes
                          join c in entity.Classes on ep.ClassId equals c.Id
                          join r in entity.Courses on ep.CourseId equals r.Id
                          where ep.IsActive==true
                          select new Models.StudentInfoModel()
                          {
                              Id=ep.Id,
                              ClassName=c.Name,
                              CourseName=r.Name,
                              Contact=ep.Contact,
                              Name=ep.Name,
                              Code=ep.Code,
                          }).ToList();
                          
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StudentRemove(Int32 id)
        {
            try
            {
                DAL.MVCCRUDEntities3 entity = new DAL.MVCCRUDEntities3();
                DAL.StudentInfo students = entity.StudentInfoes.Where(w => w.Id == id).FirstOrDefault();
                students.IsActive = false;
                entity.StudentInfoes.Add(students);
                entity.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
                return Json("Delete Sucessfully", JsonRequestBehavior.AllowGet);
        }
        public JsonResult StudentEdit(Int32 Id)
        {
            DAL.MVCCRUDEntities3 entity = new DAL.MVCCRUDEntities3();
            var result = entity.StudentInfoes.Where(w => w.Id == Id).Select(s=>new Models.StudentInfoModel()
                                                                            {
                                                                                Id=s.Id,
                                                                                Code=s.Code,
                                                                                Contact=s.Contact,
                                                                                Name=s.Name,
                                                                                ClassId=s.ClassId,
                                                                                CourseId=s.CourseId
                                                                            }).FirstOrDefault();
            

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}