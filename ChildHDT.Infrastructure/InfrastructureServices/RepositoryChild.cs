﻿using System;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

namespace ChildHDT.Infrastructure.InfrastructureServices
{
    public class RepositoryChild : ControllerBase
    {
        private readonly DbContext _context;
        private readonly IUnitOfwork _unitOfWork;
        public static DbSet<Child> children;
        private static Dictionary<Guid, IFeatures> _featuresCache = new Dictionary<Guid, IFeatures>();
        private readonly IConfiguration _configuration;
        public string server;
        public int port;
        public string user;
        public string pwd;
        private static readonly object _databaseLock = new object();

        public RepositoryChild(IUnitOfwork unitOfwork, IConfiguration configuration)
        {
            Random rnd = new Random();
            _unitOfWork = unitOfwork;
            children = _unitOfWork.Context.Set<Child>();
            _configuration = configuration;
            server = _configuration["MQTT:Server"];
            port = Convert.ToInt32(_configuration["MQTT:Port"]);
            user = _configuration["MQTT:UserName"];
            pwd = _configuration["MQTT:Password"];
        }

        public async Task<Child> FindById(Guid id)
        {
            Child data;
            lock (_databaseLock)
            {
                data = children.Find(id);
            }

            if (data == null) return null;
            if (_featuresCache.TryGetValue(id, out var features))
            {
                data.Features = features;
            }
            else
            {
                data.Features = new PWAFeatures(data.Id, _configuration);
            }
            return data;
        }

        public async Task<Child> Add(Child child)
        {
            lock (_databaseLock)
            {
                children.Add(child);
            }
            await _unitOfWork.SaveChangesAsync();
            child.Features = new PWAFeatures(child.Id, _configuration);
            _featuresCache[child.Id] = child.Features;
            return child;
        }

        public async Task<Child> Update(Child child)
        {
            lock (_databaseLock)
            {
                children.Update(child);
            }
            await _unitOfWork.SaveChangesAsync();
            if (child.Features != null)
            {
                _featuresCache[child.Id] = child.Features;
            }
            return child;
        }

        public async Task<List<Child>> GetAll()
        {
            List<Child> childrenList;
            lock (_databaseLock)
            {
                childrenList = await children.ToListAsync();
            }

            foreach (var child in childrenList)
            {
                if (_featuresCache.TryGetValue(child.Id, out var features))
                {
                    child.Features = features;
                }
                else
                {
                    child.Features = new PWAFeatures(child.Id, _configuration);
                }
            }

            return childrenList;
        }

        public async Task<bool> Delete(Guid id)
        {
            Child data;
            lock (_databaseLock)
            {
                data = await children.FindAsync(id);
                if (data == null) return false;

                children.Remove(data);
            }
            await _unitOfWork.SaveChangesAsync();
            _featuresCache.Remove(id);
            return true;
        }
    }
}
