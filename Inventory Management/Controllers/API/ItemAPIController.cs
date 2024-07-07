﻿using Inventory_Management.Models;
using Inventory_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemAPIController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemAPIController(IItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpGet]

        public List<ItemModel> GetAll()
        {
            return _itemService.GetAll();
        }

        [HttpGet("{id}")]

        public ItemModel GetById(int id)
        {
            return _itemService.GetById(id);
        }

        [HttpPost]
        public bool Add(ItemModel obj)
        {
            return _itemService.Add(obj);
        }

        [HttpPut]
        public bool Updates(ItemModel obj)
        {
            return _itemService.Updates(obj);
        }

        [HttpDelete]
        public int Delete(int id)
        {
            return _itemService.Delete(id);
        }
    }
}
