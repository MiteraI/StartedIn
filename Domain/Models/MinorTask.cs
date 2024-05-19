﻿using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseEntities;

namespace Domain.Models;

public class MinorTask : BaseAuditEntity
{
    public string TaskTitle { get; set; }
    public string Description { get; set; }
    
    [ForeignKey(nameof(MajorTask))]
    public string MajorTaskId { get; set; }
    
    public MajorTask MajorTask { get; set; } = null!;
    public Taskboard Taskboard { get; set; } = null!;
}