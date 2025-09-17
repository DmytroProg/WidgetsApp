using System.ComponentModel.DataAnnotations;

namespace WidgetsApp.Storage.Models;

public class TodoList
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<ListItem> ListItems { get; set; }
}