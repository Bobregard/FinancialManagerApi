using AutoMapper;
using FinancialManager.DataAccess.Data;
using FinancialManager.Interfaces;
using FinancialManager.Models;
using FinancialManager.Service.Interfaces;
using FinancialManager.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace FinancialManager.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;

        private IUserAuthenticationRepository _userAuthenticationRepository;
        private ITransactionRepository _transactionRepository;
        private IBankRepository _bankRepository;
        private ILocationRepository _locationRepository;
        private ICategoryRepository _categoryRepository;
        private UserManager<User> _userManager;
        private IMapper _mapper;
        private IConfiguration _configuration;

        public UnitOfWork(AppDbContext db, UserManager<User> userManager, IMapper mapper, IConfiguration configuration)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public IUserAuthenticationRepository UserAuthentication
        {
            get
            {
                if (_userAuthenticationRepository is null)
                {
                    _userAuthenticationRepository = new UserAuthenticationRepository(_userManager, _configuration, _mapper);
                }
                return _userAuthenticationRepository;
            }
        }
        public ITransactionRepository TransactionRepository
        {
            get
            {
                if (_transactionRepository is null)
                {
                    _transactionRepository = new TransactionRepository(_db);
                }
                return _transactionRepository;
            }
        }

        public IBankRepository BankRepository
        {
            get
            {
                if (_bankRepository is null)
                {
                    _bankRepository = new BankRepository(_db);
                }
                return _bankRepository;
            }
        }

        public ILocationRepository LocationRepository
        {
            get
            {
                if (_locationRepository is null)
                {
                    _locationRepository = new LocationRepository(_db);
                }
                return _locationRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository is null)
                {
                    _categoryRepository = new CategoryRepository(_db);
                }
                return _categoryRepository;
            }
        }

        public Task SaveAsync() => _db.SaveChangesAsync();
    }
}
