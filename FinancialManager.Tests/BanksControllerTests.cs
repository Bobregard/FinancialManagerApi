using AutoMapper;
using FinancialManager.Interfaces;
using FinancialManager.Models;
using FinancialManager.Service.Interfaces;
using FinancialManagerAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Web.Http.Results;

namespace FinancialManager.Tests
{
    public class BanksControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ILoggerManager> _loggerMock;
        private Mock<IMapper> _mapperMock;
        private BanksController _banksController;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILoggerManager>();
            _mapperMock = new Mock<IMapper>();
            _banksController = new BanksController(_unitOfWorkMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetAllBanks_WhenCalled_ReturnsOk()
        {
            var banks = new List<Bank> { new Bank { Id = 1 }, new Bank { Id = 2 } };
            _unitOfWorkMock.Setup(u => u.BankRepository.GetAllBanks()).ReturnsAsync(banks);

            var result = await _banksController.GetAllBanks();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            var model = okResult.Value as IEnumerable<Bank>;
            Assert.That(banks, Has.Count.EqualTo(model.Count()));
        }

        [Test]
        public async Task GetAllBanks_WhenExceptionIsThrown_ReturnsInternalServerError()
        {
            _unitOfWorkMock.Setup(uow => uow.BankRepository.GetAllBanks()).ThrowsAsync(new Exception());

            var result = await _banksController.GetAllBanks();

            var okResult = result as ObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(500));
        }
    }
}