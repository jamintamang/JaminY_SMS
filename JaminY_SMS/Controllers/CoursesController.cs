using JaminY_SMS.Models;
using JaminY_SMS.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace JaminY_SMS.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IRepository<Course> _courseRepository;

        public CoursesController(IRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetAllAsync();
            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            Course course;
            if (id > 0)
            {
                course = await _courseRepository.GetAsync(id);
                if (course == null)
                {
                    return NotFound(); // or handle not found case
                }
            }
            else
            {
                course = new Course();
                course.IsActive = true;
            }
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (course.Id == 0)
                    {
                        await _courseRepository.InsertAsync(course);
                        TempData["Success"] = "Data Added Successfully";
                    }
                    else
                    {
                        var existingCourse = await _courseRepository.GetAsync(course.Id);
                        if (existingCourse == null)
                        {
                            return NotFound(); // or handle not found case
                        }
                        existingCourse.CourseName = course.CourseName;
                        existingCourse.Description = course.Description;
                        existingCourse.IsActive = course.IsActive;
                        await _courseRepository.UpdateAsync(existingCourse);
                        TempData["Success"] = "Data Updated Successfully";
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred: " + ex.Message;
            }

            TempData["Warning"] = "Please enter valid data.";
            return View(course);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var course = await _courseRepository.GetAsync(id);
                if (course != null)
                {
                    _courseRepository.Delete(course);
                    TempData["Success"] = "Data Deleted Successfully";
                }
                else
                {
                    TempData["Error"] = "Course not found.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
