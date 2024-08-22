using System;
using MagicCommander.Domain.Cards.Entities;

namespace MagicCommander.Domain.Cards.Services;

public record MagicApiCardResponseDto(MagicApiCardDto Card);
public record MagicApiCardDto(
    int MultiverseId,
    double Cmc,
    string Name,
    string ManaCost,
    string Type,
    string Text,
    string Artist,
    string Number,
    string Layout,
    string ImageUrl,
    List<string> Colors,
    List<string> Subtypes,
    List<TypeCard> Types,
    TypeRarity Rarity,
    List<Ruling> Rulings,
    Guid Id
);
