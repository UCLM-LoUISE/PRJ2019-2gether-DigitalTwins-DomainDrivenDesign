using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChildHDT.Domain.Entities;
using ChildHDT.Domain.ValueObjects;
using ChildHDT.Infrastructure.InfrastructureServices.Context;
using ChildHDT.Infrastructure.IntegrationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace ChildHDT.Infrastructure.InfrastructureServices
{
    public class RepositoryChild : ControllerBase
    {
        public int IDPrueba;
        private readonly DbContext _context;
        protected DbSet<Child> children;
        private readonly IUnitOfwork _unitOfWork;
        private static Dictionary<Guid, IFeatures> _featuresCache = new Dictionary<Guid, IFeatures>();


        public RepositoryChild(IUnitOfwork unitOfwork)
        {
            Random rnd = new Random();
            IDPrueba = rnd.Next(100);
            _unitOfWork = unitOfwork;
            children = _unitOfWork.Context.Set<Child>();
        }

        public async Task<Child> FindById(Guid id)
        {
            var data = await children.FindAsync(id);
            if (data != null && _featuresCache.TryGetValue(id, out var features))
            {
                data.Features = features;
            }
            return data;
        }

        public async Task<Child> Add(Child child)
        {
            children.Add(child);
            await _unitOfWork.SaveChangesAsync();
            child.Features = new PWAFeatures(child.Id);
            _featuresCache[child.Id] = child.Features;
            var prueba = _featuresCache;
            return child;
        }

        public async Task<Child> Update(Child child)
        {
            children.Update(child);
            await _unitOfWork.SaveChangesAsync();
            if (child.Features != null)
            {
                _featuresCache[child.Id] = child.Features;
            }
            return child;
        }

        public async Task<bool> Delete(Guid id)
        {
            var data = await children.FindAsync(id);
            if (data == null) return false;
            
            children.Remove(data);
            await _unitOfWork.SaveChangesAsync();
            _featuresCache.Remove(id);
            return true;
            
        }
    }
}
