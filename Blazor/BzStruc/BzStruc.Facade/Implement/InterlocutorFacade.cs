using BzStruc.Facade.Service;
using BzStruc.Repository.DAL;
using BzStruc.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BzStruc.Facade.Implement
{
    public class InterlocutorFacade : GenericEFRepository<MsSql1DbContext, GenericUser>, IInterlocutorFacade
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_msGenericDb">Dont use this in this facade ,use for send to InterfaceOfService</param>
        public InterlocutorFacade(MsSql1DbContext _msGenericDb
            
            ) : base(_msGenericDb)
        {

        }
          
        public async Task AddC()
        {
            Expression<Func<Logs, bool>> predicate = p => true;
            await AddGeneric<Logs>(predicate);
            await UserService.CreateLogs(_msGenericDb.Set<Logs>());
            await _msGenericDb.SaveChangesAsync();
        }


        public async Task GetC()
        {
            var result = await UserService.GetFirstLogs(_msGenericDb.Set<Logs>());
        }



        //todo test with task
        public async Task AddCTask(int time, int value)
        {
            await UserService.AddCTask(time, value);


        }

        public async Task<string> AddA(string nona)
        {
            var aa = await _msGenericDb.Set<GenericUser>().Where(w => w.Id > 0).ToListAsync();
            Expression<Func<GenericUser, bool>> p = s => s.FirstName != null;
            var a = await TestGet(p);
            return nona + "_a";
        }
    }

    public interface IInterlocutorFacade
    {
        Task<string> AddA(string nona);
        Task AddCTask(int time, int value);
    }
}
