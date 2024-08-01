using System.ComponentModel.DataAnnotations;

namespace LibraryInIeshiva.Models
{
    public class Library
    {
        public Library()
        {
            shelves = new List<Shelf>();
        }
        [Key]
        public int Id { get; set; } // מפתח

        public int numberLibrery { get; set; } // לבדוק אם צריך

        public string genre { get; set; } // סוגה של ספריה

        public int length { get; set; } // אני צריך לדאוג לכך שאורך המדף בקלאס מדף יקבע לפי האורך הזה

        public List<Shelf> shelves { get; set; } // רשימת מדפים
    }
}
