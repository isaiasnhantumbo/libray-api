using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Authors
{
    public class AddAuthor
    {
        public class AddAuthorCommand : IRequest<Author>
        {
            public string Name { get; set; }
        }

        public class AddAuthorValidator : AbstractValidator<AddAuthorCommand>
        {
            public AddAuthorValidator()
            {
                RuleFor(x => x.Name).NotNull();
            }

            public class AddAuthorHandler : IRequestHandler<AddAuthorCommand,Author>
            {
                private readonly DataContext _context;

                public AddAuthorHandler(DataContext context)
                {
                    _context = context;
                }

                public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
                {
                    var authorExist = await _context.Authors.Where(x =>
                        x.Name.ToUpper() == request.Name.ToUpper()).FirstOrDefaultAsync();
                    if (authorExist != null)
                    {
                        throw new Exception("The author exist in database");
                    }

                    var author = new Author {Name = request.Name};
                    await _context.Authors.AddAsync(author);
                    var result = await _context.SaveChangesAsync();

                    if (result <=0)
                    {
                        throw new Exception("Failed to add the author");
                    }

                    return author;

                }
            }

        }
    }
}