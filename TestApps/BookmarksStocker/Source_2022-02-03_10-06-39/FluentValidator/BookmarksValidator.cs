using System;
using FluentValidation;

namespace Bookmarks.Project.Entity
{
    public class BookmarksValidator : AbstractValidator<Bookmarks>
    {
        public BookmarksValidator()
        {
            RuleFor(entity => entity.Id).NotNull().WithMessage("Id alan� bo� ge�ilemez.");

            RuleFor(entity => entity.Name).NotEmpty().WithMessage("Name alan� bo� ge�ilemez.");
            RuleFor(entity => entity.Name).MaximumLength(100).WithMessage("Name alan� 100 karakterden uzun olamaz.");

            RuleFor(entity => entity.Url).NotEmpty().WithMessage("Url alan� bo� ge�ilemez.");
            RuleFor(entity => entity.Url).MaximumLength(500).WithMessage("Url alan� 500 karakterden uzun olamaz.");

            RuleFor(entity => entity.CreationTime).NotNull().WithMessage("CreationTime alan� bo� ge�ilemez.");
        }
    }
}