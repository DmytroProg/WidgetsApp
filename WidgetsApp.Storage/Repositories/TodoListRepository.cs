using Microsoft.EntityFrameworkCore;
using WidgetsApp.Storage.Interfaces;
using WidgetsApp.Storage.Models;

namespace WidgetsApp.Storage.Repositories;

public class TodoListRepository : ITodoListRepository
{
    private readonly WidgetsContext _context;

    public TodoListRepository(WidgetsContext context)
    {
        _context = context;
    }

    public async Task<TodoList> AddTodoListAsync(TodoList todoList)
    {
        _context.Add(todoList);

        await _context.SaveChangesAsync();

        return todoList;
    }

    public async Task<ListItem> AddListItemAsync(int todoListId, ListItem listItem)
    {
        listItem.TodoListId = todoListId;

        _context.Add(listItem);
        await _context.SaveChangesAsync();

        return listItem;
    }

    public async Task CheckListItemAsync(int listItemId)
    {
        var listItem = await _context.Set<ListItem>().FindAsync(listItemId);

        listItem.IsChecked = !listItem.IsChecked;

        await _context.SaveChangesAsync();
    }

    public Task<TodoList[]> GetTodoListsAsync()
    {
        return _context.Set<TodoList>()
            .Include(x => x.ListItems)
            .ToArrayAsync();
    }

    public Task<TodoList> GetTodoListByIdAsync(int id)
    {
        return _context.Set<TodoList>()
            .Include(x => x.ListItems)
            .FirstAsync(x => x.Id == id);
    }
}