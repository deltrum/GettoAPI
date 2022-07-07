using GettoAPI.Models;

namespace GettoAPI.Data
{
    public interface IMemberRepo
    {
        Task SaveChanges();
        Task<Member?> GetMemberById(int id);
        Task<IEnumerable<Member>> GetAllMemebers();
        Task CreateMember (Member member);
        
        void DeleteCommand(Member member);

    }
}