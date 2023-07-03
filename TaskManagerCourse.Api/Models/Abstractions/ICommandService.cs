namespace TaskManagerCourse.Api.Models.Abstractions
{
    public interface ICommandService<T>
    {
        bool Create(T model);
        bool Update(int id,T model);
        bool Delete(int id);
    }
}
