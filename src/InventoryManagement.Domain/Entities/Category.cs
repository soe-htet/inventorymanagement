namespace InventoryManagement.Domain.Entities;

public class Category
{
    public int Id{get; set;}
    public string Name{get; private set;}

    public Category(string name){
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category name is required");
        }
        Name = name;
    }
}