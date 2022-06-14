using TWTodoList.Contexts;

namespace TWTodoList.Repositories;

public class TodoRepository
{
    private readonly AppDbContex _context;

    public TodoRepository(AppDbContex context)
    {
        _context = context;
    }
}