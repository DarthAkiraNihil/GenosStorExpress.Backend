using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GenosStorExpress.API.Controllers
{
    [Route("api/computer_cases")]
    [ApiController]
    public class ComputerCasesController : ControllerBase {
        
        private readonly IComputerCaseService _computerCaseService;

        public ComputerCasesController(IComputerCaseService computerCaseService) {
            _computerCaseService = computerCaseService;
        }

        // GET: api/<ComputerCasesController>
        [HttpGet]
        public IEnumerable<ComputerCaseWrapper> Get() {
            return _computerCaseService.List();
        }

        // GET api/<ComputerCasesController>/5
        [HttpGet("{id}")]
        public ComputerCaseWrapper Get(int id) {
            return _computerCaseService.Get(id);
        }

        // POST api/<ComputerCasesController>
        [HttpPost]
        public ActionResult<ComputerCaseWrapper> Post([FromBody]ComputerCaseWrapper value) {
            _computerCaseService.Create((ComputerCaseWrapper) value);
            _computerCaseService.Save();
            return CreatedAtAction(nameof(Get), new { id = ((ComputerCaseWrapper) value).Id }, value);
        }

        // PUT api/<ComputerCasesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ComputerCaseWrapper value) {
            if (id != value.Id) return BadRequest();
            _computerCaseService.Update(id, value);
            _computerCaseService.Save();
            return NoContent();
        }

        // DELETE api/<ComputerCasesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            _computerCaseService.Delete(id);
            _computerCaseService.Save();
            return NoContent();
        }
    }
}
