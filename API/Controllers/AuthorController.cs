using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Authors;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthorController : BaseController
    {
        [HttpGet]
        public async Task<IReadOnlyList<Author>> ListAuthor()
        {
            return await Mediator.Send(new ListAuthor.ListAuthorQuery());
        }

        [HttpPost]
        public async Task<ActionResult<Author>> AddAuthor(AddAuthor.AddAuthorCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Author>> DeleteAuthor(DeleteAuthor.DeleteAuthorCommand command,int id)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Author>> UpdateAuthor(UpdateAuthor.UpdateAuthorCommand command, int id)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }
    }
}