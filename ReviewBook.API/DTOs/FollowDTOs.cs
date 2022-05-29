
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class FollowDTOs
    {
        public int ID_Follower { get; set; }
        public int ID_Following { get; set; }
        public Follow toEntitiesFollow()
        {
            Follow f = new Follow();
            f.ID_Follower = ID_Follower;
            f.ID_Following = ID_Following;
            return f;
        }
    }
}