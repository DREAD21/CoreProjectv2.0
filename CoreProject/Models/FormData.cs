using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CoreProject.Models
{
    public class FormData
    {
        public string name { get; set; }
        public string title { get; set; }
        public string group { get; set; }
        public string tutorName { get; set; }
        public string academicDegree { get; set; }
        public string academicTitle { get; set; }
        public IFormFile file { get; set; }
    }
}