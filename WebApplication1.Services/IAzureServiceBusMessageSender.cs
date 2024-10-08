﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public interface IAzureServiceBusMessageSender
    {
        Task SendBusMessageAsync(IOperationContext context, string message, string topicName);
    }
}
