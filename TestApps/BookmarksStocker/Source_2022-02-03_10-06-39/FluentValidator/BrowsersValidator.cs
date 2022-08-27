using FluentValidation;

namespace Bookmarks.Project.Entity
{
    public class BrowsersValidator : AbstractValidator<Browsers>
    {
        public BrowsersValidator()
        {
            RuleFor(entity => entity.Id).NotNull().WithMessage("Id alan� bo� ge�ilemez.");

            RuleFor(entity => entity.Name).NotEmpty().WithMessage("Name alan� bo� ge�ilemez.");
            RuleFor(entity => entity.Name).MaximumLength(100).WithMessage("Name alan� 100 karakterden uzun olamaz.");

            RuleFor(entity => entity.Path).NotEmpty().WithMessage("Path alan� bo� ge�ilemez.");
            RuleFor(entity => entity.Path).MaximumLength(500).WithMessage("Path alan� 500 karakterden uzun olamaz.");
        }
    }
}