namespace HM1;

public class Program
{
    public static void Main()
    {
        var phoneBook = new PhoneBook();

        var ivan = new Person("Иван Петров", 30);
        var iPhone = new Phone("iPhone 14", "МТС", ivan);
        var samsung = new Phone("Samsung Galaxy", "Билайн", ivan);
        
        ivan.AddPhone(iPhone);
        ivan.AddPhone(samsung);

        var anna = new Person("Анна Смирнова", 25);
        var pixel = new Phone("Google Pixel", "Мегафон", anna);
        anna.AddPhone(pixel);

        phoneBook.AddContact(ivan);
        phoneBook.AddContact(anna);

        Console.WriteLine(phoneBook);
        Console.WriteLine();

        foreach (var contact in phoneBook.Contacts)
        {
            Console.WriteLine(contact);
            foreach (var phone in contact.Phones)
            {
                Console.WriteLine($"  - {phone}");
            }
            Console.WriteLine();
        }

        var found = phoneBook.FindByName("Иван Петров");
        if (found != null)
        {
            Console.WriteLine($"Найден контакт: {found}");
        }
    }
}