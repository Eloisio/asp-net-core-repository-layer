using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TWTodoList.Contexts;
using TWTodoList.Models;

namespace TWTodoList.Repositories;

public class TodoRepository
{
    private readonly AppDbContex _context;

    public TodoRepository(AppDbContex context)
    {
        _context = context;
    }

    public ICollection<Todo> FindAll()
    {
        return _context.Todos
            .AsNoTracking()
            .ToList();
    }

    public ICollection<Todo> FindAll<TKey>(Expression<Func<Todo, TKey>> orderBy)
    {
        return _context.Todos
            .OrderBy(orderBy)
            .AsNoTracking()
            .ToList();
    }
}