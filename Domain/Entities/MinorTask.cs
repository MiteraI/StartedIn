﻿using Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class MinorTask : BaseAuditEntity<string>
{
    public int Position { get; set; }
    public string TaskTitle { get; set; }
    public string Description { get; set; }
    public DateTime DeadLine { get; set; }
    
    [ForeignKey(nameof(MajorTask))]
    public string MajorTaskId { get; set; }
    
    public MajorTask MajorTask { get; set; } = null!;
    public Taskboard Taskboard { get; set; } = null!;
}