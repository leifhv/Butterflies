using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Butterflies.Shared;
using ButterflyApi.Models;
using ButterflyApi.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace ButterflyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ButterfliesController : ControllerBase
    {
        private readonly IButterflyStore _butterflyStore;
        private readonly IUrlHelper _urlHelper;

        public ButterfliesController(IButterflyStore butterflyStore)
        {
            _butterflyStore = butterflyStore;           
        }

        [HttpGet]
        public ActionResult GetAllButterflies()
        {
            var butterflies = _butterflyStore.GetAllEntries();


            return Ok(butterflies);
        }

        [HttpGet]
        [Route("{id}", Name = nameof(GetSingleButterfly))]
        public ActionResult GetSingleButterfly(string id)
        {
            var butterfly = _butterflyStore.Get(id);
            if(butterfly == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(butterfly);
            }
            
        }

        [HttpPost(Name = nameof(SaveButterfly))]
        public ActionResult<ButterflyDto> SaveButterfly([FromBody] ButterflyDto butterfly)
        {
            if (butterfly == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            butterfly = _butterflyStore.Save(butterfly);
            return Ok(butterfly);
        }

    
        [HttpDelete]
        [Route("{id}", Name = nameof(DeleteButterfly))]
        public ActionResult DeleteButterfly(string id)
        {
            var butterfly = _butterflyStore.Get(id);

            if (butterfly == null)
            {
                return Content("heihei");
            }

            _butterflyStore.Delete(id);
            return NoContent();
        }                
    }
}
