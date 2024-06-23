using JaminY_SMS.Models;
using JaminY_SMS.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JaminY_SMS.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Course> _courseRepository;

        public StudentsController(IRepository<Student> studentRepository,
            IRepository<Course> courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Courses = await _courseRepository.GetAllAsync(p => p.IsActive == true);

            var students = await _studentRepository.GetAllAsync(p => p.IsActive == true);
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            ViewBag.Courses =await _courseRepository.GetAllAsync(p => p.IsActive == true);
            Student student = new Student();
            if (id > 0)
            {
                student = await _studentRepository.GetAsync(id);
            }
            ViewBag.Sections = new List<SelectListItem>
            {
                new SelectListItem { Value = "A", Text = "Section A" },
                new SelectListItem { Value = "B", Text = "Section B" },
                new SelectListItem { Value = "C", Text = "Section C" },
                new SelectListItem { Value = "D", Text = "Section D" },
                new SelectListItem { Value = "E", Text = "Section E" }
            };
            return View(student);

           
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Student student)
        {
            ViewBag.Courses =await _courseRepository.GetAllAsync(p => p.IsActive == true);

            if (ModelState.IsValid)
            {
                try
                {
                    if (student.ImageFile != null)
                    {
                        string fileDirectory = "wwwroot/Images";
                        if (!Directory.Exists(fileDirectory))
                        {
                            Directory.CreateDirectory(fileDirectory);
                        }

                        string uniqueFileName = Guid.NewGuid() + "_" + student.ImageFile.FileName;
                        string filePath = Path.Combine(fileDirectory, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await student.ImageFile.CopyToAsync(fileStream);
                        }

                        student.ImageUrl = $"Images/{uniqueFileName}";
                    }

                    if (student.Id == 0)
                    {
                        await _studentRepository.InsertAsync(student);
                        TempData["success"] = "Data added successfully";
                    }
                    else
                    {
                        var orgStudent = await _studentRepository.GetAsync(student.Id);
                        orgStudent.Name = student.Name;
                        orgStudent.Email = student.Email;
                        orgStudent.PhoneNumber = student.PhoneNumber;
                        orgStudent.CourseId = student.CourseId;
                        orgStudent.Gender = student.Gender;
                        orgStudent.Address = student.Address;
                        orgStudent.Class = student.Class;
                        orgStudent.Section = student.Section;
                        orgStudent.IsActive = student.IsActive;

                        if (student.ImageFile != null)
                        {
                            orgStudent.ImageUrl = student.ImageUrl;
                        }

                        await _studentRepository.UpdateAsync(orgStudent);
                        TempData["success"] = "Data updated successfully";
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = "An error occurred, please try again";
                    return View(student);
                }
            }
            TempData["warning"] = "Please enter valid data";
            return View(student);
        }
        public async Task<IActionResult> Details(int id)
        {
            var studentInfo = await _studentRepository.GetAsync(id);
            ViewBag.CourseInfos = await _courseRepository.GetAllAsync(p => p.IsActive == true);

            return View(studentInfo);
        }


            public async Task<IActionResult> Delete(int id)
        {
            var studentInfo = await _studentRepository.GetAsync(id);
            _studentRepository.Delete(studentInfo);
            TempData["error"] = "Data Deleted Sucessfully";
            return RedirectToAction("Index");
        }
    }
}
