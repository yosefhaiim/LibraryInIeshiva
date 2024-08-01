using System.ComponentModel.DataAnnotations;

namespace LibraryInIeshiva.Models
{
    public class Book
    {
        public Book()
        {
        
        }

        [Key]
        public int Id { get; set; } // תעודת זהות - מפתח

        public int shelfId { get; set; } // מקבל את המדף בו הוא נמצא

        public string name { get; set; } // שם ספר

        public string genre { get; set; } // סוגה

        public int high {  get; set; } // גובה

        public int thickness { get; set; } // עובי

    }
}
