using api.Dtos.User;
using api.Models;

namespace api.Mappers;

public static class UserMappers
{
    public static UserDto ToUserDto(this User userModel)
    {
        return new UserDto
        {
            Id = userModel.Id,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Document = userModel.Document,
            Email = userModel.Email,
            Balance = userModel.Balance,
            UserType = userModel.UserType
        };
    }
}
