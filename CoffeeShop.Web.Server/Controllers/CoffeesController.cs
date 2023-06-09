﻿using CoffeeShop.Models;
using CoffeeShop.Models.Coffees;
using CoffeeShop.Services.Coffees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Web.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Common.Constants.AdminRole)]
public class CoffeesController : ControllerBase
{
    private readonly ICoffeesService _coffees;

    public CoffeesController(ICoffeesService coffees) 
        => _coffees = coffees;

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CoffeeResponseModel>>> GetAll()
    {
        var result = await _coffees.GetAllAsync();
        if (!result.IsSuccessful)
        {
            return ToError(result);
        }
        return Ok(result.Data);
    }

    [HttpGet("{id:int}", Name = nameof(GetById))]
    [AllowAnonymous]
    public async Task<ActionResult<CoffeeResponseModel>> GetById(int id)
    {
        var result = await _coffees.GetByIdAsync(id);
        
        if (!result.IsSuccessful)
        {
            return ToError(result);
        }
        
        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<ActionResult<CoffeeResponseModel>> Create(
        [FromBody] CoffeeRequestModel model)
    {
        if (model is null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _coffees.CreateAsync(model);
        
        if (!result.IsSuccessful)
        {
            return ToError(result);
        }
        
        return CreatedAtRoute(
            nameof(GetById),
            new { id = result.Data.Id },
            result.Data);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] CoffeeRequestModel model)
    {
        if (model is null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _coffees.UpdateAsync(id, model);

        if (!result.IsSuccessful)
        {
            return ToError(result);
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _coffees.DeleteAsync(id);

        if (!result.IsSuccessful)
        {
            return ToError(result);
        }

        return NoContent();
    }

    private ObjectResult ToError(Result result)
    {
        return StatusCode(result.StatusCode, result.Errors);
    }
}
