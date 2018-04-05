﻿using PaymentContext.Shared.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public CommandResult() { }
        public CommandResult(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
    }
}
