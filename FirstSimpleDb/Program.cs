using FirstSimpleDb.Data;
using Microsoft.EntityFrameworkCore;

using var context = new AppDbContext();

// 0. Сброс базы данных (понадобилось в следствии частого перезапуска программы)
context.Persons.RemoveRange(context.Persons.ToList());
context.SaveChanges();
context.Database.ExecuteSqlRaw("ALTER SEQUENCE \"Persons_Id_seq\" RESTART WITH 1");

// 1. Добавляю 1 запись
var person1 = new Person { Name = "Alice", Age = 25 };
context.Persons.Add(person1);

// 2. Добавляю несколько записей вручную
var persons = new[]
{
    new Person { Name = "Bob", Age = 28 },
    new Person { Name = "Charlie", Age = 35 },
    new Person { Name = "David", Age = 22 },
    new Person { Name = "Eve", Age = 42 },
    new Person { Name = "Frank", Age = 60 },
    new Person { Name = "Grace", Age = 59 },
};
context.Persons.AddRange(persons);

// 3. Добавляю ещё 20 случайных записей для теста
var random = new Random();
for (int i = 1; i <= 20; i++)
{
    context.Persons.Add(new Person
    {
        Name = $"Person_{i}",
        Age = random.Next(18, 60)
    });
}

context.SaveChanges();

// 4. Обновляю 1 запись
var alice = context.Persons.First(p => p.Name == "Alice");
alice.Age = 26;
context.SaveChanges();

// 5. Обновляю несколько записей
var adults = context.Persons.Where(p => p.Age >= 30).ToList();
foreach (var person in adults)
{
    person.Age += 1;
}
context.SaveChanges();

// 6. Удаляю 1 запись
var charlie = context.Persons.FirstOrDefault(p => p.Name == "Charlie");
if (charlie != null) context.Persons.Remove(charlie);
context.SaveChanges();

// 7. Удаляю несколько записей (например, очень взрослых >45)
var toDelete = context.Persons.Where(p => p.Age > 60).ToList();
context.Persons.RemoveRange(toDelete);
context.SaveChanges();

// 8. Получаем количество записей
var count = context.Persons.Count();
Console.WriteLine($"Total records: {count}");

// 9. Получаем 1 запись по условию
var bob = context.Persons.FirstOrDefault(p => p.Name == "Bob");
Console.WriteLine(bob != null ? $"Found: {bob.Name}, {bob.Age}" : "Not found");

// 10. Получаем несколько записей по условию
var young = context.Persons.Where(p => p.Age < 30).ToList();
Console.WriteLine($"Young persons count: {young.Count}");