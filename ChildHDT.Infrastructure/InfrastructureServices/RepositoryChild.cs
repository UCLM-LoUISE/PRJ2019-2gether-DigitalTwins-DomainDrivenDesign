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
        private readonly DbContext _context;
        private readonly IUnitOfwork _unitOfWork;
        public static DbSet<Child> children;
        private static Dictionary<Guid, IFeatures> _featuresCache = new Dictionary<Guid, IFeatures>();
        public string server;
        public int port;
        public string user;
        public string pwd;


        public RepositoryChild(IUnitOfwork unitOfwork, string server, int port, string user, string pwd)
        {
            Random rnd = new Random();
            _unitOfWork = unitOfwork;
            children = _unitOfWork.Context.Set<Child>();
            this.server = server;
            this.port = port;
            this.user = user;
            this.pwd = pwd;
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
            child.Features = new PWAFeatures(child.Id, server, port, user, pwd);
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

        public async Task<List<Child>> GetAll()
        {
            var childrenList = await children.ToListAsync();
            foreach (var child in childrenList)
            {
                if (_featuresCache.TryGetValue(child.Id, out var features))
                {
                    child.Features = features;
                }
            }
            return childrenList;
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
