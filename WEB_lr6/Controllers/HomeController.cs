using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEB_lr6.Models;
using System.Text.Json;


namespace WEB_lr6.Controllers
{
    public class HomeController : Controller
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public IActionResult SaveFile(string filename, int order)
        {
            if (string.IsNullOrWhiteSpace(filename) || order < 1)
            {
                return BadRequest("Invalid input data");
            }

            string appDataPath = Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data");

            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            string filePath = Path.Combine(appDataPath, "data.json");

            var fileData = new Dictionary<int, string>
        {
            { order, filename }
        };

            string json = JsonSerializer.Serialize(fileData);

            System.IO.File.AppendAllText(filePath, json + "\n");

            ViewBag.Message = "Dish added!";

            return View("Index");
        }

        [HttpPost]
        public IActionResult ClearData()
        {
            string appDataPath = Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data");
            string filePath = Path.Combine(appDataPath, "data.json");

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            ViewBag.Message = "Data cleared!";

            return View("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Deserts()
        {
            return View();
        }
        public IActionResult FirstCourses()
        {
            return View();
        }
        public IActionResult SecondCourses()
        {
            return View();
        }
        public IActionResult SideDishes()
        {
            return View();
        }

        public IActionResult Carousel()
        {
            string appDataPath = Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data");
            string filePath = Path.Combine(appDataPath, "data.json");

            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);

                var savedData = new List<Dictionary<int, string>>();
                var lines = json.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in lines)
                {
                    var fileData = JsonSerializer.Deserialize<Dictionary<int, string>>(line);
                    savedData.Add(fileData);
                }

                // Flatten the list of dictionaries and order by key
                var orderedValues = savedData
                    .SelectMany(dict => dict)
                    .OrderBy(kvp => kvp.Key)
                    .Select(kvp => kvp.Value)
                    .ToList();

                ViewBag.SavedData = orderedValues;
            }
            else
            {
                ViewBag.SavedData = new List<string>();
            }

            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
