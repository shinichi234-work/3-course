namespace HM1;

public class PhoneBook
{
    private readonly List<Person> contacts;

    public IReadOnlyList<Person> Contacts => contacts.AsReadOnly();

    public PhoneBook()
    {
        contacts = new List<Person>();
    }

    public void AddContact(Person person)
    {
        if (person == null)
        {
            throw new ArgumentNullException(nameof(person));
        }

        contacts.Add(person);
    }

public Person? FindByName(string fullName)
{
    return contacts.FirstOrDefault(
        p => p.FullName.Equals(
            fullName,
            StringComparison.OrdinalIgnoreCase
        )
    );
}

    public override string ToString()
    {
        string contactWord = contacts.Count == 1 ? "контакт" :
                            contacts.Count < 5 ? "контакта" : "контактов";
        return $"Телефонная книга: {contacts.Count} {contactWord}";
    }
}