using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Authors
{
    public class DeleteAuthor
    {
        public class DeleteAuthorCommand:IRequest<Author>
        {
            public int Id { get; set; }

        }

        public class DeleteAuthorHandler:IRequestHandler<DeleteAuthorCommand,Author>
        {
            private readonly DataContext _context;

            public DeleteAuthorHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<Author> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
            {
                var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (author is null)
                {
                    throw new Exception("Author not found");
                }

                _context.Authors.Remove(author);
                var result = await _context.SaveChangesAsync();
                if (result <= 0)
                {
                    throw new Exception("Error to delete Author");
                }

                return author;

            }
        }
    }
}