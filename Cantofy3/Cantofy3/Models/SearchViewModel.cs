using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cantofy3.Models
{
    public class SearchViewModel
    {
        public int SearchID { get; set; }
        public string Items { get; set; }
        public string Romanization { get; set; }
        public string Translation { get; set; }
        public DateTime Date { get; set; }
    }
}