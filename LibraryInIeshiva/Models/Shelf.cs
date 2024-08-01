using System.ComponentModel.DataAnnotations;

namespace LibraryInIeshiva.Models
{
    public class Shelf
    {
        public Shelf()
        {
            books = new List<Book>();
        }

        [Key]
        public int Id { get; set; } // מפתח

        public int libraryId { get; set; } // ספריה

        public int height {  get; set; } // גובה

        public int length { get; set; } // אורך צריך לקבוע אותו לפי אורך ספריה

        public List<Book> books { get; set; } // רשימת ספרים

    }
}
