﻿using BankServer.Controllers.Base;
using BankServer.Controllers.Models;
using BankServer.Models;
using BankServer.Services;
using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : BankControllerBase<Location, LocationInputModel, LocationsService>
    {
        private readonly LocationsService locationsService;

        public LocationsController(LocationsService locationsService) : base(locationsService)
        {
            this.locationsService = locationsService;
        }
    }
}
