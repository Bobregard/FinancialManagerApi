using FinancialManager.Service.Interfaces;

namespace FinancialManager.Interfaces
{
    public interface IUnitOfWork
    {
        ITransactionRepository TransactionRepository { get; }
        IBankRepository BankRepository { get; }
        ILocationRepository LocationRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IUserAuthenticationRepository UserAuthentication { get; }
        Task SaveAsync();
    }
}
