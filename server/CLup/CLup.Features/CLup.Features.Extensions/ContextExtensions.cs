using System;
using System.Threading.Tasks;

using CLup.Data;
using CLup.Domain;

namespace CLup.Features.Extensions
{
    public static class ContextExtensions
    {
        public static async Task<int> AddAndSave(this CLupContext context, BaseEntity value)
        {
            context.Add(value);

            return await context.SaveChangesAsync();
        }

        public static void CreateEntityIfNotExists<T>(this CLupContext context, T existingEntity, T newEntity) where T : BaseEntity
        {
            if (existingEntity == null)
            {
                newEntity.Id = Guid.NewGuid().ToString();
                context.Add(newEntity);
            }
        }
        public static async Task<int> RemoveAndSave(this CLupContext context, BaseEntity value)
        {
            context.Remove(value);

            return await context.SaveChangesAsync();
        }

        public static async Task<int> UpdateEntity<T>(this CLupContext context, string id, T updatedEntity) where T : BaseEntity
        {
            var entity = (BaseEntity) await context.FindAsync(typeof(T), id);
            updatedEntity.Id = entity.Id;

            context.Entry(entity).CurrentValues.SetValues(updatedEntity);

            return await context.SaveChangesAsync();
        }
    }
}