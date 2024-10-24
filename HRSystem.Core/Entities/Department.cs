using HRSystem.Core.Common;
using System;
using System.Collections.Generic;

namespace HRSystem.Core.Entities;

public partial class Department : BaseEntity
{
    public int? ParentId { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Department> InverseParent { get; } = new List<Department>();

    public virtual Department? Parent { get; set; }
}
