using AutoMapper;
using MediatR;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.Infrastructure.Exceptions;
using Sozluk.Common.Models.Queries;

namespace Sozluk.Api.Application.Features.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQueryRequest, UserDetailViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDetailViewModel> Handle(GetUserDetailQueryRequest request, CancellationToken cancellationToken)
        {
            User user=null;
            if (request.UserId != Guid.Empty)
                user = await _userRepository.GetByIdAsync(request.UserId);
            else if (!string.IsNullOrEmpty(request.UserName))
                user = await _userRepository.GetSingleAsync(x => x.UserName == request.UserName);

            if (user == null)
                throw new DatabaseValidationException("User not found!");

            var userDetail = _mapper.Map<UserDetailViewModel>(user);
            return userDetail;
        }
    }
}
