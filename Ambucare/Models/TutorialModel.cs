﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ambucare.Models
{
    public class TutorialModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Attachment { get; set; }
    }
}