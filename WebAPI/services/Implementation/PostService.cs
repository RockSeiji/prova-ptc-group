using Cast.Models;
using Cast.services.Interface;

namespace Cast.services.Implementation
{
    public class PostService : IPostService
    {
        private readonly CastDBContext _context;

        public PostService(CastDBContext context)
        {
            _context = context;
        }

        public List<Post> GetAll()
        {
            var list = _context.Posts.ToList();
            return list;
        }

        public Post GetById(Guid id)
        {
            var post = _context.Posts.First(l => l.Id == id);
            return post;
        }

        public void Add(Post post) 
        {
            _context.Add(post);
            _context.SaveChanges();
        }

        public void Update(Post post)
        {
            _context.Update(post);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var post = _context.Posts.First(l => l.Id == id);
            _context.Remove(post);
            _context.SaveChanges();
        }
    }
}
