﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Services
{
    public interface IMailService
    {
        Task SendMailAsync(string to, string subject, string body);
    }
}
