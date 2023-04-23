using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models
{
    public class HelpModel
    {
        private string _cathedraName = "Математическая кибернетика";
        private string _cathedraNumber = "805";
        private string _facultyName = "Компьютерные науки и прикладная математика";
        private string _facultyNumber = "8";
        public string cathedraName
        {
            get
            {
                return _cathedraName;
            }
            private set { }
        }
        public string cathedraNumber
        {
            get
            {
                return _cathedraNumber;
            }
            private set { }
        }
        public string facultyName
        {
            get
            {
                return _facultyName;
            }
            private set { }
        }
        public string facultyNumber
        {
            get
            {
                return _facultyNumber;
            }
            private set { }
        }
        public string cathedraNameTutor
        {
            get
            {
                return _cathedraName;
            }
            private set { }
        }
        public string cathedraNumberTutor
        {
            get
            {
                return _cathedraNumber;
            }
            private set { }
        }
        public DateTime date
        {
            get
            {
                return DateTime.Now;
            }
            private set { }
        }
    }
}