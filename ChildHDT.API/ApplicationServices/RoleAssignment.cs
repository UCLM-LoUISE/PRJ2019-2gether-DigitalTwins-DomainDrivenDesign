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

        public async void AssignRoleVictimToChild(Guid childId)
        {
            var child = await rc.FindById(childId);
            child.AssignRole(new Victim());
        }

        public async void AssignRoleBullyToChild(Guid childId)
        {
            var child = await rc.FindById(childId);
            child.AssignRole(new Bully());
        }

        public async void AssignRoleToMObserverToChild(Guid childId)
        {
            var child = await rc.FindById(childId);
            child.AssignRole(new ToMObserver());
        }

        public async void AssignRoleNonToMObserverToChild(Guid childId)
        {
            var child = await rc.FindById(childId);
            child.AssignRole(new NonToMObserver());
        }

        public async void DeleteRoleToChild(Guid childId)
        {
            var child = await rc.FindById(childId);
            child.AssignRole(null);
        }
    }
}
