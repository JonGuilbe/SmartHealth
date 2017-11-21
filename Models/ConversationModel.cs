using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHealth.Models
{
    public class ConversationModel
    {
        public Boolean IsDoctor { get; set; }
        public IQueryable<Message> Messages { get; set; }

        public string text { get; set; }


    }
}