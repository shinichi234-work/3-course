#varinat 3
class Employee:

    def __init__(self, name, salary):
        self.name = name
        self.salary = salary
    
    def calculate_bonus(self):
        return self.salary * 0.10
    
    def __str__(self):
        return f"Сотрудник: {self.name}, Зарплата: {self.salary} руб., Премия: {self.calculate_bonus()} руб."


class Manager(Employee):

    def __init__(self, name, salary, management_level):
        super().__init__(name, salary)
        self.management_level = management_level
    
    def calculate_bonus(self):
        bonus = super().calculate_bonus()
        if self.management_level > 3:
            bonus += self.salary * 0.05
        return bonus
    
    def __str__(self):
        return f"Менеджер: {self.name}, Зарплата: {self.salary} руб., Уровень руководства: {self.management_level}, Премия: {self.calculate_bonus()} руб."


class Developer(Employee):

    def __init__(self, name, salary, know_javascript):
        super().__init__(name, salary)
        self.know_javascript = know_javascript
    
    def calculate_bonus(self):
        bonus = super().calculate_bonus()
        if self.know_javascript:
            bonus += self.salary * 0.03
        return bonus
    
    def __str__(self):
        js_status = "Да" if self.know_javascript else "Нет"
        return f"Разработчик: {self.name}, Зарплата: {self.salary} руб., Знает JavaScript: {js_status}, Премия: {self.calculate_bonus()} руб."


if __name__ == "__main__":
    print("=== Демонстрация работы классов Employee, Manager и Developer ===\n")
    
    print("1. Создание сотрудников:")
    
    employee1 = Employee("Иван Петров", 50000)
    print(employee1)
    print()
    
    manager1 = Manager("Анна Смирнова", 80000, 4)
    print(manager1)
    print()
    
    developer1 = Developer("Дмитрий Козлов", 70000, True)
    print(developer1)
    print()
    
    print("="*70)
    print()
    
    print("2. Сравнение премий:")
    
    manager2 = Manager("Ольга Николаева", 80000, 2)
    developer2 = Developer("Сергей Волков", 70000, False)
    
    print(f"Менеджер уровня 4: {manager1.name}")
    print(f"  Зарплата: {manager1.salary} руб.")
    print(f"  Премия: {manager1.calculate_bonus()} руб. (10% базовая + 5% за уровень > 3)")
    print()
    
    print(f"Менеджер уровня 2: {manager2.name}")
    print(f"  Зарплата: {manager2.salary} руб.")
    print(f"  Премия: {manager2.calculate_bonus()} руб. (только 10% базовая)")
    print()
    
    print(f"Разработчик, владеющий JavaScript: {developer1.name}")
    print(f"  Зарплата: {developer1.salary} руб.")
    print(f"  Премия: {developer1.calculate_bonus()} руб. (10% базовая + 3% за JS)")
    print()
    
    print(f"Разработчик, не владеющий JavaScript: {developer2.name}")
    print(f"  Зарплата: {developer2.salary} руб.")
    print(f"  Премия: {developer2.calculate_bonus()} руб. (только 10% базовая)")
    print()
    
    print("="*70)
    print()
    
    print("3. Список всех сотрудников:")
    sotrudniki = [employee1, manager1, manager2, developer1, developer2]
    
    for i, sotrudnik in enumerate(sotrudniki, 1):
        print(f"{i}. {sotrudnik}")
    
    print()
    print("="*70)
    print()
    
    print("4. Общая сумма премий:")
    total_bonus = sum(sotrudnik.calculate_bonus() for sotrudnik in sotrudniki)
    print(f"Итого премий: {total_bonus} руб.")