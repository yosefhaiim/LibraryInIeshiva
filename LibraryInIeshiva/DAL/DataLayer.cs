using Microsoft.EntityFrameworkCore;
using LibraryInIeshiva.Models;
using static System.Net.Mime.MediaTypeNames;

namespace LibraryInIeshiva.DAL;
//  קלאס שמייצג את שכבת הנתונים יורש מקלאס נוסף, שזה דיבי קונטקסט
public class DataLayer: DbContext // הנקודותיים זה הורשה
{

    // קונסטרקטור שמקבל מחרוזת חיבור ומעביר אותה לקונסטרקטור האב
   public DataLayer(string connectionString) : base(GetOptions(connectionString))
   {
         Database.EnsureCreated();
         seed();
   }
    private void seed()
    {
        if (libraries.Any())
        {
            return;
        }
        Library firstLibrary = new Library();

        firstLibrary.numberLibrery = 1;
        firstLibrary.genre = "blabla";
        //firstLibrary.length = 20;
        libraries.Add(firstLibrary);
        SaveChanges();
    }
    


    public DbSet<Book> books { get; set; }


    public DbSet<Shelf> shelves { get; set; }

    public DbSet<Library> libraries { get; set; }





    //  פונקציה שמחזירה את אפשרויות ההתחברות למסד הנתונים
    private static DbContextOptions GetOptions(string connectionString)
    {
        return SqlServerDbContextOptionsExtensions
            .UseSqlServer(new DbContextOptionsBuilder(), connectionString)
            .Options;
       
    }


}
