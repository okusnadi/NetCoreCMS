﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreCMS.Framework.Core.Mvc.Repository
{
    public interface IBaseRepository<EntityT, IdT>
    {
        IQueryable<EntityT> Query();
        EntityEntry GetEntityEntry(EntityT T);
        EntityT Add(EntityT entity);
        EntityT Edit(EntityT entity);
        EntityT Get(IdT id);
        List<EntityT> LoadAll();
        List<EntityT> LoadAllActive();
        List<EntityT> LoadAllByStatus(int status);
        List<EntityT> LoadAllByName(string name);
        List<EntityT> LoadAllByNameContains(string name);
        IDbContextTransaction BeginTransaction();        
        void Remove(EntityT entity);
        void DeletePermanently(EntityT entity);
        void SaveChange();
    }
}
