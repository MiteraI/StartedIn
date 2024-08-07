﻿using Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Team : BaseAuditEntity<string>
{
    public string TeamName { get; set; }
    [ForeignKey(nameof(User))]
    public string TeamLeaderId { get; set; }
    public string Description { get; set; }
    
    public IEnumerable<Project> Projects { get; set; }
    public IEnumerable<TeamUser> TeamUsers { get; set; }
    public User TeamLeader { get; set; }
}