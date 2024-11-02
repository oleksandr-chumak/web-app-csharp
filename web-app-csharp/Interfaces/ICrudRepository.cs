namespace web_app_csharp.Interfaces;

public interface ICrudRepository<T>
{
    IEnumerable<T> GetAll();
    
    T? GetById(decimal id);
    
    void Add(T obj);

    void Update(T model);
    
    void DeleteById(int id);
}