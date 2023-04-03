using AutoMapper;
using FinancialManager.Interfaces;
using FinancialManager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManager.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ILoggerManager _logger;
        protected readonly IMapper _mapper;

        public BaseApiController(IUnitOfWork unitOfWork, ILoggerManager logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
    }
}
