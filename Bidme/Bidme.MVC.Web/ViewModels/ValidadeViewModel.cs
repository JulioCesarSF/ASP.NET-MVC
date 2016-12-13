using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bidme.MVC.Web.ViewModels
{
    public class ValidadeViewModel
    {        
        public DateTime DataValidade { get; set; }
        public int ValidadeDias { get; set; }
        public DateTime DataInicio { get; set; }
    }
}