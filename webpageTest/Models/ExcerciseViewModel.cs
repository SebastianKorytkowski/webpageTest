using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webpageTest.Models
{
    public class ExcerciseViewModel
    {
        public Excercise Excercise { get; set; }

        public IEnumerable<SelectListItem> AllExcerciseTypeSelectList { get; set; }
    }
}