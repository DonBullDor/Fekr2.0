using Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repository
{
    public class UserRepository : GenericRepository<EspEtudiant>, IUserRepository
    {
        public UserRepository(Oracle1Context context, ILogger logger) : base(context, logger) { }
        public override async Task<IEnumerable<EspEtudiant>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(UserRepository));
                return new List<EspEtudiant>();
            }
        }

        public override async Task<bool> Upsert(EspEtudiant entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.IdEt == entity.IdEt)
                                                    .FirstOrDefaultAsync();

                if (existingUser == null)
                    return await Add(entity);

                existingUser.NomEt = entity.NomEt;
                existingUser.PnomEt = entity.PnomEt;
                existingUser.EMailEt = entity.EMailEt;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(UserRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(string id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.IdEt == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(UserRepository));
                return false;
            }
        }
    }
}
