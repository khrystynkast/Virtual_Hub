using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;

public class ContactController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _env;

    public ContactController(ApplicationDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SaveContact(string name, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phoneNumber))
        {
            TempData["Message"] = "Wypełnij wszyskie pola";
            return RedirectToAction("Index");
        }
        if (!Regex.IsMatch(name, @"^[A-Za-zÀ-ÿА-Яа-яҐґЄєІіЇї]{3,}\s[A-Za-zÀ-ÿА-Яа-яҐґЄєІіЇї]{3,}$")){
            TempData["Message"] = "Imie i Nazwisko musi składać się z dwoch słow przez spacje.";
            return RedirectToAction("Index");
        }

        if (!Regex.IsMatch( phoneNumber, @"^\d{9}$"))
        {
            TempData["Message"] = "Numer telefonu musi zawierać dokładnie 9 cyfr.";
            return RedirectToAction("Index");
        }

        string? path = _env.WebRootPath;
        if (string.IsNullOrEmpty(path)){
            throw new InvalidOperationException("WebRootPath is not set.");
        }

        string fullPath = Path.Combine(path, "App_Data", "contacts.txt");
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);


        System.IO.File.AppendAllText(fullPath, $"{DateTime.Now}: {name}, {phoneNumber}{Environment.NewLine}");



        var contact = new Contact { Name = name, PhoneNumber = phoneNumber };
        _db.Contacts.Add(contact);
        _db.SaveChanges();

        TempData["Message"] = "Dane zapisane";
        return RedirectToAction("Index");
    }
}
