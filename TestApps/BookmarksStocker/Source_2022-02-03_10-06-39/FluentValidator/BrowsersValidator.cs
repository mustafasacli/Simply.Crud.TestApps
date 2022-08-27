using FluentValidation;

namespace Bookmarks.Project.Entity
{
    public class BrowsersValidator : AbstractValidator<Browsers>
    {
        public BrowsersValidator()
        {
            RuleFor(entity => entity.Id).NotNull().WithMessage("Id alaný boþ geçilemez.");

            RuleFor(entity => entity.Name).NotEmpty().WithMessage("Name alaný boþ geçilemez.");
            RuleFor(entity => entity.Name).MaximumLength(100).WithMessage("Name alaný 100 karakterden uzun olamaz.");

            RuleFor(entity => entity.Path).NotEmpty().WithMessage("Path alaný boþ geçilemez.");
            RuleFor(entity => entity.Path).MaximumLength(500).WithMessage("Path alaný 500 karakterden uzun olamaz.");
        }
    }
}