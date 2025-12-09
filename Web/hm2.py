#variant 10
class Author:

    def __init__(self, polnoe_imya, biografiya, spisok_proizvedeniy=None):
        self.polnoe_imya = polnoe_imya
        self.biografiya = biografiya
        self.spisok_proizvedeniy = spisok_proizvedeniy if spisok_proizvedeniy else []
    
    def dobavit_proizvedenie(self, proizvedenie):
        self.spisok_proizvedeniy.append(proizvedenie)
        print(f"Произведение '{proizvedenie}' добавлено в список произведений {self.polnoe_imya}")
    
    def __str__(self):
        proizvedeniya_str = ", ".join(self.spisok_proizvedeniy) if self.spisok_proizvedeniy else "нет произведений"
        return f"Автор: {self.polnoe_imya}\nБиография: {self.biografiya}\nПроизведения: {proizvedeniya_str}"


class Publication:

    def __init__(self, isbn, zhanr, avtor):
        self.isbn = isbn
        self.zhanr = zhanr
        self.avtor = avtor
    
    def __str__(self):
        return f"ISBN: {self.isbn}, Жанр: {self.zhanr}, Автор: {self.avtor.polnoe_imya}"


class Library:

    def __init__(self, nazvanie, mestonahozhdenie, fond_izdaniy=None):
        self.nazvanie = nazvanie
        self.mestonahozhdenie = mestonahozhdenie
        self.fond_izdaniy = fond_izdaniy if fond_izdaniy else []
    
    def dobavit_izdanie(self, izdanie):
        self.fond_izdaniy.append(izdanie)
        print(f"Издание (ISBN: {izdanie.isbn}) добавлено в библиотеку '{self.nazvanie}'")
    
    def nayti_po_avtoru(self, imya_avtora):
        rezultat = [izdanie for izdanie in self.fond_izdaniy 
                    if izdanie.avtor.polnoe_imya == imya_avtora]
        return rezultat
    
    def nayti_po_zhanru(self, zhanr):
        rezultat = [izdanie for izdanie in self.fond_izdaniy 
                    if izdanie.zhanr == zhanr]
        return rezultat
    
    def pokazat_vse_izdaniya(self):
        if not self.fond_izdaniy:
            print(f"В библиотеке '{self.nazvanie}' нет изданий.")
        else:
            print(f"\nИздания в библиотеке '{self.nazvanie}':")
            for i, izdanie in enumerate(self.fond_izdaniy, 1):
                print(f"  {i}. {izdanie}")
    
    def __str__(self):
        return f"Библиотека: {self.nazvanie}\nАдрес: {self.mestonahozhdenie}\nКоличество изданий: {len(self.fond_izdaniy)}"


if __name__ == "__main__":
    print("=== Демонстрация работы классов Library, Publication и Author ===\n")
    
    print("1. Создание авторов:")
    avtor1 = Author(
        "Лев Николаевич Толстой",
        "Русский писатель, мыслитель, просветитель (1828-1910)",
        ["Война и мир", "Анна Каренина"]
    )
    avtor2 = Author(
        "Федор Михайлович Достоевский",
        "Русский писатель, мыслитель, философ (1821-1881)",
        ["Преступление и наказание", "Братья Карамазовы"]
    )
    avtor3 = Author(
        "Александр Сергеевич Пушкин",
        "Русский поэт, драматург и прозаик (1799-1837)",
        []
    )
    
    avtor3.dobavit_proizvedenie("Евгений Онегин")
    avtor3.dobavit_proizvedenie("Капитанская дочка")
    print()
    
    print(avtor1)
    print("\n" + "="*60 + "\n")
    print(avtor3)
    print("\n" + "="*60 + "\n")
    
    print("2. Создание изданий:")
    izdanie1 = Publication("978-5-17-123456-1", "Роман", avtor1)
    izdanie2 = Publication("978-5-17-123456-2", "Роман", avtor1)
    izdanie3 = Publication("978-5-17-123456-3", "Роман", avtor2)
    izdanie4 = Publication("978-5-17-123456-4", "Роман", avtor2)
    izdanie5 = Publication("978-5-17-123456-5", "Поэма", avtor3)
    izdanie6 = Publication("978-5-17-123456-6", "Повесть", avtor3)
    
    print(izdanie1)
    print(izdanie3)
    print(izdanie5)
    print("\n" + "="*60 + "\n")
    
    print("3. Создание библиотеки:")
    biblioteka = Library(
        "Центральная городская библиотека",
        "г. Москва, ул. Пушкина, д. 10"
    )
    print(biblioteka)
    print("\n" + "="*60 + "\n")
    
    print("4. Добавление изданий в библиотеку:")
    biblioteka.dobavit_izdanie(izdanie1)
    biblioteka.dobavit_izdanie(izdanie2)
    biblioteka.dobavit_izdanie(izdanie3)
    biblioteka.dobavit_izdanie(izdanie4)
    biblioteka.dobavit_izdanie(izdanie5)
    biblioteka.dobavit_izdanie(izdanie6)
    print("\n" + "="*60 + "\n")
    
    print("5. Все издания в библиотеке:")
    biblioteka.pokazat_vse_izdaniya()
    print("\n" + "="*60 + "\n")
    
    print("6. Поиск изданий по автору:")
    naydennye = biblioteka.nayti_po_avtoru("Лев Николаевич Толстой")
    print(f"Найдено изданий Толстого: {len(naydennye)}")
    for izdanie in naydennye:
        print(f"  - {izdanie}")
    print()
    
    naydennye = biblioteka.nayti_po_avtoru("Александр Сергеевич Пушкин")
    print(f"Найдено изданий Пушкина: {len(naydennye)}")
    for izdanie in naydennye:
        print(f"  - {izdanie}")
    print("\n" + "="*60 + "\n")
    
    print("7. Поиск изданий по жанру:")
    naydennye_romany = biblioteka.nayti_po_zhanru("Роман")
    print(f"Найдено романов: {len(naydennye_romany)}")
    for izdanie in naydennye_romany:
        print(f"  - {izdanie}")
    print()
    
    naydennye_poemy = biblioteka.nayti_po_zhanru("Поэма")
    print(f"Найдено поэм: {len(naydennye_poemy)}")
    for izdanie in naydennye_poemy:
        print(f"  - {izdanie}")
    print("\n" + "="*60 + "\n")
    
    print("8. Итоговая информация о библиотеке:")
    print(biblioteka)