using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookbook.Data.Models;
using Cookbook.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.Mvc.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IDataRepository<User, User> _dataRepository;

        public UserController(IDataRepository<User, User> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _dataRepository.GetAll();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _dataRepository.Get(id);
        }
    }
}
