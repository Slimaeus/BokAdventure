using BokAdventure.Domain.Entities;
using BokAdventure.Domain.Enumerations;

namespace BokAdventure.Domain.Interfaces;
public interface IBokFlowService
{
    Task<Bok> Get(BokIdentify id);
}