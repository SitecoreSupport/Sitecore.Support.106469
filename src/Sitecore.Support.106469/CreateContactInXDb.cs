using Sitecore.Analytics.Model.Entities;
using Sitecore.Analytics.Tracking;
using Sitecore.Commerce.Data.Customers;
using Sitecore.Commerce.Entities;
using Sitecore.Commerce.Services.Customers;
using CreateContactInXDbProcessor = Sitecore.Commerce.Pipelines.Customers.CreateContact.CreateContactInXDb;


namespace Sitecore.Support.Commerce.Pipelines.Customers.CreateContact
{
    /// <summary>
    /// Creates Contact in xDb
    /// </summary>
    /// <seealso cref="Sitecore.Commerce.Pipelines.Customers.CreateContact.CreateContactInXDb" />
    public class CreateContactInXDb : CreateContactInXDbProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateContactInXDb"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="entityFactory">The entity factory.</param>
        public CreateContactInXDb(IUserRepository userRepository, IEntityFactory entityFactory)
            : base(userRepository, entityFactory)
        {
        }

        /// <summary>
        /// Adds the facets.
        /// </summary>
        /// <param name="contact">The contact.</param>
        /// <param name="createUserResult">The create user result.</param>
        protected override void AddFacets(Contact contact, CreateUserResult createUserResult)
        {
            var emailAddress = contact.GetFacet<IContactEmailAddresses>("Emails");

            if (emailAddress.Entries.Contains("main"))
            {
                emailAddress.Preferred = "main";
                emailAddress.Entries["main"].SmtpAddress = createUserResult.CommerceUser.Email;
                return;
            }
            base.AddFacets(contact, createUserResult);
        }
    }
}