﻿using Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class MajorTask : BaseAuditEntity<string>
{
    public int Position { get; set; }
    [ForeignKey(nameof(Phase))]
    public string PhaseId { get; set; }
    public string TaskTitle { get; set; }
    public string Description { get; set; }
    
    public Phase Phase { get; set; } = null!;

    public IEnumerable<MinorTask> MinorTasks { get; set; } = null!;
}