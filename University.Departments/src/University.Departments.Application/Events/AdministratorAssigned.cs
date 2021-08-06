﻿using System;
using BuildingBlocks.CQRS.Events;

namespace University.Departments.Application.Events
{
    public class AdministratorAssigned: IEvent
    {
        public Guid InstructorId { get;}
        public Guid DepartmentId { get;}

        public AdministratorAssigned(Guid instructorId, Guid departmentId)
        {
            InstructorId = instructorId;
            DepartmentId = departmentId;
        }
    }
}