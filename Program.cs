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

                // добавляем начальные данные
                Company microsoft = new Company { Name = "Microsoft" };
                Company google = new Company { Name = "Google" };
                db.Companies.AddRange(microsoft, google);

                User tom = new User { Name = "Tom", Company = microsoft };
                User bob = new User { Name = "Bob", Company = google };
                User alice = new User { Name = "Alice", Company = microsoft };
                User kate = new User { Name = "Kate", Company = google };
                db.Users.AddRange(tom, bob, alice, kate);

                db.SaveChanges();

                /*
                // получаем пользователей
                var users = db.Users
                    .Include(u => u.Company)  // подгружаем данные по компаниям
                    .ToList();
                foreach (var user in users)
                    Console.WriteLine($"{user.Name} - {user.Company?.Name}");
                */


                var users = db.Users.ToList();  // метод Include не используется
                //                      Потому что мы уже добавили все объекты в контекст при их создании
                foreach (var user in users)
                    Console.WriteLine($"{user.Name} - {user.Company?.Name}");

            }




        }
    }
}