using ChildHDT.Domain.Entities;
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

        public async Task<Child> AssignRoleVictimToChild(Guid childId)
        {
            var child = await rc.FindById(childId);
            child.AssignRole(new Victim());
            return await rc.Update(child);
        }

        public async Task<Child> AssignRoleBullyToChild(Guid childId)
        {
            var child = await rc.FindById(childId);
            child.AssignRole(new Bully());
            return await rc.Update(child);
        }

        public async Task<Child> AssignRoleToMObserverToChild(Guid childId)
        {
            var child = await rc.FindById(childId);
            child.AssignRole(new ToMObserver());
            return await rc.Update(child);
        }

        public async Task<Child> AssignRoleNonToMObserverToChild(Guid childId)
        {
            var child = await rc.FindById(childId);
            child.AssignRole(new NonToMObserver());
            return await rc.Update(child);
        }

        public async Task<Child> DeleteRoleToChild(Guid childId)
        {
            var child = await rc.FindById(childId);
            child.AssignRole(null);
            return await rc.Update(child);
        }
    }
}
