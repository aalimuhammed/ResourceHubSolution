using FluentValidation;
using ResourceHub.Application.Dtos;

namespace ResourceHub.Application.Validators
{
    public class CreateServiceDtoValidator : AbstractValidator<ServiceDto>
    {
        public CreateServiceDtoValidator() 
        {
            RuleFor(x => x.ActivityNo)
            .NotEmpty();

            RuleFor(x => x.Division)
                .NotEmpty();

            RuleFor(x => x.Unit)
                .NotEmpty();

            RuleFor(x => x.ServiceCat)
                .NotEmpty();

            RuleFor(x => x.ChangedBy)
                .NotEmpty();

            RuleFor(x => x.CreatedBy)
                .NotEmpty();

            RuleFor(x => x.ShortTxt)
                .NotEmpty();

            RuleFor(x => x.LongTxt)
                .NotEmpty();

            RuleFor(x => x.PrimaryLang)
                .NotEmpty();

            RuleFor(x => x.ValuationClass)
                .NotEmpty();
        }
    }
}