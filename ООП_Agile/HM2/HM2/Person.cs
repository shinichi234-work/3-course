namespace HM1;

public class Person
{
    private readonly List<Phone> phones;

    public string FullName { get; }
    public int Age { get; }
    public IReadOnlyList<Phone> Phones => phones.AsReadOnly();

    public Person(string fullName, int age)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException(
                "ФИО не может быть пустым",
                nameof(fullName)
            );
        }

        if (age < 0)
        {
            throw new ArgumentException(
                "Возраст не может быть отрицательным",
                nameof(age)
            );
        }

        FullName = fullName;
        Age = age;
        phones = new List<Phone>();
    }

    public void AddPhone(Phone phone)
    {
        if (phone == null)
        {
            throw new ArgumentNullException(nameof(phone));
        }

        phones.Add(phone);
    }

    public override string ToString()
    {
        string phoneWord = phones.Count == 1 ? "телефон" : 
                          phones.Count < 5 ? "телефона" : "телефонов";
        return $"{FullName}, {Age} лет, {phones.Count} {phoneWord}";
    }
}