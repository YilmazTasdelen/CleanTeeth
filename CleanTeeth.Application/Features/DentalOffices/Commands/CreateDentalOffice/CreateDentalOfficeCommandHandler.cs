using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice
{
    public class CreateDentalOfficeCommandHandler:IRequestHandler<CreateDentalOfficeCommand,Guid>
    {
        private readonly IDentalOfficeRepository _dentalOfficeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateDentalOfficeCommand> _validator;
        public CreateDentalOfficeCommandHandler(IDentalOfficeRepository dentalOfficeRepository, IUnitOfWork unitOfWork,
            IValidator<CreateDentalOfficeCommand> validator)
        {
            _dentalOfficeRepository = dentalOfficeRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task<Guid> Handle(CreateDentalOfficeCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new CustomValidationException(validationResult);
            }

            try
            {
                var dentalOffice = new DentalOffice(command.Name);
                var result = await _dentalOfficeRepository.Add(dentalOffice);
                await _unitOfWork.Commit();
                return result.Id;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw;
            }
            
        }
    }
}
