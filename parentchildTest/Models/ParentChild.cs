using System;
using System.Collections.Generic;

namespace parentchildTest.Models;

public partial class ParentChild
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public int? LanguageFlag { get; set; }
}
