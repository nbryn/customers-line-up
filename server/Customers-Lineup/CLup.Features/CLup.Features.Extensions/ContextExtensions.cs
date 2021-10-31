using System;
using System.Threading.Tasks;

using CLup.Data;
using CLup.Domain.Shared;

namespace CLup.Features.Extensions
{
    public static class ContextExtensions
    {
        public static async Task<int> AddAndSave(this CLupContext context, params Entity[] entities)
        {
            foreach (var entity in entities)
            {
                context.Add(entity);
            }
            
            return await context.SaveChangesAsync();
        }

        public static void CreateEntityIfNotExists<T>(this CLupContext context, T existingEntity, T newEntity) where T : Entity
        {
            if (existingEntity == null)
            {
                newEntity.Id = Guid.NewGuid().ToString();
                context.Add(newEntity);
            }
        }

        public static async Task<int> RemoveAndSave(this CLupContext context, Entity value)
        {
            context.Remove(value);

            return await context.SaveChangesAsync();
        }

        public static async Task<int> UpdateEntity<T>(this CLupContext context, string id, T updatedEntity) where T : Entity
        {
            var entity = (Entity)await context.FindAsync(typeof(T), id);

            updatedEntity.Id = entity.Id;
            context.Entry(entity).CurrentValues.SetValues(updatedEntity);

            return await context.SaveChangesAsync();
        }
    }
}