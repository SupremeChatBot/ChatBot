﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Entities
{
    public class Message
    {
        [Key]        
        public int Id { get; set; } 
    }
}
