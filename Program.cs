using Microsoft.EntityFrameworkCore;

namespace LoadingData
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using (ApplicationContext db = new ApplicationContext())
            {
                // пересоздадим базу данных
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Country usa = new Country { Name = "USA" };
                Country japan = new Country { Name = "Japan" };
                db.Countries.AddRange(usa, japan);

                // добавляем начальные данные
                Company microsoft = new Company { Name = "Microsoft", Country = usa };
                Company sony = new Company { Name = "Sony", Country = japan };
                db.Companies.AddRange(microsoft, sony);


                User tom = new User { Name = "Tom", Company = microsoft };
                User bob = new User { Name = "Bob", Company = sony };
                User alice = new User { Name = "Alice", Company = microsoft };
                User kate = new User { Name = "Kate", Company = sony };
                db.Users.AddRange(tom, bob, alice, kate);

                db.SaveChanges();
            }
            // получение данных
            using (ApplicationContext db = new ApplicationContext())
            {
                // получаем пользователей
                var users = db.Users
                    .Include(u => u.Company)  // подгружаем данные по компаниям
                        .ThenInclude(c => c!.Country)    // к компаниям подгружаем данные по странам
                    .ToList();         // (null-forgiving оператор), чтобы указать, что значение null в данной ситуации невоможно
                foreach (var user in users)
                    Console.WriteLine($"{user.Name} - {user.Company?.Name} - {user.Company?.Country?.Name}");
            }





        }
    }
}