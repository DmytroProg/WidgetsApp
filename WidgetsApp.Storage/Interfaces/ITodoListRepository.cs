using WidgetsApp.Storage.Models;

namespace WidgetsApp.Storage.Interfaces;

public interface ITodoListRepository
{
    Task<TodoList> AddTodoListAsync(TodoList todoList);
    Task<ListItem> AddListItemAsync(int todoListId, ListItem listItem);
    Task CheckListItemAsync(int listItemId);

    Task<TodoList[]> GetTodoListsAsync();
    Task<TodoList> GetTodoListByIdAsync(int id);
}