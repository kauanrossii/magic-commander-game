﻿using MagicCommander.Application._Shared.Dtos;
using MagicCommander.Domain.Cards.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace MagicCommander.Application.Decks.CreateDeck
{
	public class CreateDeckRequest : IRequest<EntityKeyDto?>
	{
		[JsonIgnore]
		public int UserId { get; set; }
		public required string Name { get; init; }
		public required Card Commander { get; init; }
	}
}
