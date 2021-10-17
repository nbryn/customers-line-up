using System.Linq;

using AutoMapper;

using CLup.Data;
using CLup.Domain;
using CLup.Features.Businesses.Commands;

using Converter = System.Convert;

namespace CLup.Features.Businesses
{
    public class UpdateBusinessConverter : ITypeConverter<UpdateBusinessCommand, Business>
    {
        private readonly CLupContext _context;
        public UpdateBusinessConverter(CLupContext context)
        {
            _context = context;
        }
        public Business Convert(UpdateBusinessCommand updateBusinessCommand, Business b, ResolutionContext context)
        {
            var business = _context.Businesses.FirstOrDefault(b => b.Id == updateBusinessCommand.Id);

            foreach (var prop in updateBusinessCommand.GetType().GetProperties())
            {
                if (prop.Name != "Type")
                {
                    var propInfo = business.GetType().GetProperty(prop.Name);
                    propInfo.SetValue(business, Converter.ChangeType(prop.GetValue(updateBusinessCommand), propInfo.PropertyType), null);
                }
            }

            business.Type = (BusinessType)BusinessType.Parse(typeof(BusinessType), updateBusinessCommand.Type);

            return business;
        }
    }
}