﻿using BlazorContactBook.Data;
using BlazorContactBook.Models;
using BlazorContactBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorContactBook.Services
{
    public class ContactRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : IContactRepository
    {
        public async Task AddCategoriesToContactAsync(int contactId, string userId, IEnumerable<int> categoryIds)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            Contact? contact = await context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId && c.AppUserId == userId);

            if (contact is not null) 
            {
                foreach (int categoryId in categoryIds)
                {
                    Category? category = await context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId && c.AppUserId == userId);

                    if (category is not null)
                    {
                        contact.Categories.Add(category);
                    }
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            context.Contacts.Add(contact);
            await context.SaveChangesAsync();

            return contact;
        }
    }
}
