﻿using BankServer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BankServer.Services
{
    public class ProfileService
    {
        private readonly AppDbContext appDbContext;
        private readonly UserManager<Person> userManager;
        private readonly AddressesService addressesService;
        private readonly PhoneNumbersService phoneNumberService;
        private readonly ILogger logger;

        public ProfileService(AppDbContext appDbContext, AddressesService addressesService, PhoneNumbersService phoneNumberService, UserManager<Person> userManager, ILogger<ProfileService> logger)
        {
            this.appDbContext = appDbContext;
            this.addressesService = addressesService;
            this.phoneNumberService = phoneNumberService;
            this.userManager = userManager;
            this.logger = logger;
        }

        public async Task<GetProfileInfoResponse?> GetProfileInfo(string userName)
        {
            var normalizedName = userManager.NormalizeName(userName);
            var person = await appDbContext.Peoples.Include(p => p.PhoneNumber).Include(a => a.Address).SingleOrDefaultAsync(x => x.UserName == normalizedName);
            if (person is null)
                return null;

            return new GetProfileInfoResponse
            {
                Id = person.Id,
                UserName = person.UserName,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                PhoneNumber = person.PhoneNumber,
                Address = person.Address,
                AccessFailedCount = person.AccessFailedCount
            };
        }

        public async Task EditProfileInfo(string userName, EditProfileInput editProfileInput)
        {
            var strategy = appDbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var dbContextTransaction = await appDbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable))
                {
                    try
                    {
                        var normalizedName = userManager.NormalizeName(userName);
                        var person = await appDbContext.Peoples.Include(p => p.PhoneNumber).Include(a => a.Address).SingleOrDefaultAsync(x => x.UserName == normalizedName);
                        if (person is null)
                            throw new NullReferenceException();

                        person.FirstName = editProfileInput.FirstName;
                        person.LastName = editProfileInput.LastName;

                        if (!person.Email?.Equals(editProfileInput.Email, StringComparison.OrdinalIgnoreCase) ?? false)
                        {
                            var result = await userManager.SetEmailAsync(person, editProfileInput.Email);
                            if (!result.Succeeded)
                                throw new Exception(string.Join(",", result.Errors.Select(s => $"{s.Code}: {s.Description}")));
                        }

                        if (!person.PhoneNumber?.Phone?.Equals(editProfileInput.PhoneNumber, StringComparison.OrdinalIgnoreCase) ?? false)
                        {
                            var phoneNumber = await this.phoneNumberService.GetPhoneNumber(editProfileInput.PhoneNumber);
                            if (phoneNumber is null)
                            {
                                phoneNumber = await this.phoneNumberService.CreateAsync(new PhoneNumber { Phone = editProfileInput.PhoneNumber });
                                if (phoneNumber is null)
                                    throw new Exception("Error creating phone number!");
                            }

                            person.PhoneNumber = phoneNumber;
                        }

                        if (person.AddressId != editProfileInput.AddressId)
                        {
                            var address = await this.addressesService.Get(editProfileInput.AddressId);
                            if (editProfileInput.AddressId > 0 && address is null)
                            {
                                throw new Exception($"Address id {editProfileInput.AddressId} not found!");
                            }

                            person.AddressId = address?.Id ?? null;
                            person.Address = address;
                        }

                        await appDbContext.SaveChangesAsync();
                        await dbContextTransaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await dbContextTransaction.RollbackAsync();
                        this.logger.LogError(ex.Message);

                        throw;
                    }
                }
            });
        }

        public async Task ChangePassword(string personId, ChangePasswordInput changePasswordInput)
        {
            var strategy = appDbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var dbContextTransaction = await appDbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable))
                {
                    try
                    {
                        var person = await userManager.FindByNameAsync(personId);
                        if (person is null)
                            throw new NullReferenceException();

                        var result = await userManager.ChangePasswordAsync(person, changePasswordInput.CurrentPassword, changePasswordInput.NewPassword);
                        if (!result.Succeeded)
                            throw new Exception(string.Join(",", result.Errors.Select(s => $"{s.Code}: {s.Description}")));

                        await appDbContext.SaveChangesAsync();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        await dbContextTransaction.RollbackAsync();
                        this.logger.LogError(ex.Message);

                        throw;
                    }
                }
            });
        }
    }
}
