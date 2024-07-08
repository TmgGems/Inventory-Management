﻿using Inventory_Management.Models.ViewModels;
using Inventory_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesDetailsAPIController : ControllerBase
    {
        private readonly ISalesDetailsService _salesDetailsService;
        public SalesDetailsAPIController(ISalesDetailsService salesDetailsService)
        {
            _salesDetailsService = salesDetailsService;
        }

        [HttpGet]

        public List<SalesMasterVM> GetAll()
        {
            return _salesDetailsService.GetAll();
        }
        [HttpGet("{id}")]
        public SalesMasterVM GetById(int id)
        {
            return _salesDetailsService.GetById(id);
        }

        [HttpPost]
        public bool Add(SalesMasterVM model)
        {
            return _salesDetailsService.Add(model);
        }

        [HttpPut]
        public bool Updates(SalesMasterVM obj)
        {
            return _salesDetailsService.Updates(obj);
        }

        [HttpDelete]
        public int Delete(int id)
        {
            return _salesDetailsService.Delete(id);
        }
    }
}