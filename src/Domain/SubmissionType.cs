﻿using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionService.Domain
{
    public class SubmissionType
    {
        public int Id { get; set; }
        public string Type { get; set; }    
        public string Description { get; set; }
    }
}
