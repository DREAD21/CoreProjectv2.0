using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models
{
    public class workModel
    {
        public int ThesisId { get; set; }
        public string Title { get; set; }
        public DateTime? UploadDate { get; set; }
        public string StudentSurname { get; set; }
        public string StudentName { get; set; }
        public string StudentPatronymic { get; set; }
        public string StudentGroup { get; set; }
        public string CathedraNumber { get; set; }
        public string CathedraName { get; set; }
        public string FacultyNumber { get; set; }
        public string FacultyName { get; set; }
        public string TutorSurname { get; set; }
        public string TutorName { get; set; }
        public string TutorPatronymic { get; set; }
        public string TCathedraNumber { get; set; }
        public string TCathedraName { get; set; }
        public string AcademicDegree { get; set; }
        public string AcademicTitle { get; set; }
    }
}