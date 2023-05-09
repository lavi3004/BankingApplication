using BankingApplication.Models;
using BankingApplication.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankingApplication.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected BankingApplicationContext bankingApplicationContext { get; set; }

    public RepositoryBase(BankingApplicationContext eLearningContext)
    {
        this.bankingApplicationContext = eLearningContext;
    }

    public IQueryable<T> FindAll()
    {
        return this.bankingApplicationContext.Set<T>().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return this.bankingApplicationContext.Set<T>().Where(expression).AsNoTracking();
    }

    public void Create(T entity)
    {
        this.bankingApplicationContext.Set<T>().Add(entity);
        this.bankingApplicationContext.SaveChanges();
    }

    public void Update(T entity)
    {
        this.bankingApplicationContext.Set<T>().Update(entity);
        this.bankingApplicationContext.SaveChanges();
    }

    public void Delete(T entity)
    {
        this.bankingApplicationContext.Set<T>().Remove(entity);
        this.bankingApplicationContext.SaveChanges();
    }
}