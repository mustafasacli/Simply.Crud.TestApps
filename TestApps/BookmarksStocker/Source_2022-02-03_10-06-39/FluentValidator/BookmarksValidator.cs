using System;
using FluentValidation;

namespace Bookmarks.Project.Entity
{
    public class BookmarksValidator : AbstractValidator<Bookmarks>
    {
        public BookmarksValidator()
        {
            RuleFor(entity => entity.Id).NotNull().WithMessage("Id alaný boþ geçilemez.");

            RuleFor(entity => entity.Name).NotEmpty().WithMessage("Name alaný boþ geçilemez.");
            RuleFor(entity => entity.Name).MaximumLength(100).WithMessage("Name alaný 100 karakterden uzun olamaz.");

            RuleFor(entity => entity.Url).NotEmpty().WithMessage("Url alaný boþ geçilemez.");
            RuleFor(entity => entity.Url).MaximumLength(500).WithMessage("Url alaný 500 karakterden uzun olamaz.");

            RuleFor(entity => entity.CreationTime).NotNull().WithMessage("CreationTime alaný boþ geçilemez.");
        }
    }
}