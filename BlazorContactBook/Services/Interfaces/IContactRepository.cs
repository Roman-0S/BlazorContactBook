﻿using BlazorContactBook.Models;

namespace BlazorContactBook.Services.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact> CreateContactAsync(Contact contact);

        Task AddCategoriesToContactAsync(int contactId, string userId, IEnumerable<int> categoryIds);
    }
}
