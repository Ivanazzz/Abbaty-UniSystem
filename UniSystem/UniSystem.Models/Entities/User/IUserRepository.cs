namespace UniSystem.Models.Entities.User
{
    public interface IUserRepository
    {
        List<User> GetAll();

        User GetById(int id);
    }
}
