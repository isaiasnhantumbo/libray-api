using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Authors
{
    public class ListAuthor
    {
        public class ListAuthorQuery:IRequest<IReadOnlyList<Author>>
        {

        }
        public class ListAuthorHandler:IRequestHandler<ListAuthorQuery,IReadOnlyList<Author>>
        {
            private readonly DataContext _context;

            public ListAuthorHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<IReadOnlyList<Author>> Handle(ListAuthorQuery request, CancellationToken cancellationToken)
            {
                return await _context.Authors.ToListAsync();
            }
        }
    }
}