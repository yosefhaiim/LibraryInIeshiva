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




        // ���� ������ �� �������
        public IActionResult Librarys()
        {
            // ����� ���� �� �� �������
            List<Library> libraries = data.get.libraries.ToList();
            return View(libraries);
        }


        // ����� ���� ����� ����� ����
        public IActionResult CreateLibrery()
        {
            // ����� ���� ������
            return View(new Library());
        }

        // ����� ������� ����� ��� �������
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddLibrary(Library library)
        {
            // ����� ������ �����
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

        // ������� ������ �����
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditLibrarySaved(Library newLibrary)
        {
            if (newLibrary == null)
            {
                return RedirectToAction("Librarys");

            }
            // ����� ����� ��� ������ �� ������ ���� ��� �� ��� ������ �����
            Library? existingLibrary = data.get.libraries.FirstOrDefault(l => l.Id == newLibrary.Id);

            // ����� ��� ��� ����
            if (existingLibrary == null)
            {
                return RedirectToAction("Librarys");

            }

            data.get.Entry(existingLibrary).CurrentValues.SetValues(newLibrary);
            // ����� ��� ����
            data.get.SaveChanges();
            // ����� ����� �� ���� ����
            return RedirectToAction("Librarys");
        }

        // ������� ������
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Librarys");
            }

            // ����� �����
            Library? library = data.get.libraries.FirstOrDefault(library => library.Id == id);
            // ����� ��� ��� ����
            if (library == null)
            {
                // ��� ���� ���� ������
                return RedirectToAction("Librarys");
            }
            // ����� �� ���� ������

            return View(library);
        }

        public IActionResult DeleteLibrary(int? id)
        {
            // ����� ������� ���� ���� ����
            if (id == null)
            {
                return NotFound();
            }

            // ��� ���� ����� �����
            List<Library> libraries = data.get.libraries.ToList();

            // ��� ���� �� ����� �� ���� ������� ���� ��� ������ ��� �������
            Library? libraryToRemove = libraries.Find(library => library.Id == id);

            // ����� ���� �� ���
            if (libraryToRemove == null)
            {
                return NotFound();
            }

            // ����� ����
            data.get.libraries.Remove(libraryToRemove);
            // �����
            data.get.SaveChanges();
            // ����� ���� ������
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
            // ����� ���� �� �� ������
            List<Shelf> shelves = data.get.shelves.ToList();
            return View(shelves);
        }

        //  ����� ���� ����
        public IActionResult createSehlf()
        {
            return View(new Shelf());
        }

        // ����� ������� ��� �������
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddShelf(Shelf shelf, Library library)
        {

            shelf.length = library.length;
            shelf.libraryId = library.Id;
            // ����� ��� ����
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

            // ����� �����
            Shelf? shelf = data.get.shelves.FirstOrDefault(shelf => shelf.Id == id);
            // ����� ��� ��� ����
            if (shelf == null)
            {
                // ��� ���� ���� ������
                return RedirectToAction("Shelves");
            }
            // ����� �� ���� ���

            return View(shelf);
        }


        public IActionResult EditShelfSaved(Shelf newShelf)
        {
            if (newShelf == null)
            {
                return RedirectToAction("Shelves");

            }
            // ����� ����� ��� ������ �� ������ ���� ��� �� ��� ������ �����
            Shelf? existingShelf = data.get.shelves.FirstOrDefault(s => s.Id == newShelf.Id);

            // ����� ��� ��� ����
            if (existingShelf == null)
            {
                return RedirectToAction("Shelves");

            }

            data.get.Entry(existingShelf).CurrentValues.SetValues(newShelf);
            // ����� ��� ����
            data.get.SaveChanges();
            // ����� ����� �� ���� ����
            return RedirectToAction("Shelves");
        }


        public IActionResult DeleteShelf(int? id)
        {
            // ����� ������� ���� ���� ����
            if (id == null)
            {
                return NotFound();
            }

            // ��� ���� ����� �����
            List<Shelf> shelves = data.get.shelves.ToList();

            // ��� ���� �� ����� �� ���� ������� ���� ��� ������ ��� �������
            Shelf? shelfToRemove = shelves.Find(shelf => shelf.Id == id);

            // ����� ���� �� ���
            if (shelfToRemove == null)
            {
                return NotFound();
            }

            // ����� ����
            data.get.shelves.Remove(shelfToRemove);
            // �����
            data.get.SaveChanges();
            // ����� ���� ������
            return RedirectToAction(nameof(Shelves));

        }













        public IActionResult Books()
        {
            // ����� ���� �� �� ������
            List<Book> books = data.get.books.ToList();
            return View(books);
        }



        // ����� ���� ����
        public IActionResult createBook()
        {
            return View(new Book());
        }





        // ����� ������� ����

        // ����� �����
        //private Library lib;
        //[HttpPost, ValidateAntiForgeryToken]

        /*Library lib;
        public IActionResult AddBook(Book book, Shelf shelf)
		{
            
            if (book.high > shelf.height)
            {
                string? message = "The book is high";
                // ����� ��� ������ ����� �����
               //return RedirectToAction("Books", message);// ���� ���� �� �� ������ ����� ����� ����

               return ModelState(message: message);// ���� ����� ��� ������� ���
            }
            if (book.high - shelf.height > 10)
            {
                // ����� ��� ������ ����� ���� ���� ���� ������ ���� ��� ���
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
                    // ����� ����� �� ��� ����� ���� �� ������ ������� ����� ����
                    // ���� �� ���� ��� ����� ������
                    book.genre = lib.genre;
                    // ���� �� ������ ���� ��� ������ ���� �� ���� �� ��� ����
                    book.shelfId = shelf.Id;


                    // ����� ��� ����
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

            // ����� �����
            Book? book = data.get.books.FirstOrDefault(book => book.Id == id);
            // ����� ��� ��� ����
            if (book == null)
            {
                // ��� ���� ���� ������
                return RedirectToAction("Books");
            }
            // ����� �� ���� ���

            return View(book);
        }


        public IActionResult EditBookSaved(Book newBook)
        {
            if (newBook == null)
            {
                return RedirectToAction("Books");

            }
            // ����� ����� ��� ������ �� ������ ���� ��� �� ��� ������ �����
            Book? existingBook = data.get.books.FirstOrDefault(b => b.Id == newBook.Id);

            // ����� ��� ��� ����
            if (existingBook == null)
            {
                return RedirectToAction("Books");

            }

            data.get.Entry(existingBook).CurrentValues.SetValues(newBook);
            // ����� ��� ����
            data.get.SaveChanges();
            // ����� ����� �� ���� ����
            return RedirectToAction("Books");
        }













        public IActionResult DeleteBook(int? id)
        {

            List<Book> books = data.get.books.ToList();

            // ��� ���� �� ����� �� ���� ������� ���� ��� ������ ��� �������
            Book? bookToRemove = books.Find(book => book.Id == id);

            // ����� ���� �� ���
            if (bookToRemove == null)
            {
                return NotFound();
            }

            // ����� ����
            data.get.books.Remove(bookToRemove);
            // �����
            data.get.SaveChanges();
            // ����� ���� ������
            return RedirectToAction(nameof(books));

        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
