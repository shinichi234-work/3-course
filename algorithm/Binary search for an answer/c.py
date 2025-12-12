a = int(input())
b = int(input())
c = int(input())

left = 0
right = 10**16
result = 0

while left <= right:
    mid = (left + right) // 2
    
    total_grades = a + b + c + mid
    sum_grades = 2 * a + 3 * b + 4 * c + 5 * mid
    
    if 2 * sum_grades >= 7 * total_grades:
        result = mid
        right = mid - 1
    else:
        left = mid + 1

print(result)