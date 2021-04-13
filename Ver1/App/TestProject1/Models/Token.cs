﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject1.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string access_token { get; set; }
        public string error_description { get; set; }
        public DateTime expire_date { get; set; }
        public int expire_int { get; set; }
        public Token() { }
    }
}
