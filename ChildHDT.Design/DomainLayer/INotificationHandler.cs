﻿using ChildHDT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Domain.DomainServices
{
    public interface INotificationHandler
    { 
        public void SendHelpMessage(Child child);
        public void EncouragingMessage(Child child);
        public void AdviceMessage(Child child);
        public void StressManagementMessage(Child child);
    }
}
