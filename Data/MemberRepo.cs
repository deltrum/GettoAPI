using GettoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GettoAPI.Data
{
    public class MemberRepo : IMemberRepo
    {
        private readonly AppDbContext _context;

        public MemberRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateMember(Member member)
        {
            if(member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            await _context.AddAsync(member);
        }

        public async Task<IEnumerable<Member>> GetAllMemebers()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member?> GetMemberById(int id)
        {
            return await _context.Members.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void DeleteCommand(Member member)
        {
            if(member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            _context.Members.Remove(member);
        }
    }
}