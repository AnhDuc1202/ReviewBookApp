
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class FollowDTOs
    {
        public int ID_Following { get; set; }
        public Follow toEntitiesFollow(int ID_Follower)
        {
            Follow f = new Follow();
            f.ID_Follower = ID_Follower;
            f.ID_Following = ID_Following;
            return f;
        }
    }
}