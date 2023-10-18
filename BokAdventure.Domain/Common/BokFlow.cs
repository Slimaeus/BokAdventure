using BokAdventure.Domain.Entities;

namespace BokAdventure.Domain.Common;

public sealed class BokFlow<TResult>
{
    public TResult? Data { get; set; }
    public IList<Bok> Boks { get; set; } = new List<Bok>();

    public static BokFlow<TResult> Create(TResult data)
        => new()
        { Data = data };
    public static BokFlow<TResult> NoDataCreate()
    => new();

    public void AddBok(Bok bok)
    {
        if (!Boks.Any(x => x.Id == bok.Id))
        {
            Boks.Add(bok);
        }
    }
}
