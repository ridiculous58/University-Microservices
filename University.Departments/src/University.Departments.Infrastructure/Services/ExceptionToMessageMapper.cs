﻿using System;
using BuildingBlocks.CQRS.Events;
using BuildingBlocks.Exception;

namespace University.Departments.Infrastructure.Services
{
    public class ExceptionToMessageMapper: IExceptionToMessageMapper
    {
        public IRejectedEvent Map(Exception exception, object message)
        => exception switch
            {
            _ => null
            };
    }
}