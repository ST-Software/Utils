using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinaDb.Shared;

namespace FinaDb.Common.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Synchronize old list with new dto list. Automatically removes old items, updates existing ones and add new items
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="list">List of entities to be updated</param>
        /// <param name="newList">List of dtos</param>
        /// <param name="syncAction">Dto-to-entity transformation action to be performed on each new or updated item.</param>
        /// <param name="getEntityId">Func which returns entity id</param>
        /// <param name="getDtoId">Func which returns dto id - The Id must match that entity id</param>
        /// <returns>Updated list of entities</returns>
        public static List<TEntity> Synchronize<TEntity, TDto>(this List<TEntity> list, List<TDto> newList, Action<TEntity, TDto> syncAction,
            Func<TEntity, Guid> getEntityId, 
            Func<TDto, Guid?> getDtoId)
            where TEntity : class, new()
        {
            var result = new List<TEntity>();
            list.RemoveAll(i => newList.All(j => getDtoId(j) != getEntityId(i)));

            foreach (var dto in newList)
            {
                var entity = list.FirstOrDefault(i => getEntityId(i) == getDtoId(dto)) ?? new TEntity();
                syncAction(entity, dto);
                result.Add(entity);
            }
            return result;
        }

        //Asynchronous version of the above
        public static async Task<List<TEntity>> Synchronize<TEntity, TDto>(this List<TEntity> list, List<TDto> newList, Func<TEntity, TDto, Task> syncActionAsync,
            Func<TEntity, Guid> getEntityId,
            Func<TDto, Guid?> getDtoId)
            where TEntity : class, new()
        {
            var result = new List<TEntity>();
            list.RemoveAll(i => newList.All(j => getDtoId(j) != getEntityId(i)));

            foreach (var dto in newList)
            {
                var entity = list.FirstOrDefault(i => getEntityId(i) == getDtoId(dto)) ?? new TEntity();
                await syncActionAsync(entity, dto);
                result.Add(entity);
            }
            return result;
        }

        /// <summary>
        /// Synchronize old list with new dto list. Automatically removes old items, updates existing ones and add new items
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="list">List of entities to be updated</param>
        /// <param name="newList">List of dtos</param>
        /// <param name="syncAction">Dto-to-entity transformation action to be performed on each new or updated item.</param>
        /// <returns>Updated list of entities</returns>
        public static List<TEntity> Synchronize<TEntity, TDto>(this List<TEntity> list, List<TDto> newList, Action<TEntity, TDto> syncAction) 
            where TEntity: class, IIdObject, new() 
            where TDto: class, IIdObject
        {
            return Synchronize(list, newList, syncAction, entity => entity.Id, dto => dto.Id);
        }
    }
}
