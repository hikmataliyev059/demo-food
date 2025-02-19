using FoodStore.Core.Entities.Contacts;
using FoodStore.Core.Repositories.Interfaces.Contacts;
using FoodStore.DAL.Context;
using FoodStore.DAL.Repositories.Implements.Common;

namespace FoodStore.DAL.Repositories.Implements.Contacts;

public class ContactRepository : GenericRepository<Contact>, IContactRepository
{
    public ContactRepository(FoodStoreDbContext context) : base(context)
    {
    }
}