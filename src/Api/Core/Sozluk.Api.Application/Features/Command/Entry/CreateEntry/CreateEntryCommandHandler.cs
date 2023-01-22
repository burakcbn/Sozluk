using AutoMapper;
using MediatR;
using Sozluk.Api.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Command.Entry.CreateEntry
{
    public class CreateEntryCommandHandler : IRequestHandler<CreateEntryCommandRequest, string>
    {
        private readonly IEntryRepository _entryRepository;
        private readonly IMapper _mapper;


        public CreateEntryCommandHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            _entryRepository = entryRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateEntryCommandRequest request, CancellationToken cancellationToken)
        {
            var entry = _mapper.Map<Domain.Models.Entry>(request);

            await _entryRepository.AddAsync(entry);
            return entry.Id.ToString();
        }


    }
}
