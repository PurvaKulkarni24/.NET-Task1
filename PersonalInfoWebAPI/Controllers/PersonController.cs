using PersonalInfoWebAPI.DAO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalInfoWebAPI.Models;

namespace PersonalInfoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonDAO _dao;

        public PersonController(IPersonDAO dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalInfo>>> GetPersonalInfos()
        {
            var personalInfos = await _dao.GetPersonalInfos();
            return Ok(personalInfos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalInfo>> GetPersonalInfo(int id)
        {
            var personalInfo = await _dao.GetPersonalInfoById(id);
            if (personalInfo == null)
            {
                return NotFound();
            }
            return Ok(personalInfo);
        }

        [HttpPost]
        public async Task<ActionResult> PostPersonalInfo([FromBody] PersonalInfo personalInfo)
        {
            if (personalInfo == null)
            {
                return BadRequest();
            }

            int rowsInserted = await _dao.InsertPersonalInfo(personalInfo);
            if (rowsInserted > 0)
            {
                return CreatedAtAction(nameof(GetPersonalInfo), new { id = personalInfo.Id }, personalInfo);
            }

            return StatusCode(500, "An error occurred while inserting the record.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutPersonalInfo(int id, [FromBody] PersonalInfo personalInfo)
        {
            if (id != personalInfo.Id)
            {
                return BadRequest();
            }

            int rowsUpdated = await _dao.UpdatePersonalInfo(personalInfo);
            if (rowsUpdated > 0)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePersonalInfo(int id)
        {
            int rowsDeleted = await _dao.DeletePersonalInfo(id);
            if (rowsDeleted > 0)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
