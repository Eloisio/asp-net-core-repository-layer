using TWTodoList.Contexts;
using TWTodoList.Exceptions;
using TWTodoList.Models;
using TWTodoList.Repositories;
using TWTodoList.ViewModels;

namespace TWTodoList.Services;

public class TodoService
{
    private readonly AppDbContex _context;
    private readonly TodoRepository _repository;

    public TodoService(AppDbContex context, TodoRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public ListTodoViewModel FindAll()
    {
        return new ListTodoViewModel
        {
            Todos = _repository.FindAll(x => x.Date)
        };
    }

    public void Create(FormTodoViewModel data)
    {
        var todo = new Todo(data.Title, data.Date);
        _repository.Create(todo);
    }

    public FormTodoViewModel FindById(int id)
    {
        var todo = findByIdOrElseThrow(id);
        return new FormTodoViewModel { Title = todo.Title, Date = todo.Date };
    }

    public void UpdateById(int id, FormTodoViewModel data)
    {
        var todo = findByIdOrElseThrow(id);
        todo.Title = data.Title;
        todo.Date = data.Date;
        _repository.Update(todo);
    }

    public void DeleteById(int id)
    {
        var todo = findByIdOrElseThrow(id);
        _context.Remove(todo);
        _context.SaveChanges();
    }

    public void ToComplete(int id)
    {
        var todo = findByIdOrElseThrow(id);
        todo.IsCompleted = true;
        _context.SaveChanges();
    }

    private Todo findByIdOrElseThrow(int id)
    {
        var todo = _repository.FindById(id);
        if (todo is null)
        {
            throw new TodoNotFoundException();
        }
        return todo;
    }
}