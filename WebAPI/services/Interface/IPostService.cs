using Cast.Models;

namespace Cast.services.Interface
{
    public interface IPostService
    {
        List<Post> GetAll();
        Post GetById(Guid id);
        void Add(Post post);
        void Update(Post post);
        void Delete(Guid id);
    }
}
