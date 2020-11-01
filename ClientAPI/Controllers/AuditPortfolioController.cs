using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientAPI.BuildingBlocks.Queries;
using ClientAPI.Contracts;
using ClientAPI.Persistence.UnitOfWork;
using ClientAPI.Services;
using ClientAPI.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientAPI.Controllers
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
        public AuditPortfolioController(IAuditPortfolioService auditPorfolioService, IClientUnitOfWork unitOfWork, IMediator mediator)
        {
            _auditPortfolioService = auditPorfolioService;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        public async Task<List<AuditPortfolioCreationResponse>> Get()
        {
            // return new string[] { "value1", "value2" };
            var query = new GetAllAuditPortfoliosQuery();
            var result = await _mediator.Send(query);
            return result;
            //return result.GetAwaiter().GetResult();
            // return _auditPortfolioService.GetAll();
        }
    }
}
