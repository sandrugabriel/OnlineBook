using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Categories.Controller.interfaces;
using OnlineBookShop.Categories.Dto;
using OnlineBookShop.Categories.Service.interfaces;
using OnlineCarWash.System.Exceptions;

namespace OnlineBookShop.Categories.Controller
{
    public class ControllerCategory : ControllerAPICategory
    {
        ICategoryQueryService _query;
        ICategoryCommandService _command;

        public ControllerCategory(ICategoryQueryService query, ICategoryCommandService command)
        {
            _query = query;
            _command = command;
        }

        [Authorize]
        public override async Task<ActionResult<List<CategoryResponse>>> GetAll()
        {
            try
            {
                var services = await _query.GetAllAsync();
                return Ok(services);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<CategoryResponse>> GetById([FromQuery] int id)
        {
            try
            {
                var services = await _query.GetByIdAsync(id);
                return Ok(services);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<CategoryResponse>> GetByName([FromQuery] string name)
        {
            try
            {
                var services = await _query.GetByNameAsync(name);
                return Ok(services);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<CategoryResponse>> CreateCategory([FromQuery] string name)
        {
            try
            {
                var service = await _command.CreateCategory(name);
                return Ok(service);
            }
            catch (InvalidName ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<CategoryResponse>> UpdateCategory([FromQuery] int id, [FromQuery] string name)
        {
            try
            {
                var service = await _command.UpdateCategory(id, name);
                return Ok(service);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidName ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        public override async Task<ActionResult<CategoryResponse>> DeleteCategory([FromQuery] int id)
        {
            try
            {
                var service = await _command.DeleteCategory(id);
                return Ok(service);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
