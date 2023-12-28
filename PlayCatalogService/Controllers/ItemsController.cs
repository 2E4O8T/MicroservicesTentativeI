using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayCatalogService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayCatalogService.Controllers
{
    [Route("Items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restore...", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Restore...", 10, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Sword", "Restore...", 25, DateTimeOffset.UtcNow)
        };

        // GET /items
        [HttpGet]
        public IEnumerable<ItemDto> GetAllItems()
        {
            return items;
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItemById(Guid id)
        {
            var item = items.Where(item => item.Id == id).FirstOrDefault();

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST /items
        [HttpPost]
        public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
        {
            var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            items.Add(item);

            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = items.Where(item => item.Id == id).FirstOrDefault();

            if (existingItem == null)
            {
                return NotFound();
            }

            var updatedItem = existingItem with
            {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price
            };

            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items[index] = updatedItem;

            return NoContent();
        }
        
        // DELETE /items/{id}
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);

            if (index < 0)
            {
                return NotFound();
            }

            items.RemoveAt(index);

            return NoContent();
        }
    }
}
