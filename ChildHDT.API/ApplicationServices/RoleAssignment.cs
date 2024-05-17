using ChildHDT.Domain.ValueObjects;
using ChildHDT.Infrastructure.InfrastructureServices;

namespace ChildHDT.API.ApplicationServices
{
    public class RoleAssignment
    {
        // ATTRIBUTES
        private RepositoryChild rc;

        // METHODS
        public RoleAssignment(RepositoryChild rc) 
        {
            this.rc = rc;
        }

        public void AssignRoleVictimToChild(Guid childId)
        {
            var child = rc.FindById(childId);
            child.AssignRole(new Victim());
        }

        public void AssignRoleBullyToChild(Guid childId)
        {
            var child = rc.FindById(childId);
            child.AssignRole(new Bully());
        }

        public void AssignRoleToMObserverToChild(Guid childId)
        {
            var child = rc.FindById(childId);
            child.AssignRole(new ToMObserver());
        }

        public void AssignRoleNonToMObserverToChild(Guid childId)
        {
            var child = rc.FindById(childId);
            child.AssignRole(new NonToMObserver());
        }

        public void DeleteRoleToChild(Guid childId)
        {
            var child = rc.FindById(childId);
            child.AssignRole(null);
        }
    }
}
