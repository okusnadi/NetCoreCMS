﻿using System;
using System.Collections.Generic;
using System.Linq;
using NetCoreCMS.Framework.Core.Models;
using NetCoreCMS.Framework.Core.Mvc.Models;
using NetCoreCMS.Framework.Core.Mvc.Services;
using NetCoreCMS.Framework.Core.Repository;

namespace NetCoreCMS.Framework.Core.Services
{
    public class NccSettingsService : IBaseService<NccSettings>
    {
        private readonly NccSettingsRepository _entityRepository;
         
        public NccSettingsService(NccSettingsRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }
         
        public NccSettings Get(long entityId)
        {
            return _entityRepository.Get(entityId);
        }

        public NccSettings GetByKey(string key)
        {
            return _entityRepository.Query().FirstOrDefault(x => x.Key == key);
        }

        public NccSettings SetByKey(string key, string value)
        {
            var settings = _entityRepository.Query().FirstOrDefault(x => x.Key == key);
            if(settings != null)
            {
                settings.Value = value;
                _entityRepository.Edit(settings);
            }
            else
            {
                settings = new NccSettings() { Key = key, Value = value };
                _entityRepository.Add(settings);
            }
            _entityRepository.SaveChange();

            return settings;
        }

        public NccSettings Save(NccSettings entity)
        {
            _entityRepository.Add(entity);
            _entityRepository.SaveChange();
            return entity;
        }

        public NccSettings Update(NccSettings entity)
        {
            var oldEntity = _entityRepository.Get(entity.Id);
            if(oldEntity != null)
            {
                using (var txn = _entityRepository.BeginTransaction())
                {
                    CopyNewData(oldEntity, entity);
                    _entityRepository.Edit(oldEntity);
                    _entityRepository.SaveChange();
                    txn.Commit();
                }
            }
            
            return entity;
        }
        
        public void Remove(long entityId)
        {
            var entity = _entityRepository.Get(entityId );
            if (entity != null)
            {
                entity.Status = EntityStatus.Deleted;
                _entityRepository.Edit(entity);
                _entityRepository.SaveChange();
            }
        }

        public List<NccSettings> LoadAll()
        {
            return _entityRepository.LoadAll();
        }

        public List<NccSettings> LoadAllActive()
        {
            return _entityRepository.LoadAllActive();
        }

        public List<NccSettings> LoadAllByStatus(int status)
        {
            return _entityRepository.LoadAllByStatus(status);
        }

        public List<NccSettings> LoadAllByName(string name)
        {
            return _entityRepository.LoadAllByName(name);
        }

        public List<NccSettings> LoadAllByNameContains(string name)
        {
            return _entityRepository.LoadAllByNameContains(name);
        }

        public void DeletePermanently(long entityId)
        {
            var entity = _entityRepository.Get(entityId);
            if (entity != null)
            {
                _entityRepository.Remove(entity);
                _entityRepository.SaveChange();
            }
        }

        private void CopyNewData(NccSettings oldEntity, NccSettings entity)
        {             
            oldEntity.ModificationDate = entity.ModificationDate;
            oldEntity.ModifyBy = entity.ModifyBy;
            oldEntity.Name = entity.Name; 
            oldEntity.Status = entity.Status; 
            oldEntity.CreateBy = entity.CreateBy;
            oldEntity.CreationDate = entity.CreationDate;  
            
            oldEntity.Status = entity.Status; 
            oldEntity.VersionNumber = entity.VersionNumber;
            oldEntity.Key = entity.Key;
            oldEntity.Value = entity.Value;
        }        
    }
}
