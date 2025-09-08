class Tovar:
    def __init__(self, name, price, availability):
        self.name = name
        self.price = price
        self.availability = availability  # True - есть в наличии, False - нет в наличии

    def buy(self):
        self.availability = not self.availability

    def __str__(self):
        status = "есть в наличии" if self.availability else "нет в наличии"
        return f"Товар: {self.name}, Цена: {self.price}, Статус: {status}"

# Демонстрация работы класса
tovar = Tovar("Ноутбук", 50000, True)
print(tovar)
tovar.buy()
print(tovar)