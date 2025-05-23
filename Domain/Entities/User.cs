﻿using Domain.Primitives;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public sealed class User : Auditable, IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    [EmailAddress]
    public string Email { get; set; } = default!;
    public string PasswordSalt { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}