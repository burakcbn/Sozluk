using AutoMapper;
using MediatR;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Command.EntryComment.CreateEntryComment
{
    public class CreateEntryCommentCommandHandler : IRequestHandler<CreateEntryCommentCommandRequest, string>
    {
        private readonly IEntryCommentRepository _entryCommentRepository;
        private readonly IMapper _mapper;
        public CreateEntryCommentCommandHandler(IEntryCommentRepository entryCommentRepository, IMapper mapper)
        {
            _entryCommentRepository = entryCommentRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateEntryCommentCommandRequest request, CancellationToken cancellationToken)
        {
            var entryComment = _mapper.Map<Domain.Models.EntryComment>(request);
            await _entryCommentRepository.AddAsync(entryComment);
            return entryComment.Id.ToString();
        }
    }
}
