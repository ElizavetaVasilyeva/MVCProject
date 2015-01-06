﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.DTO
{
    public class DalComment:IEntity
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }

        public int PhotoId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
