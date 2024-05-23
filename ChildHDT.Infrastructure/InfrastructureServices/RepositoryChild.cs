using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChildHDT.Domain.Entities;
using ChildHDT.Infrastructure.InfrastructureServices.Context;
using ChildHDT.Infrastructure.IntegrationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace ChildHDT.Infrastructure.InfrastructureServices
{
    public class RepositoryChild : ControllerBase
    {
        private readonly DbContext _context;
        protected DbSet<Child> children;
        private readonly IUnitOfwork _unitOfWork;

        public RepositoryChild(IUnitOfwork unitOfwork)
        {
            _unitOfWork = unitOfwork;
            children = _unitOfWork.Context.Set<Child>();
        }

        public async Task<Child> FindById(Guid id)
        {
            var data = await children.FindAsync(id);
            return data;
        }

        public async Task<Child> Add(Child child)
        {
            child.Features = new PWAFeatures(child.Id);
            children.Add(child);
            await _unitOfWork.SaveChangesAsync();
            return child;
        }

        public async Task<Child> Update(Child child)
        {
            children.Update(child);
            await _unitOfWork.SaveChangesAsync();
            return child;
        }

        public async Task<bool> Delete(Guid id)
        {
            var data = await children.FindAsync(id);
            if (data == null) return false;
            
            children.Remove(data);
            await _unitOfWork.SaveChangesAsync();
            return true;
            
        }
    }
}
