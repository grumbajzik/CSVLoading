using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Interfaces;
using api.Json;
using api.Mappers;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;

namespace api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ApplicationDBContext _context;
        public UserController(IUserRepository repository, ApplicationDBContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.FindAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await _repository.FindById(id);

            if (user == null)
            {
                return NotFound("User was not found");
            }
            return Ok(user);
        }

        [HttpGet("birthNumber/{birthNumber}")]
        public async Task<IActionResult> GetByBirthNumber([FromRoute] string birthNumber)
        {
            var user = await _repository.FindByBirthNumber(birthNumber);

            if (user == null)
            {
                return NotFound("User was not found");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User userModel)
        {

            await _repository.Save(userModel);

            return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, userModel);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UserRequestDto userDto)
        {
            return Ok(await _repository.UpdateById(id, userDto));
        }


        [HttpPut]
        [Route("putBirthNumber/{birthNumber}")]
        public async Task<IActionResult> PutByBirthNumber([FromRoute] string birthNumber, [FromBody] UserUpdateByBirthNumberDto userDto)
        {
            var userModel = _repository.UpdateByBirthNumber(birthNumber, userDto, _context);
            return Ok(userModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _repository.DeleteById(id);
            return NoContent();
        }

        [HttpPost]
        [Route("/loadCsv")]
        public async Task<IActionResult> SaveUsersFromCsv([FromBody] FilePathRequest jsonFilePath)
        {
            string filePath = jsonFilePath.FilePath;
            try
            {
                var response = await _repository.importUsersFromCsv(filePath);
                return Ok(response);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return StatusCode(500, new { message = "Exception: ", details = ex.Message });
            }
        }
    }
}