using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Authors
{
    public class UpdateAuthor
    {
        public class UpdateAuthorCommand : IRequest<Author>
        {
            public string Name { get; set; }
            public int Id { get; set; }
        }

        public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorCommand>
        {
            public UpdateAuthorValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
            }
        }
        public class UpdateAuthorHandler:IRequestHandler<UpdateAuthorCommand,Author>
        {
            private readonly DataContext _context;

            public UpdateAuthorHandler(DataContext context)
            {
                _context = context;
            }
            public  async Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
            {
                var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (author is null)
                {
                    throw new Exception("Author not found");
                }

                author.Name = request.Name;

                _context.Authors.Update(author);
                var result = await _context.SaveChangesAsync();

                if (result <=0)
                {
                    throw new Exception("Failded to update the author");
                }

                return author;
            }
        }
    }
}