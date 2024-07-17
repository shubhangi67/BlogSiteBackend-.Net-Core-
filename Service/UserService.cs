using System.Text;
using BlogSite.Data;
using BlogSite.Entity;
using BlogSite.ViewModel.User;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.Service
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private static Random random = new Random();
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(UserCreateViewModel input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var user = new UserModel
            {
                Name = input.Name,
                Email = input.Email,
                Password = GenerateRandomPassword(10),
                PhoneNumber = input.PhoneNumber,
                Address = input.Address,
                CreatedOn = DateTime.Now
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }
        public static string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            StringBuilder password = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                password.Append(validChars[random.Next(validChars.Length)]);
            }

            return password.ToString();
        }
        public async Task<UserModel> GetUserAsync(Guid id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<IEnumerable<UserModel>> GetAllUserAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task UpdateUserAsync(Guid id, UserUpdateViewModel input)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("Blog not found");
            }
            user.PhoneNumber = input.PhoneNumber;
            user.Address = input.Address;
            user.Email = input.Email;
            user.Name = input.Name;
            user.ModifiedOn = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("Blog not found");
            }
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}