﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Dtos.Configuration
{
    public class Menu
    {
        public string Name { get; set; }
        public List<Action> Actions { get; set; } = new();
    }
}
