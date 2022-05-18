using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services;
public class ShowRestrictionService : IShowRestrictionService
{
    private IRepository<ShowRestriction> _showRestrictionRepository;

    public ShowRestrictionService(IRepository<ShowRestriction> showRestrictionRepository)
    {
        _showRestrictionRepository = showRestrictionRepository;
    }

    public async Task<ShowRestriction> CreateShowRestrictionAsync(ShowRestriction request)
    {
        return await _showRestrictionRepository.AddAsync(request);
    }

    public async Task<bool> DeleteShowRestrictionAsync(int id)
    {
        var showRestriction = await _showRestrictionRepository.GetByIdAsync(id);
        return await _showRestrictionRepository.DeleteAsync(showRestriction);
    }

    public async Task<ShowRestriction> GetShowRestrictionByShowAsync(int id)
    {
        return await _showRestrictionRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<ShowRestriction>> GetShowRestrictionsAsync()
    {
        return await _showRestrictionRepository.GetListAsync();
    }

    public async Task<bool> UpdateShowRestrictionAsync(ShowRestriction request)
    {
        return await _showRestrictionRepository.UpdateAsync(request);
    }
}
