using BzStruc.Repository.DAL;
using BzStruc.Repository.Models;
using BzStruc.Repository.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BzStruc.Repository.Service.Implement
{
    public class ContactService : IContactService
    {
        public async Task GetContact(MsSql1DbContext _msGenericDb, Expression<Func<Contact, bool>> predicate, PagingParameters paging)
        {
            var query = _msGenericDb.Set<Contact>().Where(predicate);
            //var query2 = _msGenericDb.Set<Contact>().Where(predicate).Select(s => new Logs());
            var results = await LinqExtensions.PagingResultProjectionAsync<IQueryable<Contact>, Contact>(query, paging);
            var result = await results.results.ToListAsync();

        }
    }
}
