step:1 : create an web api project 
step2: add negut pakages (install)
step3: test the project with the weather api 
step4: add a model class 
namespace TodoApi.Models;

public class TodoItem
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}
step 5: add a database context:
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;
}

step 6:  register the db context   (update those builders in program.cs)
step 7: crate a controllelr folder and add the controller  
step 8 : update the updat ethe create todo method 
step: 9 test the post api and then get that api using get 
