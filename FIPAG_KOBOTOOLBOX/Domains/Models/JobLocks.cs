﻿using System;
using System.Collections.Generic;

namespace FIPAG_KOBOTOOLBOX.Domains.Models
{
    public partial class JobLocks
    {
        public string JobId { get; set; } = null!;
        public bool IsRunning { get; set; }
        public DateTime DataExec { get; set; }
    }
}
