
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IShowRestrictionService
    {
        Task<ShowRestriction> CreateShowRestrictionAsync(ShowRestriction request);
        Task<bool> UpdateShowRestrictionAsync(ShowRestriction request);
        Task<ShowRestriction> GetShowRestrictionByShowAsync(int id);
        Task<IEnumerable<ShowRestriction>> GetShowRestrictionsAsync();
        Task<bool> DeleteShowRestrictionAsync(int id);
    }
}