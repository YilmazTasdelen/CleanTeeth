using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Domain.Entities;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTeeth.Tests.Application.Features.DentalOffices
{
    [TestClass]
    public class CreateDentalOfficeComandHandlerTest
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IDentalOfficeRepository _dentalOfficeRepository;
        private IUnitOfWork _unitOfWork;
        private CreateDentalOfficeCommandHandler _handler;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void Setup()
        {
            _dentalOfficeRepository = Substitute.For<IDentalOfficeRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new CreateDentalOfficeCommandHandler(_dentalOfficeRepository, _unitOfWork);
        }

        [TestMethod]
        public async Task Handle_ValidCommand_ReturnsOfficeId()
        {
            // Arrange
            var command = new CreateDentalOfficeCommand { Name = "Test Dental Office" };
            var dentalOffice = new DentalOffice("Test Dental Office");

            _dentalOfficeRepository.Add(Arg.Any<DentalOffice>()).Returns(dentalOffice);

            var result = await _handler.Handle(command);

            await _dentalOfficeRepository.Received(1).Add(Arg.Any<DentalOffice>());
            await _unitOfWork.Received(1).Commit();

            Assert.AreEqual(dentalOffice.Id, result);
        }

        [TestMethod]
        public async Task Handle_WhenTheresAnError_WeRollback()
        {
            // Arrange
            var command = new CreateDentalOfficeCommand { Name = "Test Dental Office" };
            var dentalOffice = new DentalOffice("Test Dental Office");

            _dentalOfficeRepository.Add(Arg.Any<DentalOffice>()).Throws<Exception>();

            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                await _handler.Handle(command);
            });

            await _unitOfWork.Received(1).Rollback();

        }
    }
}