data = list(map(int, input().split()))
n = data[0]
grades = data[1:]

max_grade = max(grades)
min_grade = min(grades)

for i in range(n):
    if grades[i] == max_grade:
        grades[i] = min_grade

print(*grades)