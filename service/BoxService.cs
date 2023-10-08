using System.ComponentModel.DataAnnotations;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using infrastructure.Repositories;

namespace service;
public class BoxService
{
    private readonly BoxRepository _boxRepository;
    
    public BoxService(BoxRepository boxRepository)
    {
        _boxRepository = boxRepository;
    }

    public IEnumerable<BoxFeedQuery> GetBoxForFeed()
    {
        return _boxRepository.GetBoxForFeed();
    }

    public Box CreateBox(string boxName, double boxWeight)
    {
        var doesBookExist = _boxRepository.DoesBoxtWithNameExist( boxName);
       if (doesBookExist)
        {
            throw new ValidationException("Box already exists with name " + boxName);
        }
        
        return _boxRepository.CreateBox(boxName, boxWeight);
    }

    public Box UpdateBox(int boxId, string boxName, double boxWeight)
    {
        return _boxRepository.UpdateBox( boxId, boxName, boxWeight);
    }

    public void DeleteBox(int boxId)
    {
        
        var result = _boxRepository.DeleteBox(boxId);
        if (!result)
        {
            throw new Exception("Could not insert box");
        }
    }

    public async Task<Box> GetBoxByIdAsync(int boxId)
    {
        // Call the repository method to get a box by its ID asynchronously.
        return await _boxRepository.GetBoxByIdAsync(boxId);
    }
}
