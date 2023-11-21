using API_Code_First_001.Data;
using API_Code_First_001.DTO_s;
using API_Code_First_001.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Code_First_001.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {

        private AppDbContext appDbContext;
        public PersonsController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        [HttpPost]
        public async ValueTask<IActionResult> CreatePersonAsync([FromForm] PersonCreateUpdateDto personCreateDto)
        {
            Person newPerson = new Person()
            {
                FirstName = personCreateDto.FirstName,
                LastName = personCreateDto.LastName,
                Age = personCreateDto.Age,
                Address = personCreateDto.Address,
            };
            await appDbContext.Persons.AddAsync(newPerson);
            await appDbContext.SaveChangesAsync();

            return Ok(newPerson);
        }

        [HttpPut]
        public async ValueTask<IActionResult> UpdatePersonAsync([FromForm] int id, [FromForm] PersonCreateUpdateDto personUpdateDto)
        {
            var result = await appDbContext.Persons.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                result.FirstName = personUpdateDto.FirstName;
                result.LastName = personUpdateDto.LastName;
                result.Age = personUpdateDto.Age;
                result.Address = personUpdateDto.Address;
            }

            return Ok(result);

        }

        [HttpDelete]
        public async ValueTask<IActionResult> DeletePersonById([FromForm] int id)
        {
            var result = await appDbContext.Persons.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                appDbContext.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
            return Ok(result);
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
        {
            var result = await appDbContext.Persons.ToListAsync();
            return Ok(result);
        }
    }
}
