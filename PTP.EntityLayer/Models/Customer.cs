﻿using PTP.EntityLayer.Abstractions;

namespace PTP.EntityLayer.Models;

public class Customer : BaseEntity
{
    public string FullName { get; set; } = default!;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
