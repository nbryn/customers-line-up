using System.Linq;

using AutoMapper;

using CLup.Data;
using CLup.Domain;
using CLup.Features.Users.Commands;

using Converter = System.Convert;

namespace CLup.Features.Users
{
    public class UpdateUserInfoConverter : ITypeConverter<UpdateUserInfoCommand, User>
    {
        private readonly CLupContext _context;
        public UpdateUserInfoConverter(CLupContext context)
        {
            _context = context;
        }
        public User Convert(UpdateUserInfoCommand updateUserInfoCommand, User u, ResolutionContext context)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == updateUserInfoCommand.Id);

            foreach (var prop in updateUserInfoCommand.GetType().GetProperties())
            {
                var propInfo = user.GetType().GetProperty(prop.Name);
                propInfo.SetValue(user, Converter.ChangeType(prop.GetValue(updateUserInfoCommand), propInfo.PropertyType), null);
            }

            return user;
        }
    }
}