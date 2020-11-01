using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditorAPI.BuildingBlocks.Commands;
using AuditorAPI.BuildingBlocks.Queries;
using AuditorAPI.Contracts;
using AuditorAPI.Domain;
using AuditorAPI.Persistence;
using AuditorAPI.Services;
using AuditorAPI.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuditorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Authorize]
    public class AuditPortfolioController : ControllerBase
    {
        private readonly IAuditPortfolioService _auditPortfolioService;
        private readonly IUnitOfWork _unitOfWork;
        private IMediator _mediator;
        public AuditPortfolioController(IAuditPortfolioService auditPorfolioService, IAuditUnitOfWork unitOfWork, IMediator mediator)
        {
            _auditPortfolioService = auditPorfolioService;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        // GET: api/<AuditPortfolioController>
        [HttpGet]
        public async Task<List<AuditPortfolioCreationResponse>> Get()
        {
            
            var query = new GetAllAuditPortfoliosQuery();
            var result = await _mediator.Send(query);
            return result;
           
        }

        // GET api/<AuditPortfolioController>/5
        [HttpGet("{id}")]
        public async Task<AuditPortfolioCreationResponse> Get(int id)
        {
            var query = new GetAuditPortfolioQuery(id);
            var result = await _mediator.Send(query);
            return result;
        }

        // POST api/<AuditPortfolioController>
        //[HttpPost]
        //public void Post([FromBody] AuditPortfolioCreationRequest auditPortfolioCreationRequest)
        //{
        //    _auditPortfolioService.Create(new AuditPortfolio() { 
        //        ClientId=auditPortfolioCreationRequest.ClientId , ReportReleaseDate = auditPortfolioCreationRequest.ReportReleaseDate , Name=auditPortfolioCreationRequest.Name
        //    }
        //    );
        //}

        // POST api/<AuditPortfolioController>
        [HttpPost]
        public async Task Post([FromBody] CreateAuditPortfolioCommand auditPortfolioCreationCommand)
        {
            AuditPortfolioCreationResponse response= await _mediator.Send(auditPortfolioCreationCommand);

            
        }

        // PUT api/<AuditPortfolioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuditPortfolioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
