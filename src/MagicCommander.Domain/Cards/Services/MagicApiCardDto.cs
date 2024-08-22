using System;
using MagicCommander.Domain.Cards.Entities;

namespace MagicCommander.Domain.Cards.Services;

public record MagicApiCardResponseDto(MagicApiCardDto Card);
public record MagicApiCardDto(
    string Name,
    string ManaCost,
    double Cmc,
    List<string> Colors,
    string Type,
    // List<TypeCard> Types,
    // List<SubtypeCard> Subtypes,
    // TypeRarity Rarity,
    string Text,
    string Artist,
    string Number,
    string Layout,
    string MultiverseId,
    string ImageUrl,
    List<Ruling> Rulings,
    Guid Id
);
