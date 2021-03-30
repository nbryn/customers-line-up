using CLup.Businesses;

namespace CLup.Context.Initialiser.DataCreators
{
    public static class BusinessOwnerCreator
    {
        public static BusinessOwner Create(int id, string UserEmail)
        {
            var owner = new BusinessOwner
            {
                Id = id,
                UserEmail = UserEmail
            };

            return owner;
        }
    }
}