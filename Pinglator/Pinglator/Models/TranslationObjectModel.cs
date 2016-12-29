using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pinglator.Models
{
    public class TranslationObjectModel
    {
        public int ID { get; set; }
        public string Item { get; set; }
        public string Romanization { get; set; }
        public string Translation { get; set; }
    }
}