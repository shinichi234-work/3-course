#variant 8
import time


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


def debounce(wait_seconds):
    def decorator(func):
        last_call_time = [0]
        
        def wrapper(*args, **kwargs):
            current_time = time.time()
            time_since_last_call = current_time - last_call_time[0]
            
            if time_since_last_call < wait_seconds:
                wait_time = wait_seconds - time_since_last_call
                print(f"Ожидание {wait_time:.3f} секунд...")
                time.sleep(wait_time)
            
            last_call_time[0] = time.time()
            return func(*args, **kwargs)
        
        return wrapper
    return decorator


if __name__ == "__main__":
    print("=== Задание 4.1: Декоратор retry3 ===\n")
    
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
    
    print("\n" + "="*70 + "\n")
    
    print("=== Задание 4.2: Декоратор debounce ===\n")
    
    print("1. Пример из задания:")
    
    @debounce(0.2)
    def action():
        print("done")
    
    start = time.time()
    action()
    action()
    action()
    end = time.time()
    print(f"Общее время выполнения: {end - start:.3f} секунд\n")
    
    print("2. Быстрые вызовы с паузой 0.5 секунд:")
    
    @debounce(0.5)
    def quick_action():
        print("Выполнено!")
    
    start = time.time()
    for i in range(3):
        print(f"Вызов {i + 1}:")
        quick_action()
    end = time.time()
    print(f"Общее время: {end - start:.3f} секунд\n")
    
    print("3. Вызов с естественной паузой:")
    
    @debounce(0.3)
    def delayed_action():
        print("Действие выполнено")
    
    delayed_action()
    time.sleep(0.4)
    delayed_action()