﻿using MediatR;

namespace Sozluk.Api.Application.Features.Command.EntryComment.CreateFav
{
    public class CreateEntryCommentFavCommandRequest:IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}