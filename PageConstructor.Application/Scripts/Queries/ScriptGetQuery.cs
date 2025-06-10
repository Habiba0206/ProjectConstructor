using PageConstructor.Domain.Common.Queries;
using PageConstructor.Application.Scripts.Models;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Application.Scripts.Queries;

public class ScriptGetQuery : IQuery<ICollection<ScriptDto>>
{
    public ScriptFilter ScriptFilter { get; set; }
}
