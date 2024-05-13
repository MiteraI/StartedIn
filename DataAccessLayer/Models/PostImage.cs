﻿using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.BaseEntities;

namespace DataAccessLayer.Models;

public class PostImage : BaseAuditEntity
{
    [ForeignKey(nameof(Post))]
    public string PostId { get; set; }
    public string ImageLink { get; set; }

    public Post Post { get; set; } = null!;
}