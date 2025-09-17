namespace WidgetsApp.Storage.Models;

public class ListItem
{
    public int Id { get; set; }
    public string Content { get; set; }
    public bool IsChecked { get; set; }
    
    public TodoList TodoList { get; set; }
    
    public int TodoListId { get; set; }
}

/*
 * create table ListItem(
 * Id int primary key identity,
 * Content varchar()
 * isChecked bit not null,
 * TodoListId int foreign key references TodoList(Id),
 */