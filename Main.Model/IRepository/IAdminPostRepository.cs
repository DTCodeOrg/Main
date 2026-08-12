using Main.Model.Tenant;
namespace Main.IRepository;

public interface IAdminPostRepository
{
    Task<AdminPost> GetAdminPostByPostID (int postId);

    Task<List<AdminPost>> GetAllAdminContentPosts ();

    Task<bool> SaveNewAdminPost (AdminPost adminPostEntity);

    Task<bool> DeleteAdminPost (int postId);

    Task<bool> DeleteAdminPostImage (int id,int postId);

    Task<bool> SaveChanges ();

    Task<bool> UpdateAdminPost (AdminPost objPostDm);

    Task<List<AdminPost>> GetSelectAdminPosts ();
}

