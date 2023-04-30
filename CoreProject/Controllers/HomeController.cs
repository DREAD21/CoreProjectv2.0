using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Net;
using CoreProject.Models;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Net.Http.Headers;
using System;

namespace CoreProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static HttpClient client = new HttpClient();
   
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index1()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }
        public async Task<FileResult> Index6(string value)
        {
            //имитация работы апи для получения файла по id
            //получлии файл типа fileBytes
            //это все для монги
            byte[] product = null;
            HttpResponseMessage response = await client.GetAsync("https://localhost:7224/" + value);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsByteArrayAsync();
            }
            return File(product, "application/pdf", "11.pdf");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public string getName(string name, string type)
        {
            return type;
        }
        [HttpGet]
        public string getMaterial(MainFormData mainFormData)
        {
            return mainFormData.dropDownText;
        }
        public async Task<IActionResult> Index4(string text, string dropdown)
        {
               var model = await getWorks(text, dropdown);
               return View(model);

        }
        public async Task<IActionResult> Index5()
        {
            var model = await getAllWorks();
            return View(model);
        }
        [HttpPost]
        [Route("/Home/upload")]
        public async void uploadFile(FormData formData)
        {
            byte[] bytes = ConvertToByteArray(formData.file);
            HelpModel helpModel = new HelpModel();
            PostgreModel pModel = new PostgreModel();
            var names = HelpFunctions.HelpFunctions.getName(formData.name);
            var tutorNames = HelpFunctions.HelpFunctions.getName(formData.tutorName);
            pModel.AcademicDegree = formData.academicDegree;
            pModel.AcademicTitle = formData.academicTitle;
            pModel.CathedraName = helpModel.cathedraName;
            pModel.CathedraNumber = helpModel.cathedraNumber;
            pModel.FacultyName = helpModel.facultyName;
            pModel.FacultyNumber = helpModel.facultyNumber;
            pModel.StudentGroup = formData.group;
            pModel.StudentName = names[1];
            pModel.StudentPatronymic = names[2];
            pModel.StudentSurname = names[0];
            pModel.TCathedraName = helpModel.cathedraNameTutor;
            pModel.TCathedraNumber = helpModel.cathedraNumberTutor;
            pModel.Title = formData.title;
            pModel.TutorName = tutorNames[1];
            pModel.TutorPatronymic = tutorNames[2];
            pModel.TutorSurname = tutorNames[0];
            pModel.UploadDate = DateOnly.FromDateTime(DateTime.Now);

            var content = new StringContent(
                    JsonConvert.SerializeObject(pModel),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json);

            HttpResponseMessage response = await client.PostAsync("https://localhost:7129/Postgre", content);

            //HttpResponseMessage responseMongo = await client.PostAsync("https://localhost:7224/post?key" + getAllWorksCount().ToString(), content); //апишка должна быть другой
            string url = "https://localhost:7129/Postgre/getAll";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string response22;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response22 = streamReader.ReadToEnd();
            }


            var workResponse = JsonConvert.DeserializeObject<List<workModel>>(response22);

            int size = workResponse.Count;

            using (var httpClient = new HttpClient())
            {   
                using (var form = new MultipartFormDataContent())
                {
                    using (var fileContent = new ByteArrayContent(bytes))
                    {
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                        form.Add(fileContent, "file", "232");      
                        var response223343 = await httpClient.PostAsync("https://localhost:7224/post?key=" + size, form);
                        response.EnsureSuccessStatusCode();
                    }
                }
            }
            //var responseFromPostgre = new UploadFileToPostgre(formData).uploadFileToPostgre().ToString();
            //var responseFromMongo = new UploadFileToMongo(formData).uploadFileToMongo().ToString();
            if (response.IsSuccessStatusCode)
            {
                await Index5();
            }

            /*
            var names = HelpFunctions.HelpFunctions.getName(formData.name); //Хранится имя, фамилия, отчество
            string name = "12";
            IFormFileCollection files = Request.Form.Files;
            // путь к папке, где будут храниться файлы
            var uploadPath = "C:\\Users\\Никита\\Desktop\\test222";
            byte[] fileBytes = System.IO.File.ReadAllBytes("C:\\Users\\Никита\\Desktop\\c#\\Краснов Никита.pdf");
            // создаем папку для хранения файлов
            Directory.CreateDirectory(uploadPath);
            if (formData.name != null) 
            {
                string fullPath = $"{uploadPath}/{formData.file.FileName}";
                name = "20.pdf";
                // сохраняем файл в папку uploads
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await formData.file.CopyToAsync(fileStream);
                }
                //Все лишнее, просто передаем файл в монгу
                //Передаем данные в постгрес
            }
            return File(fileBytes, "application/pdf",name);
            */

        }
        public byte[] ConvertToByteArray(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
        public async Task<List<workModel>> getWorks(string text, string dropdown)
        {
            string url = "https://localhost:7129/Postgre/getInfo?info=" + dropdown +"&text=" + text;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }
            var workResponse = JsonConvert.DeserializeObject<List<workModel>>(response);
            return workResponse;
        }
        public async Task<List<workModel>> getAllWorks()
        {
            string url = "https://localhost:7129/Postgre/getAll";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }
            var workResponse = JsonConvert.DeserializeObject<List<workModel>>(response);
            return workResponse;
        }
    }
}