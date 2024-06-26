﻿using backend.Models.DTOs;
using backend.Models.Requests;
using backend.Models.Responses;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers {
    [Route("api/ventas")]
    [ApiController]
    public class SellController : ControllerBase {
        private readonly ISellService _service;

        public SellController(ISellService service){
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            string? search,
            string? minDate,
            string? maxDate
        ) {
            try {
                List<SellDTO> res = await _service.Get(search,minDate,maxDate);

                return Ok(res);
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id) {
            try {
                SellDTO? res = await _service.GetById(id);

                return res == null
                    ? BadRequest()
                    : Ok(res);
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSellRequest req) {
            try {
                CreateSellResponse res = await _service.Create(req);

                return Ok(res);
            } catch(ArgumentException ex) {
                return BadRequest($"The field '{ex.Message}' is invalid.");
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(
            [FromRoute] long id,
            [FromBody] SellDTO dto
        ) {
            try {
                await _service.Update(id, dto);
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
