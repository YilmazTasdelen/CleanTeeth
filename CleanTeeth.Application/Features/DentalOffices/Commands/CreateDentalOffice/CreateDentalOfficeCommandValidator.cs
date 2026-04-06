using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice
{
    public class CreateDentalOfficeCommandValidator:AbstractValidator<CreateDentalOfficeCommand>
    {
        public CreateDentalOfficeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The field {PropertyName} is required.");
                //.MaximumLength(100).WithMessage("The Name must not exceed 100 characters.");
        }
    }
}
