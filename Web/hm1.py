#variant 5
class Tovar:
    
    def __init__(self, naimenovanie, tsena, nalichie=True):
  
        self.naimenovanie = naimenovanie
        self.tsena = tsena
        self.nalichie = nalichie
    
    def kupit(self):
       
        self.nalichie = not self.nalichie
        if not self.nalichie:
            print(f"Товар '{self.naimenovanie}' куплен!")
        else:
            print(f"Товар '{self.naimenovanie}' снова доступен для покупки.")
    
    def __str__(self):
        
        status = "есть в наличии" if self.nalichie else "нет в наличии"
        return f"Товар: {self.naimenovanie}, Цена: {self.tsena} руб., Статус: {status}"


# Демонстрация работы класса
if __name__ == "__main__":
    print("=== Демонстрация работы класса Tovar ===\n")
    
    tovar1 = Tovar("Ноутбук", 45000)
    tovar2 = Tovar("Мышь компьютерная", 1200, True)
    tovar3 = Tovar("Клавиатура", 3500, False)
    
    print("1. Начальное состояние товаров:")
    print(tovar1)
    print(tovar2)
    print(tovar3)
    print()
    
    print("2. Покупаем ноутбук:")
    tovar1.kupit()
    print(tovar1)
    print()
    
    print("3. Возвращаем ноутбук в наличие:")
    tovar1.kupit()
    print(tovar1)
    print()
    
    print("4. Меняем статус клавиатуры (которой не было):")
    tovar3.kupit()
    print(tovar3)
    print()
    
    print("5. Работа с несколькими товарами:")
    tovary = [
        Tovar("Монитор", 15000),
        Tovar("Веб-камера", 2500),
        Tovar("Наушники", 3000)
    ]
    
    print("Список всех товаров:")
    for i, tovar in enumerate(tovary, 1):
        print(f"  {i}. {tovar}")
    
    print("\nПокупаем монитор и наушники:")
    tovary[0].kupit()
    tovary[2].kupit()
    
    print("\nОбновленный список товаров:")
    for i, tovar in enumerate(tovary, 1):
        print(f"  {i}. {tovar}")