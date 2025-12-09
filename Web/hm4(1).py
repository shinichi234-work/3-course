#variant 7
def retry3(func):
    def wrapper(*args, **kwargs):
        last_exception = None
        for attempt in range(3):
            try:
                result = func(*args, **kwargs)
                return result
            except Exception as e:
                last_exception = e
                print(f"Попытка {attempt + 1} не удалась: {e}")
        raise last_exception
    return wrapper


if __name__ == "__main__":
    print("=== Демонстрация работы декоратора retry3 ===\n")
    
    print("1. Пример из задания:")
    i = 0
    
    @retry3
    def flaky():
        global i
        i += 1
        if i < 3:
            raise ValueError("fail")
        return "success"
    
    print(flaky())
    print()
    
    print("2. Успешное выполнение с первой попытки:")
    
    @retry3
    def successful():
        return "Работает без ошибок!"
    
    print(successful())
    print()
    
    print("3. Функция всегда падает:")
    
    @retry3
    def always_fails():
        raise ZeroDivisionError("Ошибка!")
    
    try:
        always_fails()
    except ZeroDivisionError as e:
        print(f"Поймано исключение: {e}")