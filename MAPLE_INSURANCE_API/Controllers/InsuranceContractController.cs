using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using MAPLE_INSURANCE_API.Models;
using MAPLE_INSURANCE_API.DataAccess;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MAPLE_INSURANCE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceContractController : ControllerBase
    {
        IContractDataAccess contractDataAccess;
        public InsuranceContractController(IContractDataAccess _contractDataAccess)
        {
            contractDataAccess = _contractDataAccess;
        }
        // GET: api/<InsuranceContractController>
        [HttpGet]
        [Route("GetContracts")]
        public async Task<IActionResult> GetContracts()
        {
            try
            {
                var posts = await contractDataAccess.GetContracts();
                if (posts == null)
                {
                    return NotFound();
                }

                return Ok(posts);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET api/<InsuranceContractController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InsuranceContractController>
        [HttpPost]
        [Route("AddContract")]
        public async Task<IActionResult> AddContract([FromBody] Contracts modelContract)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var id = await contractDataAccess.AddContract(modelContract);
                    if (id > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateContract")]
        public async Task<IActionResult> UpdateContract([FromBody] Contracts modelContract)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await contractDataAccess.UpdateContract(modelContract);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteContract")]
        public async Task<IActionResult> DeleteContract(int? contractId)
        {
            int result = 0;

            if (contractId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await contractDataAccess.DeleteContract(contractId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
