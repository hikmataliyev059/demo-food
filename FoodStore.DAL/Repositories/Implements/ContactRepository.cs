using FoodStore.Core.Entities.Contacts;
using FoodStore.Core.Repositories.Interfaces;
using FoodStore.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.DAL.Repositories.Implements;

public class ContactRepository : GenericRepository<Contact>, IContactRepository
{
    public ContactRepository(FoodStoreDbContext context) : base(context)
    {
    }
}