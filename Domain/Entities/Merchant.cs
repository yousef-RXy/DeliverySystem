﻿namespace Domain.Entities;
public class Merchant
{
    public Guid Id { get; set; }
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
}