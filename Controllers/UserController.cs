using backend.Models.DTOs;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers {
    [Route("api/usuarios")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IUserService _service;

        public UserController(IUserService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? search) {
            try {
                List<UserDTO> res = await _service.Get(search);

                return Ok(res);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id) {
            try {
                UserDTO? res = await _service.GetById(id);

                return res == null
                    ? BadRequest()
                    : Ok(res);
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(
            [FromBody] UserDTO dto,
            [FromRoute] long id
        ) {
            try {
                await _service.Update(id,dto);

                return Ok();
            } catch(ArgumentException) {
                return BadRequest();
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete([FromRoute] long id) {
            try {
                await _service.Delete(id);

                return Ok();
            } catch(ArgumentException) {
                return BadRequest();
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
