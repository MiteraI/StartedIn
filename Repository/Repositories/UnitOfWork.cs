﻿using Domain.Context;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Repositories.Interface;
namespace Repository.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public async Task CommitAsync()
    {
        await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}