using Data;
using Microsoft.Extensions.Logging;
using Service.IConfiguration;
using Service.Interfaces;
using Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Oracle1Context _context;
        private readonly ILogger _logger;
        public IUserRepository Etudiants { get; private set; }
        public UnitOfWork(Oracle1Context context, ILoggerFactory logger)
        {
            _context = context;
            _logger = logger.CreateLogger("logs");

            Etudiants = new UserRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
