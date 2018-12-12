using BzStruc.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BzStruc.Facade.Service
{
    public static class UserService
    { 
        internal static async Task<Logs> GetFirstLogs(DbSet<Logs> dbSet)
        {
            var result = await dbSet.Where(w => w.LogId > 0).FirstOrDefaultAsync();
            return result;
        }

        internal static async Task CreateLogs(DbSet<Logs> dbSet)
        {
            var result = await dbSet.AddAsync(new Logs() { });
             
        }
         
        internal static async Task AddCTask(int waitTime, int value)
        { 
            var storeAValue = value;
            Thread.Sleep(waitTime * 1000);
            var result = await testTime(value);
        }

        private static async Task<int> testTime(int value)
        {
            await Task.CompletedTask;
            return value;
        }
    }
}
