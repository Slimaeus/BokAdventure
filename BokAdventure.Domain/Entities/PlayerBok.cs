﻿namespace BokAdventure.Domain.Entities;

public sealed class PlayerBok
{
    public Guid? PlayerId { get; set; }
    public Player? Player { get; set; }
    public Guid? BokId { get; set; }
    public Bok? Bok { get; set; }

    public int Amount { get; set; }
}