n = int(input())
students = []

for _ in range(n):
    surname = input().strip()
    name = input().strip()
    class_name = input().strip()
    birth_date = input().strip()
    
    class_num = int(class_name[:-1])
    class_letter = class_name[-1]
    
    students.append((class_num, class_letter, surname, name, birth_date, class_name))

students.sort(key=lambda x: (x[0], x[1], x[2]))

for class_num, class_letter, surname, name, birth_date, class_name in students:
    print(class_name, surname, name, birth_date)