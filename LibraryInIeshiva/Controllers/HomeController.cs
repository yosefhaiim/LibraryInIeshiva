using LibraryInIeshiva.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFreinds.DAL;
using System.Diagnostics;

namespace LibraryInIeshiva.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }




        // הצגת הרשימה של הספריות
        public IActionResult Librarys()
        {
            // החזרת חלון עם כל הספריות
            List<Library> libraries = data.get.libraries.ToList();
            return View(libraries);
        }


        // יצירת חלון רישום ספריה חדשה
        public IActionResult CreateLibrery()
        {
            // החזרת חלון ספרייה
            return View(new Library());
        }

        // יצירת אובייקט ספריה חדש ושמירתו
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddLibrary(Library library)
        {
            // שמירת ספרייה נוספת
            data.get.libraries.Add(library);
            data.get.SaveChanges();
            return RedirectToAction("Librarys");
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            Library? library = data.get.libraries.Include(l => l.shelves).ToList().FirstOrDefault(library => library.Id == id);

            if (library == null)
            {
                return RedirectToAction("Library");
            }

            return View(library);
        }

        // פונקציה לשמירת עריכה
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditLibrarySaved(Library newLibrary)
        {
            if (newLibrary == null)
            {
                return RedirectToAction("Librarys");

            }
            // יצירת משתנה חדש ובדיקה על התעודת זהות שלו על מנת להציגה בחלון
            Library? existingLibrary = data.get.libraries.FirstOrDefault(l => l.Id == newLibrary.Id);

            // בדיקה האם הוא קיים
            if (existingLibrary == null)
            {
                return RedirectToAction("Librarys");

            }

            data.get.Entry(existingLibrary).CurrentValues.SetValues(newLibrary);
            // שמירת חבר נוסף
            data.get.SaveChanges();
            // החזרת החלון עם החבר מחדש
            return RedirectToAction("Librarys");
        }

        // פונקציה לעריכה
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Librarys");
            }

            // הכרזת משתנה
            Library? library = data.get.libraries.FirstOrDefault(library => library.Id == id);
            // בדיקה האם הוא קיים
            if (library == null)
            {
                // אני מפנה אותו לפעולה
                return RedirectToAction("Librarys");
            }
            // מחזיר את חלון פריינד

            return View(library);
        }

        public IActionResult DeleteLibrary(int? id)
        {
            // בדיקה שהתעודת זהות אינה ריקה
            if (id == null)
            {
                return NotFound();
            }

            // אני בונה רשימת חברים
            List<Library> libraries = data.get.libraries.ToList();

            // אני אומר לו למצוא את החבר שהתעודת זהות שלו מתאימה לזו שהוכנסה
            Library? libraryToRemove = libraries.Find(library => library.Id == id);

            // בדיקה שהוא לא ריק
            if (libraryToRemove == null)
            {
                return NotFound();
            }

            // מחיקת החבר
            data.get.libraries.Remove(libraryToRemove);
            // שמירה
            data.get.SaveChanges();
            // החזרת עמוד הרשימה
            return RedirectToAction(nameof(Librarys));

        }





        public IActionResult ShelfInLibrary(int id)
        {
            Shelf? shelves = data.get.shelves.Include(s => s.libraryId)
                .ToList().FirstOrDefault(shelves => shelves.Id == id);
            return View(shelves);
        }







        public IActionResult Shelves()
        {
            // החזרת חלון עם כל המדפים
            List<Shelf> shelves = data.get.shelves.ToList();
            return View(shelves);
        }

        //  יצירת חלון למדף
        public IActionResult createSehlf()
        {
            return View(new Shelf());
        }

        // החזרת אובייקט מדף ושמירתו
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddShelf(Shelf shelf, Library library)
        {

            shelf.length = library.length;
            shelf.libraryId = library.Id;
            // שמירת מדף נוסף
            data.get.shelves.Add(shelf);
            data.get.SaveChanges();
            return RedirectToAction("Shelves");
        }

        [HttpGet]
        public IActionResult ShelfDetails(int? id)
        {
            Shelf? shelf = data.get.shelves.Include(s => s.books).ToList().FirstOrDefault(Shelf => Shelf.Id == id);

            if (shelf == null)
            {
                return RedirectToAction("Shelves");
            }

            return View(shelf);
        }

        public IActionResult EditShelf(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Shelves");
            }

            // הכרזת משתנה
            Shelf? shelf = data.get.shelves.FirstOrDefault(shelf => shelf.Id == id);
            // בדיקה האם הוא קיים
            if (shelf == null)
            {
                // אני מפנה אותו לפעולה
                return RedirectToAction("Shelves");
            }
            // מחזיר את חלון מדף

            return View(shelf);
        }


        public IActionResult EditShelfSaved(Shelf newShelf)
        {
            if (newShelf == null)
            {
                return RedirectToAction("Shelves");

            }
            // יצירת משתנה חדש ובדיקה על התעודת זהות שלו על מנת להציגה בחלון
            Shelf? existingShelf = data.get.shelves.FirstOrDefault(s => s.Id == newShelf.Id);

            // בדיקה האם הוא קיים
            if (existingShelf == null)
            {
                return RedirectToAction("Shelves");

            }

            data.get.Entry(existingShelf).CurrentValues.SetValues(newShelf);
            // שמירת מדף נוסף
            data.get.SaveChanges();
            // החזרת החלון עם המדף מחדש
            return RedirectToAction("Shelves");
        }


        public IActionResult DeleteShelf(int? id)
        {
            // בדיקה שהתעודת זהות אינה ריקה
            if (id == null)
            {
                return NotFound();
            }

            // אני בונה רשימת חברים
            List<Shelf> shelves = data.get.shelves.ToList();

            // אני אומר לו למצוא את החבר שהתעודת זהות שלו מתאימה לזו שהוכנסה
            Shelf? shelfToRemove = shelves.Find(shelf => shelf.Id == id);

            // בדיקה שהוא לא ריק
            if (shelfToRemove == null)
            {
                return NotFound();
            }

            // מחיקת החבר
            data.get.shelves.Remove(shelfToRemove);
            // שמירה
            data.get.SaveChanges();
            // החזרת עמוד הרשימה
            return RedirectToAction(nameof(Shelves));

        }













        public IActionResult Books()
        {
            // החזרת חלון עם כל המדפים
            List<Book> books = data.get.books.ToList();
            return View(books);
        }



        // יצירת חלון לספר
        public IActionResult createBook()
        {
            return View(new Book());
        }





        // יצירת אובייקט הספר

        // הכרזת משתנה
        //private Library lib;
        //[HttpPost, ValidateAntiForgeryToken]

        /*Library lib;
        public IActionResult AddBook(Book book, Shelf shelf)
		{
            
            if (book.high > shelf.height)
            {
                string? message = "The book is high";
                // למצוא דרך להדפיס הודעת שגיאה
               //return RedirectToAction("Books", message);// צריך לסדר את זה שיחזיר הודעה שהספר גדול

               return ModelState(message: message);// צריך לבדוק איך משתמשים בזה
            }
            if (book.high - shelf.height > 10)
            {
                // למצוא דרך להדפיס שהספר נמוך מידי והאם להכניס אותו בכל זאת
                if ( == "no")
                {
                    return RedirectToAction("Books");
                }
                else
                {
                    foreach (Shelf shelf1 in data.get.shelves)
                    {
                        foreach (Library library in data.get.libraries)
                        {
                            if (shelf.libraryId == library.Id)
                            {
                                lib = library;
                            }
                        }
                    }
                    // בניית משתנה על מנת להשיג סוגה של הספריה ולהכניס לסוגת הספר
                    // השמה של סוגת ספר כסוגת ספרייה
                    book.genre = lib.genre;
                    // השמה של התעודת זהות מדף כתעודת זהות של המדף בו הוא נמצא
                    book.shelfId = shelf.Id;


                    // שמירת ספר נוסף
                    data.get.books.Add(book);
                    data.get.SaveChanges();
                    return RedirectToAction("Books");
                }

            }

            return RedirectToAction("Books");

        }*/




        public IActionResult BookDetails(int? id)
        {
            Book? book = data.get.books.Include(b => b.shelfId).ToList().FirstOrDefault(Book => Book.Id == id);

            if (book == null)
            {
                return RedirectToAction("Books");
            }

            return View(book);
        }



        public IActionResult EditBook(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Books");
            }

            // הכרזת משתנה
            Book? book = data.get.books.FirstOrDefault(book => book.Id == id);
            // בדיקה האם הוא קיים
            if (book == null)
            {
                // אני מפנה אותו לפעולה
                return RedirectToAction("Books");
            }
            // מחזיר את חלון מדף

            return View(book);
        }


        public IActionResult EditBookSaved(Book newBook)
        {
            if (newBook == null)
            {
                return RedirectToAction("Books");

            }
            // יצירת משתנה חדש ובדיקה על התעודת זהות שלו על מנת להציגה בחלון
            Book? existingBook = data.get.books.FirstOrDefault(b => b.Id == newBook.Id);

            // בדיקה האם הוא קיים
            if (existingBook == null)
            {
                return RedirectToAction("Books");

            }

            data.get.Entry(existingBook).CurrentValues.SetValues(newBook);
            // שמירת ספר נוסף
            data.get.SaveChanges();
            // החזרת החלון עם הספר מחדש
            return RedirectToAction("Books");
        }













        public IActionResult DeleteBook(int? id)
        {

            List<Book> books = data.get.books.ToList();

            // אני אומר לו למצוא את החבר שהתעודת זהות שלו מתאימה לזו שהוכנסה
            Book? bookToRemove = books.Find(book => book.Id == id);

            // בדיקה שהוא לא ריק
            if (bookToRemove == null)
            {
                return NotFound();
            }

            // מחיקת החבר
            data.get.books.Remove(bookToRemove);
            // שמירה
            data.get.SaveChanges();
            // החזרת עמוד הרשימה
            return RedirectToAction(nameof(books));

        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
