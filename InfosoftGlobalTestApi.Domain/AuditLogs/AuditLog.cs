using InfosoftGlobalTestApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace InfosoftGlobalTestApi.Domain.AuditLogs
{
    public class AuditLog : Entity , IAggregateRoot
    {
        public Guid Id { get; private set; }
        public Guid EntityId { get; private set; } 
        public string Action { get; private set; }
        public DateTime CreatedAt { get; private set; }


        private AuditLog () { }

        private AuditLog(Guid id , Guid entityId , string action)
        {
            Id = id;
            EntityId = entityId;
            Action = action;
            CreatedAt = DateTime.UtcNow;
        }

        public static AuditLog Create(Guid entityId , string action)
        {
            if (string.IsNullOrWhiteSpace(action))
                throw new ArgumentNullException("Name must be filled");

            return new AuditLog(Guid.NewGuid() , entityId , action);
        }
    }
}
