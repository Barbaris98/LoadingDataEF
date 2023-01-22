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
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = db.Users.ToList();
                foreach (User user in users)
                    Console.WriteLine($"{user.Name} - {user.Company?.Name}");
            }
            /*
            Однако при использовании lazy loading следует учитывать что если в базе данных
            произошли какие-нибудь изменения, например, другой пользователь изменил данные,
            то данные не перезагужаются, контекст продолжает использовать те данные, которые
            были ранее загружены, как собственно было показано выше.

            */
        }
    }
}